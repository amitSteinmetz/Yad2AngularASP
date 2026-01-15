import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
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

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentUserSignal = signal<User | null>(null);
  currentUser = this.currentUserSignal.asReadonly();
  private accessToken: string | null = null;

  constructor(private http: HttpClient) {}

  register(credentials: RegisterDetails) {
    return this.http
      .post<RegisterResponse>('https://localhost:7003/api/Auth/register', credentials)
      .pipe(
        tap((response) => {
          console.log(response.message);
        })
      )
      .subscribe();
  }

  login(credentials: LoginDetails) {
    return this.http.post<LoginResponse>('https://localhost:7003/api/Auth/login', credentials).pipe(
      tap((response) => {
        console.log(response);

        // 1. מעדכנים את הטוקן בזיכרון
        this.accessToken = response.accessToken;

        // 2. מעדכנים את הסיגנל רק בפרטי המשתמש (בלי הטוקן)
        this.currentUserSignal.set(response.user);
      }),
      catchError((error) => {
        console.log(error);
        return error;
      })
    );
  }

  // logout() {
  //   this.accessToken = null;
  //   this.currentUserSignal.set(null);
  // }

  // refreshToken() {
  //   return this.http.post<LoginResponse>('api/auth/refresh-token', {}).pipe(
  //     tap((response) => {
  //       this.accessToken = response.accessToken;
  //       this.currentUserSignal.set(response.user);
  //     })
  //   );
  // }
}
