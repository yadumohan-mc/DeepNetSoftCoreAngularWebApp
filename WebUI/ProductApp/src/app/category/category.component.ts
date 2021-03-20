import { Component, OnInit } from '@angular/core';
import{ SharedService} from '../shared.service';
@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  constructor(public service:SharedService) { }
SubCategoryList:any=[];
  ngOnInit(): void {
  }
  getCategory(id:number){
    this.service.prevId.push(this.service.ViewModel.Category.CategoryID);
    console.log( this.service.prevId);
this.service.getCategoryListWithId(id);
  }
  

}
