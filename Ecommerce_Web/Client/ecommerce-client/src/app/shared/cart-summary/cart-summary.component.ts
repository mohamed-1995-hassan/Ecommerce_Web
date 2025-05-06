import { Component, Input, OnInit } from '@angular/core';
import { CartItem } from '../models/cart';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.scss']
})
export class CartSummaryComponent implements OnInit {

  @Input() isCart = true

  constructor(public cartService:CartService) { }

  ngOnInit(): void {
  }

}
