import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../Models/user';

@Component({
  selector: 'app-list-data',
  templateUrl: './list-data.component.html'
})
export class ListDataComponent {

  public users: User[] = null!;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<User[]>(baseUrl + 'data').subscribe(result => {
      this.users = result as User[];
    }, error => console.error(error));
  }
}


