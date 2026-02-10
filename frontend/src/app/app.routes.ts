import { Routes } from '@angular/router';
import { HomePage } from './pages/home-page/home-page';
import { LoginPage } from './pages/auth/login/login';
import { RegisterPage } from './pages/auth/register/register';
import { NotFoundPage } from './pages/not-found/not-found';
import { MainLayout } from './core/layout/main-layout/main-layout';
import { guestGuard } from './core/guards/guest.guard';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        component: HomePage,
      },
    ],
  },
  {
    path: 'auth',
    canActivateChild: [guestGuard],
    children: [
      {
        path: 'login',
        component: LoginPage,
      },
      {
        path: 'register',
        component: RegisterPage,
      },
    ],
  },
  {
    path: '**',
    component: NotFoundPage,
  },
];
