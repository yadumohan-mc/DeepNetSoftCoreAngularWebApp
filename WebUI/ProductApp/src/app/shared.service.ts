import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import{HttpClient} from'@angular/common/http';
import{Observable} from'rxjs'
import { CategoryProductViewModel } from './shared/category-product-view-model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl ="http://localhost:5000/api";
readonly GetUrl= "http://localhost:5000/api/category/";
ViewModel!:CategoryProductViewModel;
prevId:number[]=[];
  constructor(private http:HttpClient) { }
getCategoryList(){
   this.http.get<any>(this.APIUrl+'/category').toPromise()
  .then(res=> this.ViewModel = res as CategoryProductViewModel);
  }
  getCategoryListWithId(val:any){
    this.http.get<any>(this.APIUrl+'/category/'+val).toPromise()
  .then(res=> this.ViewModel = res as CategoryProductViewModel);
    }
}
