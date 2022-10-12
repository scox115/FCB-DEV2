import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { User } from '../Models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  public user: User = new User();
  response = {};

  constructor (    
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  onSubmit(): void {
    
    this.http.post(this.baseUrl + 'data', this.user).subscribe(result => {
      this.response = result;
      }, error => console.error(error));   
   
  }
}
