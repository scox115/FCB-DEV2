import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../Models/user';

@Component({
  selector: 'app-home',
  templateUrl: './add-data.component.html',
  styleUrls: ['./add-data.component.css'],
})
export class AddDataComponent {
  public user: User = new User();  
  working = false;
  response: string | null = null;

  constructor (    
    private http: HttpClient,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  onSubmit(): void {
    this.working = true;
    this.http.post(this.baseUrl + 'data', this.user).subscribe(result => {
      if (result > 0) {
        this.response = "Add user was successful";
      } else {
        this.working = false;
      }
    }, error => this.response = "Add user failed with error: " + error)
  }

  okButton() {
    this.user = new User();
    this.working = false;
    this.response = null;   
  }
}
