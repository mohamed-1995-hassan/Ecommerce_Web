import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cart, CartItem, cartTotals } from '../shared/models/cart';
import { HttpClient } from '@angular/common/http';
import { product } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseUrl = environment.apiUrl;
  private cartSource = new BehaviorSubject<Cart | null>(null)
  cartSource$ = this.cartSource.asObservable();

  private cartTotalSource = new BehaviorSubject<cartTotals | null>(null)
  cartTotalSource$ = this.cartTotalSource.asObservable();

  constructor(private http:HttpClient) { }

  getCart(id:string){
    return this.http.get<Cart>(this.baseUrl + 'cart?cartId=' + id).subscribe({
      next:cart =>{
        this.cartSource.next(cart);
        this.calculateTotals();
      } 
    })
  }

  addCartItem(item:product | CartItem, quantity:number = 1){

    let cart = this.getCurrentCartValue() ?? this.createCart();
    this.http
        .post(this.baseUrl + 'cart', {cartId:cart.id, productId:item.id, quantity:quantity})
        .subscribe({
          next: ()=>{
            const isProduct = (item as product).productBrand !== undefined;
            let cartItem:CartItem

            if(isProduct){
              cartItem = this.mapProductToCartItem(item as product, quantity)
            }
            else{
              cartItem = item as CartItem
            }
            const existingIndex = cart.items.findIndex(item => item.id === cartItem.id);
            
            if (existingIndex !== -1)
              cart.items[existingIndex].quantity += quantity;    
            else 
              cart.items.push(cartItem);

            this.cartSource.next(cart);
            this.calculateTotals();
          }})     
  }

  removeCartItem(id:number, quantity:number = 1){

    let cart = this.getCurrentCartValue();
    if(!cart) return

    this.http
        .delete(this.baseUrl + 'cart?cartId=' + cart.id + ' &productId=' + id)
        .subscribe({
          next:()=>{
            const existingIndex = cart && cart.items.findIndex(item => item.id === id);
            
            if (typeof existingIndex === 'number' && existingIndex !== -1 && cart)
            {
              cart.items[existingIndex].quantity -= quantity;
              if(cart.items[existingIndex].quantity === 0)
                cart.items.splice(existingIndex, 1)

              if(cart.items.length > 0){
                this.cartSource.next(cart);
                this.calculateTotals();
              }
              else{
                this.cartSource.next(null)
                this.cartTotalSource.next(null)
                localStorage.removeItem('cart_id')
              }
            }
          }})
  }


  createCart(){
    let cart = new Cart();
    localStorage.setItem('cart_id', cart.id)
    return cart
  }

  getCurrentCartValue(){
    return this.cartSource.value;
  }

  private mapProductToCartItem(product:product, quantity:number = 1):CartItem{
    return {
      id: product.id,
      brand:product.name,
      pictureUrl:product.pictureUrl,
      price:product.price,
      productName:product.name,
      type:product.productType,
      quantity:quantity
    }
  }

  private calculateTotals(){
    const cart= this.getCurrentCartValue()
    if(!cart) return
    const shipping = 0;
    const subTotal = cart.items.reduce((a,b) =>(b.price * b.quantity) + a, 0)
    const total = shipping + subTotal

    this.cartTotalSource.next({shipping, subTotal, total})
  }
}
