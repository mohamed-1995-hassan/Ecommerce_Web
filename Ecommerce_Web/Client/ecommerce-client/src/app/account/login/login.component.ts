import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  returnUrl:string

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{":;'?/>.<,])(?!.*\s).*$/)])
  })

  constructor(private accountService:AccountService, private router:Router, private activatedRoute:ActivatedRoute)
  {
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop'
  }

  ngOnInit(): void {
  }

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next:()=>{
        this.router.navigateByUrl(this.returnUrl)
      }
    })
  }
}
