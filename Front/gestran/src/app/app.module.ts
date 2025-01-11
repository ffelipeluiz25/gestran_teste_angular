import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './security/auth.interceptor';
import { AuthGuard } from './security/auth.guard';
import { LoginComponent } from './pages/login/login.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { AppRoutingModule } from './app.routes';
import { HomeComponent } from './pages/home/home.component';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { ChecklistComponent } from './pages/checklist/list/checklist.component';
import { RouterModule } from '@angular/router';
import { ChecklistEditComponent } from './pages/checklist/edit/checklist-edit.component';
import { ChecklistNewComponent } from './pages/checklist/new/checklist-new.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LayoutComponent,
    HomeComponent,
    ChecklistComponent,
    ChecklistEditComponent,
    ChecklistNewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([]),
    NgModule
  ],
  providers: [
    provideHttpClient(
      withInterceptorsFromDi(),
      withFetch()
    ),
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true

    }
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
