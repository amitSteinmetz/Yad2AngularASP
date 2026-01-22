import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { User } from '../models/user.model';
import { LoginDetails } from '../models/loginDetails.model';
import { RegisterDetails } from '../models/registerDetails.model';

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

  constructor(private http: HttpClient) {}

  register(credentials: RegisterDetails) {
    return this.http.post<RegisterResponse>(`${API_BASE_URL}/register`, credentials).pipe(
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
    return this.http.post<LoginResponse>(`${API_BASE_URL}/login`, credentials).pipe(
      tap((response) => {
        this.accessToken = response.accessToken;
        this.currentUserSignal.set(response.user);
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

  logout() {
    return this.http.post(`${API_BASE_URL}/logout`, {}).pipe(
      finalize(() => {
        this.accessToken = null;
        this.currentUserSignal.set(null);
      }),
    );
  }

  // refreshToken() {
  //   return this.http.post<LoginResponse>('api/auth/refresh-token', {}).pipe(
  //     tap((response) => {
  //       this.accessToken = response.accessToken;
  //       this.currentUserSignal.set(response.user);
  //     })
  //   );
  // }
}
