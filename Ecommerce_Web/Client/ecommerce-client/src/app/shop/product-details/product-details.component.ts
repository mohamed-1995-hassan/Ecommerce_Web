import { Component, OnInit } from '@angular/core';
import { product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { CartService } from 'src/app/cart/cart.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?:product
  quantity = 1
  quantityInCart = 0
  constructor(private shopeService:ShopService,
              private activatedRoute:ActivatedRoute,
              private bcService:BreadcrumbService,
              private cartService:CartService) 
              {
                this.bcService.set('@productDetails', ' ')
              }

  ngOnInit(): void {
    this.loadProducts();
  }
  loadProducts(){
    let id = this.activatedRoute.snapshot.paramMap.get('id')
    if(id) this.shopeService.getProduct(+id).subscribe({
      next: res =>{
        this.product = res
        this.bcService.set('@productDetails', this.product.name)
        this.cartService.cartSource$.pipe(take(1)).subscribe({
          next:cart =>{
            const item = cart?.items.find(x => id && x.id === +id)
            if(item){
              this.quantity = item.quantity
              this.quantityInCart = item.quantity
            }
          }
        })
      },
      error:err =>{
        console.log(err)
      }
    });
  }

  incrementQuantity(){
    this.quantity++
  }

  decrementQuantity(){
    this.quantity--
  }

  updateCart(){
    if(this.product){
      if(this.quantity > this.quantityInCart){
        const itemsToAdd = this.quantity - this.quantityInCart
        this.quantityInCart += itemsToAdd
        this.cartService.addCartItem(this.product, itemsToAdd)
      }
      else{
        const itemsToRemove =  this.quantityInCart - this.quantity
        this.quantityInCart -= itemsToRemove
        this.cartService.removeCartItem(this.product.id, itemsToRemove)
      }
    }

  }

  get buttonText(){
    return this.quantityInCart === 0 ? 'Add to cart' : 'update cart'
  }
}
