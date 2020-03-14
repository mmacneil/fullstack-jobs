import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from '../../shell/shell.service';
import { AuthGuard } from '../../core/authentication/auth.guard';

import { RootComponent } from './root/root.component';
import { BasicsComponent } from './basics/basics.component';
import { DescriptionComponent } from './description/description.component';
import { ApplicationComponent } from './application/application.component';
import { TagsComponent } from './tags/tags.component';
import { PublishComponent } from './publish/publish.component';

const routes: Routes = [
    Shell.childRoutes([
        {
            path: 'employer/job/:id/manage',
            component: RootComponent,
            canActivate: [AuthGuard],

            children: [
                { path: 'basics', component: BasicsComponent },
                { path: 'description', component: DescriptionComponent },
                { path: 'application', component: ApplicationComponent },
                { path: 'tags', component: TagsComponent },
                { path: 'publish', component: PublishComponent }
            ]
        }])
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: []
})
export class JobManagementRoutingModule { }