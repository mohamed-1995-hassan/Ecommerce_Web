import { Component, OnInit } from '@angular/core';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'ecommerce-client';

  constructor(private cartService:CartService){}
  ngOnInit(): void {

    const basketId = localStorage.getItem('cart_id');
    if(basketId) this.cartService.getCart(basketId);
    
  }
}
