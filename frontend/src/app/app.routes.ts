import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { LoginPage } from './pages/auth/login/login';
import { RegisterPage } from './pages/auth/register/register';

export const routes: Routes = [
  {
    path: '',
    component: HomePage,
  },
  {
    path: 'auth/login',
    component: LoginPage,
  },
  {
    path: 'auth/register',
    component: RegisterPage,
  },
];
