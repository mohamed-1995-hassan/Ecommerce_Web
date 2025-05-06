import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss']
})
export class CheckoutSuccessComponent implements OnInit {
  state:any;
  viewOrder:string = ''
  constructor(private router: Router)
  {
    let navigation = this.router.getCurrentNavigation()
    this.state = navigation?.extras?.state;
    if(this.state)
      this.viewOrder = "View your order"
    else
      this.viewOrder = 'View Orders'
  }

  ngOnInit(): void {
  }
  
  navigateToOrders(){
    if(this.state)
      this.router.navigateByUrl('/orders/'+ this.state.id)
    else
      this.router.navigateByUrl('/orders')
  }
}
