import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '../../Models/login';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule,HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  login:Login;
  user:any;
  errMsg: string = '';
  httpResponse: any;
 userInput: string="";


  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token'),
    }),
  };
  constructor(private router:Router,private http:HttpClient){
    this.login=new Login();
   }


  onSubmit(): void {
    console.log(this.login);
          this.http.post('http://localhost:58502/api/User/Validate', this.login)
            .subscribe((response) => {
              this.httpResponse = response;
              console.log(this.httpResponse);
                localStorage.setItem('token', this.httpResponse.token);
                localStorage.setItem('userId', this.httpResponse.userId);
                localStorage.setItem('role', this.httpResponse.role);
                console.log(this.httpResponse.isAdmin);
                if(this.httpResponse.Role=='Admin'){
                  this.router.navigateByUrl('admin-dashboard');
                }
                else{
                  this.router.navigateByUrl('user-dashboard');
                }
            });

          console.log(this.login.email);
          console.log(this.login.password);
        }

  onReset(): void {
    console.log("Reset")
    this.login = new Login(); // Reset the login object
    this.userInput = ""; // Reset the userInput
    this.errMsg = '';
  }
  

  redirectToRegister() {
    this.router.navigateByUrl('register');
  }
}
