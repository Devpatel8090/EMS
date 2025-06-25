import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeComponent } from './employee/employee.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatListModule } from '@angular/material/list';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NoPageFoundComponent } from './no-page-found/no-page-found.component';
import { ApproveLeaveRequestComponent } from './approve-leave-request/approve-leave-request.component';

import { ToastrModule } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

import { SidebarComponent } from './sidebar/sidebar.component';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import { EmployeeDialogueComponent } from './employee-dialogue/employee-dialogue.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {MatInputModule} from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';





@NgModule({
  declarations: [
    AppComponent,    
    DashboardComponent,
    NoPageFoundComponent,
    ApproveLeaveRequestComponent,
    SidebarComponent,
    EmployeeDialogueComponent, 
    
      
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatSelectModule,
    MatFormFieldModule,
    MatListModule,
    MatButtonModule,
    NgbModule,
    HttpClientModule,
    MatIconModule,
    FormsModule, 
    ReactiveFormsModule, 
    MatInputModule,
    MatPaginatorModule,
    MatDatepickerModule,
    MatNativeDateModule,
    DatePipe, 
    ToastrModule.forRoot(
      { positionClass:'top-left',
        closeButton:true
      }
    ) ,
      
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
