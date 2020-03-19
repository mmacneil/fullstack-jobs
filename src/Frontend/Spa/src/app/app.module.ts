import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Injector } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/* Module Imports */
import { HomeModule } from './home/home.module';
import { JobsModule } from './jobs/jobs.module';
import { ShellModule } from './shell/shell.module';
import { SharedModule } from './shared/shared.module';
import { ApplicantModule } from './applicant/applicant.module';
import { AccountModule } from './account/account.module';
import { CoreModule } from './core/core.module';
import { EmployerModule } from './employer/employer.module';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { GraphQLModule } from './graphql.module';
import { AppInjector } from './app-injector.service';

@NgModule({
  declarations: [
    AppComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HomeModule,
    JobsModule,
    ApplicantModule,
    EmployerModule,
    AccountModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ShellModule,
    SharedModule,
    CoreModule,
    GraphQLModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  // https://devblogs.microsoft.com/premier-developer/angular-how-to-simplify-components-with-typescript-inheritance/#comment-116
  constructor(injector: Injector) { AppInjector.setInjector(injector); }
}
