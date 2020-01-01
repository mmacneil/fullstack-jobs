import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/* Module Imports */
import { HomeModule } from './home/home.module';
import { ShellModule } from './shell/shell.module';
import { SharedModule } from './shared/shared.module';
import { AccountModule } from './account/account.module';
import { CoreModule } from './core/core.module';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HomeModule,
    AccountModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ShellModule,
    SharedModule,
    CoreModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
