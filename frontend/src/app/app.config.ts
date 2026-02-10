import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { provideAppInitializer, inject } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import { SocialLoginModule, SocialAuthServiceConfig, SOCIAL_AUTH_CONFIG } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    {
      provide: SOCIAL_AUTH_CONFIG,
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '270038545791-911pme9n2hl07tm2pkk8brnaal3153nh.apps.googleusercontent.com'
            )
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    },
    provideAppInitializer(() => {
      const authService = inject(AuthService);

      // בדיקה אם קיים סימן שהמשתמש היה מחובר
      const wasLoggedIn = localStorage.getItem('logged_in') === 'true';

      if (!wasLoggedIn) {
        // אם אין דגל, אנחנו "מדלגים" על הריענון ומחזירים Observable ריק
        console.log('No refresh token indicator found, skipping refresh.');
        return of(null);
      }

      console.log('Refreshing token...');
      return authService.refreshToken().pipe(catchError((error) => {
        // אם הריענון נכשל (למשל העוגייה פגה), ננקה את הדגל
        localStorage.removeItem('logged_in');
        return of(null);
      }));
    }),
  ],
};
