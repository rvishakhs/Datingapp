import { Component, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  model: any = {};

  constructor(private http : HttpClient) {

  }
  // login() {
  //   this.accountService.login(this.model).subscribe(response => {
  //     console.log(response);
  //   }, error => {
  //     console.log(error);
  //   }
  //   )
  // }

  login() {
    this.http.post('http://localhost:5284/api/Account/login',this.model).subscribe(response => {
          console.log(response);
         }, error => {
           console.log(error);
         }
         )
  }

}
