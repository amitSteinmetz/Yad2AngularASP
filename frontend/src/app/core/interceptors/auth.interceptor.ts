import { HttpInterceptorFn, HttpContextToken } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { switchMap } from 'rxjs/operators';

export const IS_PUBLIC = new HttpContextToken(() => false);

// Mock API address - user will fill this
const API_URL = 'https://localhost:7003/api';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const isPublic = req.context.get(IS_PUBLIC);

  // If not for our API, proceed normally without touching it
  if (!req.url.startsWith(API_URL)) {
    return next(req);
  }

  // For public API requests (login, register), just set withCredentials
  if (isPublic) {
    console.log('Public request, setting withCredentials');
    return next(req.clone({ withCredentials: true }));
  }

  // For protected API requests, ensure we have a valid token
  return authService.getValidToken().pipe(
    switchMap((token) => {
      const cloned = req.clone({
        setHeaders: token ? { Authorization: `Bearer ${token}` } : {},
        withCredentials: true,
      });
      return next(cloned);
    }),
  );
};
