import { Component, OnInit } from '@angular/core';
import { OrderService } from './order.service';
import { Order } from '../shared/models/order';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  orders:Order[] = []
  pageSize = 7;
  pageIndex = 1;
  totalCount = 0;

  constructor(private orderService:OrderService) { }

  ngOnInit(): void {
    this.loadOrders();
  }


  loadOrders(){
    this.orderService.getOrders(this.pageIndex, this.pageSize).subscribe({
      next:orders =>{
        this.orders = orders.data
        this.totalCount = orders.count
        this.pageIndex = orders.pageIndex
      } 
    });
  }

  onPageChanged(event:any){
    if(this.pageIndex !== event){
      this.pageIndex = event;
      this.loadOrders();
    }
  }
}
