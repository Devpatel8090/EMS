import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeServicesService } from '../services/employee-services.service';

export interface Department {
  id:number,
  name:string
}
@Component({
  selector: 'app-employee-dialogue',
  templateUrl: './employee-dialogue.component.html',
  styleUrls: ['./employee-dialogue.component.scss']
})
export class EmployeeDialogueComponent implements OnInit {
  updateForm!:FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,public dialogRef: MatDialogRef<EmployeeDialogueComponent>,private employeeDataService:EmployeeServicesService) { }
  ngOnInit(): void {
    console.log(this.data);
    this.EmployeeDetails();
    this.setValue();
  }
  // setting values in dialogue input field
  setValue() {
    this.updateForm.patchValue({
      Id :this.data.Id?this.data.Id:+'',
      FirstName:this.data.FirstName?this.data.FirstName:'',
      LastName:this.data.LastName?this.data.LastName:'',
      Email:this.data.Email?this.data.Email:'',
      Salary:this.data.Salary?this.data.Salary:'',
      DepartmentId:this.data.DepartmentId?this.data.DepartmentId:'',
      JoinDate:this.data.JoinDate?this.data.JoinDate:''
  })
  }
  close(data?:any) {
    this.dialogRef.close(data);
  }
  EmployeeDetails(){
    this.updateForm = new FormGroup({
      Id:new FormControl(+''),
      FirstName:new FormControl(''),
      LastName:new FormControl(''),
      Email:new FormControl(''),
      Salary:new FormControl(''),
      DepartmentId:new FormControl(''),
      JoinDate:new FormControl('')
  });
}

UpdateEmployeeDetails(){

  console.warn(this.updateForm.value);
  if(this.updateForm.value.Id == '') {
    this.updateForm.value.Id = 0;
  }
  this.employeeDataService.UpdateDetails(this.updateForm.value).subscribe((result:any) =>{     
    console.log(result);
    if(result){
      this.close(true);      
    }
    else{
      this.close(false);
    }    
  },
  (error:any)=>{
    this.close(false);
  }   
  );
}
}
