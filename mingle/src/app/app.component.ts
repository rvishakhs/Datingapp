import { NgClass, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { FormsModule, NgForm } from '@angular/forms';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, HttpClientModule, NgFor, NavComponent, FormsModule,]
})
export class AppComponent implements OnInit {
  title = 'mingle';
  users: any = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('http://localhost:5284/api/users').subscribe( response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
      console.log(this.users);
  }
  
  
}
