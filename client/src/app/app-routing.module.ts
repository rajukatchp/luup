import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateWorkFlowComponent } from './create-workflow/create-workflow.component';
import { LoginComponent } from './login/login.component';
import { WorkFlowsComponent } from './notifications/workflows.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'notificatons', component: WorkFlowsComponent},
  { path: 'create-notification',component: CreateWorkFlowComponent},
  { path: 'edit-notification',component: CreateWorkFlowComponent},
  { path: '', redirectTo:'login', pathMatch:'full'},
  { path: '**', component: PageNotFoundComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
