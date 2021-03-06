import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms'
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { AuthManager } from './services/auth-manager';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthenticationInterceptor } from './services/intercepter';

import { AuthGuard } from './services/auth-guard';
import { LocalStorageSession } from './models/local-storage.credential';
import { AuthVariables } from './models/User';
import { Remote } from './services/http-client';
import { Router, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UnAuthGuard } from './services/unauthorized-guard';
import { AuthPageComponent } from './shared-components/auth.page.component/auth-page.component';
import { RegistrationPageComponent } from './shared-components/registration-page/registration-page.component';
import { HomePageComponent } from './shared-components/home-page/home-page.component';
import { CarMapComponent } from './shared-components/car-map/car-map.component';
import { ReserveManger } from './services/reserve-manager';
import { NavbarClientHeaderComponent } from './shared-components/navbar-client-header/navbar-client-header.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthPageComponent,
    RegistrationPageComponent,
    HomePageComponent,
    CarMapComponent,
    NavbarClientHeaderComponent
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
  },AuthGuard,UnAuthGuard,LocalStorageSession,AuthManager,AuthVariables,Remote,HttpClientModule,ReserveManger],
  bootstrap: [AppComponent]
})
export class AppModule { }