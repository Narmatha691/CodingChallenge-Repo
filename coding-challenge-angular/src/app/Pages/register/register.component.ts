import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../../Models/user';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  user:User;
  errMsg:string='';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    this.user=new User();
  }
  
   onSubmit(): void {
    console.log(JSON.stringify(this.user));
    console.log(this.user);
    this.user.role="User";
    this.http.post('http://localhost:58502/api/User/Register',this.user,this.httpOptions)
    .subscribe(
      (response:any)=>{
      console.log(response);
      this.router.navigateByUrl('login');
    },
    (error: any) => {
      // Handle error here
      console.error('Error:', error.error);
      this.errMsg=error.error;
    }
    );
  }

  onReset(form: NgForm): void {
    this.errMsg='';
    form.reset();
  }
  redirectToLogin() {
    this.router.navigateByUrl('login');
  }
}
