import { Component } from '@angular/core';

@Component({
  selector: 'app-add-data-component',
  templateUrl: './add-data.component.html'
})
export class AddDataComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
