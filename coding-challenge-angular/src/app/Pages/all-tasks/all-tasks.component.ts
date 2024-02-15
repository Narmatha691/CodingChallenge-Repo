import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Task } from '../../Models/task';

@Component({
  selector: 'app-all-tasks',
  standalone: true,
  imports: [CommonModule,HttpClientModule,FormsModule,RouterLink,RouterOutlet],
  templateUrl: './all-tasks.component.html',
  styleUrl: './all-tasks.component.css'
})
export class AllTasksComponent {
task:Task;
tasks:Task[]=[];
userId:any;
httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Bearer ' + localStorage.getItem('token'),
  }),
};
constructor(private router:Router,private http:HttpClient){
  console.log("HI");
  this.task=new Task();
  this.userId=localStorage.getItem("userId")
  this.getallTasks();

}
getallTasks(){
  this.http
  .get<Task[]>('http://localhost:5201/api/Task/ListTasksByUserId/'+this.userId,this.httpOptions)
  .subscribe((response)=>{
    this.tasks=response;
    console.log(this.tasks);
  })
}
edit(taskId:any){
  console.log(taskId)
  this.router.navigateByUrl('user-dashboard/edit-task/'+taskId)
}

delete(taskId:any){
  console.log(taskId);
  this.http
    .delete('http://localhost:5201/api/Task/DeleteTask/'+taskId,this.httpOptions)
    .subscribe((response)=>{
      console.log(response);
      this.getallTasks();
    })
}
markAsRead(taskId: any) {
  console.log(taskId);
  this.http.put('http://localhost:5201/api/Task/MarkAsRead/' + taskId, taskId,this.httpOptions)
    .subscribe(() => {
      this.getallTasks();
    });
}
}
