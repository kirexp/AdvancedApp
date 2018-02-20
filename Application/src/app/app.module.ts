import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms'
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { AuthManager } from './services/auth-manager';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthenticationInterceptor } from './services/intercepter';
import { AuthPageComponent } from './shared-components/auth-page.component';
import { AuthGuard } from './services/auth-guard';
import { LocalStorageSession } from './models/local-storage.credential';
import { AuthVariables } from './models/User';
import { Remote } from './services/http-client';
import { Router, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    AuthPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule.forRoot()
  ],
  providers: [
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptor,
    multi: true,
  },AuthGuard,LocalStorageSession,AuthManager,AuthVariables,Remote,HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }