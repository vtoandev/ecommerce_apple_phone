import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule }  from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//modules
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from 'src/app/modules/material.module';
import { AdminModule } from 'src/app/modules/admin/admin.module';
import { UserModule } from 'src/app/modules/user/user.module';
import { ShareModule }  from 'src/app/modules/share/share.module';
import { CookieModule } from 'ngx-cookie';
import { FacebookModule } from 'ngx-facebook';

//components
import { AppComponent } from './app.component';
//
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/fr';
registerLocaleData(localeFr, 'fr');
//
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RequestInterceptorService }  from 'src/app/services/request-interceptor.service';


@NgModule({
  declarations: [
    AppComponent
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    AdminModule,
    UserModule,
    ShareModule,
    CookieModule.forRoot(),
    // FacebookModule.forRoot(),
    // NgxChartsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptorService , multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
