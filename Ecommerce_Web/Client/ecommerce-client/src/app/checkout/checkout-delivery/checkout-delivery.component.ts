import { Component, Input, OnInit } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { FormGroup } from '@angular/forms';
import { DeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm?:FormGroup
  deliveryMethods:DeliveryMethod[] = []
  constructor(private checkoutService:CheckoutService, private cartService:CartService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next:res=>{
        this.deliveryMethods = res
      }
    })
  }

  setShippingPrice(delveryMethod:DeliveryMethod){
    this.cartService.setShippingPrice(delveryMethod)
  }

}
