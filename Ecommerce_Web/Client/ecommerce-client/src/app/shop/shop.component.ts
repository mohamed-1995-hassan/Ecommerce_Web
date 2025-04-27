import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?:ElementRef
  products:product[] = [];
  types:Type[] = []
  brands:Brand[] = []
  shoppingParams = new ShopParams();
  sortOptions = [
    {name:'alphabetical', value:'name'},
    {name:'price low to high', value:'priceAsc'},
    {name:'price high to low', value:'priceDsc'}
  ]
  totalCount = 0;
  constructor(private shopService:ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getTypes();
    this.getBrands();
  }

  getProducts(){
    this.shopService.getProducts(this.shoppingParams).subscribe({
      next:response => {
        this.products = response.data
        this.shoppingParams.pageIndex = response.pageIndex;
        this.shoppingParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      }
    })
  }

  getProduct(){
    
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next:response => this.brands = [{id:0,name:'All'}, ...response]
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next:response => this.types = [{id:0,name:'All'}, ...response]
    })
  }

  onBrandSelected(brandId:number){
    this.shoppingParams.brandId = brandId;
    this.shoppingParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.shoppingParams.typeId = typeId;
    this.shoppingParams.pageIndex = 1;
    this.getProducts();
  }

  onSortSelected(event:any){
    this.shoppingParams.sort = event.target.value
    this.getProducts();
  }

  onPageChanged(event:any){
    if(this.shoppingParams.pageIndex !== event){
      this.shoppingParams.pageIndex = event;
      this.getProducts();
    }
  }
  onSearch(){
    this.shoppingParams.search = this.searchTerm?.nativeElement.value;
    this.shoppingParams.pageIndex = 1;
    this.getProducts();
  }
  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value =  ''
    this.shoppingParams = new ShopParams();
    this.getProducts();
  }

}
