import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl:string = environment.apiUrl
  constructor(private http:HttpClient) { }

  getOrders(){
    return this.http.get<Order[]>(this.baseUrl + 'order');
  }

  getOrder(id:number){
    return this.http.get<Order>(this.baseUrl + 'order/'+id);
  }
}
