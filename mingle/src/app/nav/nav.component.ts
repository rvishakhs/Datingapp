import { Component,  } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,CommonModule,],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true } }]
})
export class NavComponent {

  model: any = {};
  loggedIn: boolean = false;
  dropdown: boolean = false;
  constructor(private http : HttpClient) {

  }

  login() {
    this.http.post('http://localhost:5284/api/Account/login',this.model).subscribe(response => {
          console.log(response);
          this.loggedIn = true;
         }, error => {
           console.log(error);
         }
         )
  }

  loggedOut() {
    this.loggedIn =false;
  }

  toggleDropdown() {
    this.dropdown = !this.dropdown;
  }


}
