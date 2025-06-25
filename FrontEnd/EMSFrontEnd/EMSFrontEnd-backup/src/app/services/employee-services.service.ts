import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServicesService {
  private url = "https://localhost:7277/api/Employee/"
  constructor(private http:HttpClient) { }
  getDetails(){
    return this.http.get(this.url + "GetAllEmployees");
  }
  deleteEmployee(id:number){
    return this.http.delete(this.url + `DeleteEmployee?id=${id}`)
  }
  UpdateDetails(data:any){
    debugger;
    return this.http.post(this.url + "UpdateEmployee",data)
  }
}
