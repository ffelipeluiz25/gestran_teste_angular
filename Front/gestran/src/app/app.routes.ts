import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './security/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { NgModule } from '@angular/core';
import { LayoutComponent } from './pages/layout/layout.component';
import { HomeComponent } from './pages/home/home.component';
import { ChecklistNewComponent } from './pages/checklist/new/checklist-new.component';
import { ChecklistEditComponent } from './pages/checklist/edit/checklist-edit.component';
import { ChecklistComponent } from './pages/checklist/list/checklist.component';
import { ItemComponent } from './pages/item/list/item.component';
import { ItemNewComponent } from './pages/item/new/item-new.component';

export const routes: Routes = [
    { path: '', component: LayoutComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    {
        path: '', component: LayoutComponent,
        canActivate: [AuthGuard],
        children: [
            { path: 'home', component: HomeComponent },
            { path: 'checklist', component: ChecklistComponent },
            { path: 'checklist/new', component: ChecklistNewComponent },
            { path: 'checklist/edit/:id', component: ChecklistEditComponent },
            { path: 'item', component: ItemComponent },
            { path: 'item/new', component: ItemNewComponent },
        ]
    },
    { path: '**', redirectTo: 'login' }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }