
import { Routes, RouterModule } from '@angular/router';

// Component
import { LoginComponent } from '../login.Component';

const LOGIN_ROUTES: Routes = [
  {
    path: 'login',
    component: LoginComponent
  }
];

export let LoginRouterModule = RouterModule.forRoot(LOGIN_ROUTES, { useHash: false });