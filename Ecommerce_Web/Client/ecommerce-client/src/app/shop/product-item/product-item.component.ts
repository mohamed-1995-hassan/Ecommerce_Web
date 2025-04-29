import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';
import { product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {

  @Input() product?:product

  constructor(private cartService:CartService) { }

  ngOnInit(): void {
  }

  addItemToBasket(){
    this.product && this.cartService.addCartItem(this.product);
  }
}
