import { Component, OnInit } from '@angular/core';
import { CartService } from './cart/cart.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'ecommerce-client';

  constructor(private cartService:CartService, private accountService:AccountService){}
  ngOnInit(): void {
    this.loadBasket()
    this.loadCurrentUser()
  }

  loadCurrentUser(){
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe()
  }

  loadBasket(){
    const basketId = localStorage.getItem('cart_id');
    if(basketId) this.cartService.getCart(basketId);
  }
}
