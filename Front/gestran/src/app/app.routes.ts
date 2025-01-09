import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './security/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { NgModule } from '@angular/core';
import { LayoutComponent } from './pages/layout/layout.component';

export const routes: Routes = [
    { path: '', component: LayoutComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },

    // {
    //   path:'', component:LayoutComponent,
    //   canActivate:[AuthGuard],
    //   children:[
    //     {path:'', component:HomeComponent},
    //     {path:'clients', loadChildren:()=> import('./pages/clients/clients.module').then(m=>m.ClientsModule)}
    //   ]
    // },

    { path: '**', redirectTo: 'login' }

];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }