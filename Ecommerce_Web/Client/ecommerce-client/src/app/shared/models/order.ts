import { Address } from "./user"

export interface OrderToCreate{
    cartId:string,
    deliveryMethodId:number,
    address:Address
}

export interface OrderItem{
    productId:number,
    productName:string,
    pictureUrl:string,
    price:number,
    quantity:number
}

export interface Order{
    id:number,
    buyerEmail:string,
    orderDate:string,
    shippingAddress:Address,
    deliveryMethod:string,
    shippingPrice:number,
    orderItems:OrderItem[],
    subTotal:number,
    total:number,
    status:string
}