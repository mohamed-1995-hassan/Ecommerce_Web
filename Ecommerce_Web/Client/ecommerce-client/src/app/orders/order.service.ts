import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from '../shared/models/order';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl:string = environment.apiUrl
  constructor(private http:HttpClient) { }

  getOrders(pageIndex:number, pageSize:number){


    const params = new HttpParams()
                      .set('pageIndex',pageIndex)
                      .set('pageSize', pageSize)

    return this.http.get<Pagination<Order>>(this.baseUrl + 'order',{params});
  }

  getOrder(id:number){
    return this.http.get<Order>(this.baseUrl + 'order/'+id);
  }
}
