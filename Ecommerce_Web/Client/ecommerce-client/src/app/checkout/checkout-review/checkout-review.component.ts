import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CartService } from 'src/app/cart/cart.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { Address } from 'src/app/shared/models/user';
import { Cart } from 'src/app/shared/models/cart';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {

  @Input() checkoutForm?:FormGroup
  
  constructor(private cartService:CartService,
              private checkoutService:CheckoutService,
              private toastr:ToastrService,
              private router:Router) { }

  ngOnInit(): void {
  }

  submitOrder(){
      const cart = this.cartService.getCurrentCartValue()
      if(!cart) return
  
      const orderToCreate = this.getOrderToCreate(cart)
      if(!orderToCreate) return
      this.checkoutService.createOrder(orderToCreate).subscribe({
        next:order=>{
          this.toastr.success('order created successfully')
          this.cartService.deleteLocalCart();
          const navigationExtras:NavigationExtras = {state:order}
          this.router.navigate(['/checkout/success'],navigationExtras)
        }
      })
    }
  
    private getOrderToCreate(cart:Cart){
      const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value
      const address = this.checkoutForm?.get('addressForm')?.value as Address
      if(!deliveryMethodId || !address) return
      return {
        cartId:cart.id,
        deliveryMethodId,
        address
      }
  
    }
}
