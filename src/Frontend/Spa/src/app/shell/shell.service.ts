import { Routes, Route } from '@angular/router';
import { ShellComponent } from './shell.component';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

/**
 * Provides helper methods to create routes.
 */
export class Shell {

  /**
   * Creates routes using the shell component.
   * @param routes The routes to add.
   * @return {Route} The new route using shell as the base.
   */
  static childRoutes(routes: Routes): Route {
    return {
      path: '',
      component: ShellComponent,
      children: routes,
      data: { reuse: true }
    };
  }
}