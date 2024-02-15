import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Task } from '../../Models/task';

@Component({
  selector: 'app-add-task',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './add-task.component.html',
  styleUrl: './add-task.component.css'
})
export class AddTaskComponent {
  task:Task;
  userId:any;
  errMsg="";
  today: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    console.log("HI");
    this.task=new Task();
    this.today = new Date().toISOString().split('T')[0];
    this.userId=localStorage.getItem("userId")  
  }
  Add(){
    console.log(this.errMsg)
    this.task.userId=this.userId;
    console.log(this.task);
    if(this.task.title===" " || this.task.title==null){
      this.errMsg="Please provide Title";
    }
    else if(this.task.description===" "&&this.task.description==null){
      this.errMsg="Please provide description"
    }
    else if(this.task.dueDate==undefined){
      this.errMsg="Provide valid Due date"
    }
    else{
      this.http.post('http://localhost:5201/api/Task/AddTask',this.task,this.httpOptions)
    .subscribe(
      (response:any)=>{
      console.log(response);
      this.router.navigateByUrl('user-dashboard/all-tasks');
    },
    (error: any) => {
      console.error('Error:', error.error);
    }
    );
    }
    console.log(this.task.dueDate);
    
  }

}
  

