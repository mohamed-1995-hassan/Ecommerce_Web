import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { OrderRoutingModule } from './order-routing.module';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [
  
    OrderComponent,
       OrderDetailedComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    SharedModule
  ]
})
export class OrdersModule { }
