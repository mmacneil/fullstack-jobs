import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/* Module Imports */
import { HomeModule } from './home/home.module';
import { ShellModule } from './shell/shell.module';
import { SharedModule } from './shared/shared.module';
import { AccountModule } from './account/account.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HomeModule,
    AccountModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ShellModule,
    SharedModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
