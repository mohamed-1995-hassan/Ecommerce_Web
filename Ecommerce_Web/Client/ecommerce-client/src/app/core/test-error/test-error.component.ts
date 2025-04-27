import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

  baseUrl = environment.apiUrl;
  validationErrors:string[] = []


  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next:res=>console.log('res success'),
      error:err=>console.log('error happen')
    })
  }

  get500Error(){
    this.http.get(this.baseUrl +'buggy/server-error').subscribe({
      next:res=>console.log('res success'),
      error:err=>console.log('error happen')
    })
  }

  get400Error(){
    this.http.get(this.baseUrl +'buggy/bad-request').subscribe({
      next:res=>console.log('res success'),
      error:err=>console.log('error happen')
    })
  }

  get400ValidationError(){
    this.http.get(this.baseUrl +'product/fortyTwo').subscribe({
      next:res=>console.log('res success'),
      error:err=>{
        this.validationErrors = err.errors
      }
    })
  }

}
