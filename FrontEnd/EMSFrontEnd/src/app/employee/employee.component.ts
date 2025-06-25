import { AfterViewInit, Component, ViewChild } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { NgFor, NgIf } from '@angular/common';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { EmployeeServicesService} from '../services/employee-services.service';
import {formatDate } from '@angular/common';
import { CommonModule } from '@angular/common';

import { ToastrService } from 'ngx-toastr';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatSort, MatSortModule} from '@angular/material/sort';

import {MatDialog, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import { EmployeeDialogueComponent } from '../employee-dialogue/employee-dialogue.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



export interface TableElement {
  firstName: string;
  employeeNumber: string;
  joinDate: string;
  email: string;
  lastName: string;
}

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  standalone: true,
  imports: [MatTableModule, NgFor, MatButtonModule, NgIf, MatIconModule,FormsModule,ReactiveFormsModule,CommonModule,MatPaginatorModule,MatSortModule,MatDialogModule],
  
})

export class EmployeeComponent implements AfterViewInit   {

  EmployeeTableData: any;
  IsClicked:boolean = false;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private employeeData:EmployeeServicesService,private toastr: ToastrService,private dialog:MatDialog){
    employeeData.getDetails().subscribe((data:any)=>{
      console.warn("data",data)
      this.EmployeeTableData = data;
      this.EmployeeTableData = new MatTableDataSource(data);
      this.EmployeeTableData.paginator = this.paginator;
      
  })}
  ngAfterViewInit() {
    this.EmployeeTableData.paginator = this.paginator;
    this.EmployeeTableData.sort = this.sort;
    this.IsClicked = true;
    console.log("afterview works");
    
  }

  successmsg(){  
    this.toastr.success("Deleted successfully",'Success')  
}  
 errorsmsg(){  
    this.toastr.error("Please try again letter",'Error')  
  
}  
GetEmployees(){
  this.employeeData.getDetails().subscribe((data)=>{
    this.EmployeeTableData = data;
  })
}
// to open dialogue 
openDialog(item:any): void {  
  const dialogRef = this.dialog.open(EmployeeDialogueComponent, {
   width:"500px",
   height:"600px",
   data:item,
   autoFocus:false
  });

  dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed');
    debugger;
    if(result){
      this.GetEmployees();
    }    
  });
}

// Delete Employee
  DeleteEmp(id:number){
    debugger;
    this.employeeData.deleteEmployee(Number(id)).subscribe((result) =>{
      console.warn(result);
      if(result){
        this.successmsg();
        this.GetEmployees();
      }
      else{
        this.errorsmsg();
      }      
    })   
  }
  columnsToDisplay: string[] = ['employeeNumber', 'firstName', 'lastName', 'email','joinDate','action'];  
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  expandedElement: TableElement | null | undefined;  
}
