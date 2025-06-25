import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApproveLeaveRequestComponent } from './approve-leave-request/approve-leave-request.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EmployeeComponent } from './employee/employee.component';
import { NoPageFoundComponent } from './no-page-found/no-page-found.component';


const routes: Routes = [
  {path:'Dashboard',component: DashboardComponent},
  {path:'EmployeeList',component: EmployeeComponent},
  {path:'ApproveLeave',component: ApproveLeaveRequestComponent},
  {path:'',redirectTo: '/Dashboard', pathMatch:'full' },
  
  {path:'**', component: NoPageFoundComponent},



];

@NgModule({
  imports: [RouterModule.forRoot(routes),RouterModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
