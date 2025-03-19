import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  isLoggedIn = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
   
  }
  login() {
    // Simulate a login process
    this.isLoggedIn = true;
  }

  logout() {
    this.isLoggedIn = false;
  }
  
}
