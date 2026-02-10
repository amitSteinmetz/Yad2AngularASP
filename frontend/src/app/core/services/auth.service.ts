import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { catchError, tap, switchMap, map, finalize } from 'rxjs/operators';
import { throwError, Observable, of } from 'rxjs';
import { User } from '../models/user.model';
import { LoginDetails } from '../models/loginDetails.model';
import { RegisterDetails } from '../models/registerDetails.model';
import { IS_PUBLIC } from '../interceptors/auth.interceptor';

export interface LoginResponse {
  accessToken: string;
  user: User;
  message: string;
}

export interface RegisterResponse {
  message: string;
}

const API_BASE_URL = 'https://localhost:7003/api/Auth';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentUserSignal = signal<User | null>(null);
  currentUser = this.currentUserSignal.asReadonly();
  private accessToken: string | null = null;

  constructor(private http: HttpClient) { }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expiry = payload.exp;
      // Check if expired (with a 10-second buffer)
      return Math.floor(Date.now() / 1000) >= expiry - 10;
    } catch {
      return true;
    }
  }

  getValidToken(): Observable<string | null> {
    const token = this.accessToken;

    // If no token, we don't refresh here (handled by bootstrap)
    if (!token) {
      return of(null);
    }

    // If token exists but is expired, we refresh
    if (this.isTokenExpired(token)) {
      return this.handleTokenRefresh();
    }

    return of(token);
  }

  private handleTokenRefresh(): Observable<string | null> {
    return this.refreshToken().pipe(
      map((response) => response.accessToken),
      catchError((err) => throwError(() => err)),
    );
  }

  register(credentials: RegisterDetails) {
    return this.http
      .post<RegisterResponse>(`${API_BASE_URL}/register`, credentials, {
        context: new HttpContext().set(IS_PUBLIC, true),
      })
      .pipe(
        catchError((error) => {
          let errorMessage = 'אירעה שגיאה בתהליך ההרשמה.';

          if (error.error) {
            if (typeof error.error === 'string') {
              errorMessage = error.error;
            } else if (error.error.message) {
              errorMessage = error.error.message;
            } else if (error.error.error) {
              errorMessage = error.error.error;
            } else if (Array.isArray(error.error)) {
              errorMessage = error.error.map((e: any) => e.description || e.message).join('\n');
            } else if (error.error.errors) {
              // Handle ASP.NET Validation errors object { errors: { Field: ["error"] } }
              errorMessage = Object.values(error.error.errors).flat().join('\n');
            }
          } else if (error.message) {
            errorMessage = error.message;
          }

          // Return a modified error object that includes our extracted message
          return throwError(() => ({ ...error, message: errorMessage }));
        }),
      );
  }

  login(credentials: LoginDetails) {
    return this.http
      .post<LoginResponse>(`${API_BASE_URL}/login`, credentials, {
        context: new HttpContext().set(IS_PUBLIC, true),
      })
      .pipe(
        tap((response) => {
          this.accessToken = response.accessToken;
          this.currentUserSignal.set(response.user);
          localStorage.setItem('logged_in', 'true')
        }),
        catchError((error) => {
          let errorMessage = 'נא לוודא את פרטי ההתחברות.';

          if (error.error) {
            if (typeof error.error === 'string') {
              errorMessage = error.error;
            } else if (error.error.message) {
              errorMessage = error.error.message;
            } else if (error.error.error) {
              errorMessage = error.error.error;
            }
          } else if (error.message) {
            errorMessage = error.message;
          }

          return throwError(() => ({ ...error, message: errorMessage }));
        }),
      );
  }

  googleLogin(idToken: string) {
    return this.http
      .post<LoginResponse>(`${API_BASE_URL}/google-login`, { idToken }, {
        context: new HttpContext().set(IS_PUBLIC, true),
        withCredentials: true
      })
      .pipe(
        tap((response) => {
          this.accessToken = response.accessToken;
          this.currentUserSignal.set(response.user);
          localStorage.setItem('logged_in', 'true');
        }),
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  logout() {
    return this.http.post(`${API_BASE_URL}/logout`, {}).pipe(
      tap(() => {
        this.accessToken = null;
        this.currentUserSignal.set(null);
      }),
      finalize(() => {
        localStorage.removeItem('logged_in');
      })
    );
  }

  refreshToken() {
    return this.http
      .post<LoginResponse>(
        `${API_BASE_URL}/refresh-token`,
        {},
        {
          context: new HttpContext().set(IS_PUBLIC, true),
          withCredentials: true,
        },
      )
      .pipe(
        tap((response) => {
          this.accessToken = response.accessToken;
          this.currentUserSignal.set(response.user);
          console.log('new accessToken: ', this.accessToken);
          localStorage.setItem('logged_in', 'true');
        }),
      );
  }
}
