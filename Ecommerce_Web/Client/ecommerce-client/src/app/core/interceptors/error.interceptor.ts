import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { EMPTY, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router, private toaster:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error:HttpErrorResponse)=>{
        if(error){
          console.log(error.status)
        if(error.status === 400){
          if(error.error.errors){
            throw error.error
          }
          else{
            this.toaster.error(error.error.message, error.status.toString())
          } 
        }
        if(error.status === 401){
          this.toaster.error(error.error.message, error.status.toString())
        }
        if(error.status === 404 ){
          this.router.navigateByUrl('/not-found');
        }
        if(error.status === 500){
          
          this.router.navigateByUrl('/server-error', { state:{ error:error.error } })
        }
        }
        return throwError(() => new Error(error.message))
      })
    );
  }
}
