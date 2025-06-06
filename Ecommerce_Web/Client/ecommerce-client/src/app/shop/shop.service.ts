import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { product } from '../shared/models/product';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:7298/api/';

  constructor(private http:HttpClient) { }

  getProducts(shopParams:ShopParams){

    let params = new HttpParams();
    if(shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId)
    if(shopParams.typeId > 0) params = params.append('typeId', shopParams.typeId)
    if(shopParams.search) params = params.append('name', shopParams.search)
    params = params.append('sort', shopParams.sort)
    params = params.append('pageIndex', shopParams.pageIndex)
    params = params.append('pageSize', shopParams.pageSize)

    return this.http.get<Pagination<product>>(this.baseUrl + 'Product',{params});
  }

  getProduct(id:number){
    return this.http.get<product>(this.baseUrl + 'product/' + id);
  }

  getTypes(){
    return this.http.get<Type[]>(this.baseUrl + 'Product/types');  
  }

  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'Product/brands');  
  }
}
