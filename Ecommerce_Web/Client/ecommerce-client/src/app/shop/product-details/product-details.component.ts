import { Component, OnInit } from '@angular/core';
import { product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?:product
  constructor(private shopeService:ShopService,
              private activatedRoute:ActivatedRoute,
              private bcService:BreadcrumbService) 
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
      },
      error:err =>{
        console.log(err)
      }
    });
  }
}
