import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../../Models/task';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule],
  templateUrl: './edit-task.component.html',
  styleUrl: './edit-task.component.css'
})
export class EditTaskComponent {
  task:Task;
  taskId:any;
  userId:any;
  today:string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private activateRoute: ActivatedRoute,private http:HttpClient){
    this.task=new Task();
    this.userId=localStorage.getItem("userId");
    this.today = new Date().toISOString().split('T')[0];
    this.activateRoute.params.subscribe((p) => (this.taskId = p['tid']));
    this.http
      .get<Task>(
        'http://localhost:5201/api/Task/GetTaskById/' + this.taskId,this.httpOptions
      )
      .subscribe((response) => {
        console.log(response);
        this.task=response;
      });
  }
  Edit(){
    this.task.userId=this.userId;
    this.task.taskId=this.taskId;
    console.log(this.task);
    this.http.put('http://localhost:5201/api/Task/EditTask',this.task,this.httpOptions)
    .subscribe(
      (response:any)=>{
        this.router.navigateByUrl('user-dashboard/all-tasks');
      console.log(response);
    },
    );
  }

}
