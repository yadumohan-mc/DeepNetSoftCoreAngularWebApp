import { Component,OnInit } from '@angular/core';
import { SharedService } from './shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(public service: SharedService){}
  title = 'ProductApp';
  ngOnInit(){
this.service.getCategoryList();
  }
  goToPrev(){
    this.service.getCategoryListWithId(this.service.prevId.pop());
  

  }
}
