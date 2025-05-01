import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidator, AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { debounce, debounceTime, finalize, map, switchMap, take } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  errors:string[] | null = null;


  registerForm = new FormGroup({
      displayName: new FormControl('',Validators.required),
      email: new FormControl('', [Validators.required, Validators.email],[this.validateEmailNotTaken()]),
      password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{":;'?/>.<,])(?!.*\s).*$/)])
    })

  constructor(private accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe({
      next:()=>{
        this.router.navigateByUrl('/shop')
      },
      error:err=>{
        this.errors = err.errors
      }
    })
  }

  validateEmailNotTaken():AsyncValidatorFn{

    return (control:AbstractControl) =>{
      return control.valueChanges.pipe(debounceTime(1000), take(1), switchMap(()=>{
        return this.accountService.checkEmailExists(control.value).pipe(
          map(res=>{
            return res ? {emailExists:true}:null
          }),
          finalize(()=>control.markAsTouched())
        )
      }))
      
    }
  }

}
