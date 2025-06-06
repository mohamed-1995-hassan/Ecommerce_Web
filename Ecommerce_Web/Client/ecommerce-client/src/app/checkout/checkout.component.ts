import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  stepperForm?:FormGroup = new FormGroup({
    addressForm: new FormGroup({
      firstName: new FormControl('',Validators.required),
      lastName: new FormControl('',Validators.required),
      street: new FormControl('',Validators.required),
      city: new FormControl('',Validators.required),
      zip: new FormControl('',Validators.required),
      state: new FormControl('',Validators.required),
    }),
    deliveryForm:new FormGroup({
      deliveryMethod:new FormControl('', Validators.required)
    }),
    paymentForm:new FormGroup({
      nameOfCard:new FormControl('', Validators.required)
    })
  })

  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
    this.getAddressFormValues()
  }

  getAddressFormValues(){
    this.accountService.getUserAddress().subscribe({
      next: address =>{
        address && this.stepperForm?.get('addressForm')?.patchValue(address)
      }
    })
  }

}
