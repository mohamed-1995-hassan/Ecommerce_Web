import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { map } from 'rxjs/operators';
import { Order, OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl
  constructor(private http:HttpClient) { }

  getDeliveryMethods(){
    return this.http
              .get<DeliveryMethod[]>(this.baseUrl + 'order/deliveryMethods')
              .pipe(map(dm =>{
                return dm.sort((a, b)=> b.price - a.price)
              }))
  }

  createOrder(order:OrderToCreate){
    return this.http.post<Order>(this.baseUrl + 'order', order);
  }
}
