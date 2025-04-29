
export interface CartItem{
    id:number,
    productName:string,
    price:number,
    quantity:number,
    pictureUrl:string,
    brand:string,
    type:string
}

export interface Cart{
    id:string,
    items:CartItem[]
}

export class Cart implements Cart{
    id = generateGuid();
    items:CartItem[] = []
}

export interface cartTotals{
    shipping:number,
    subTotal:number,
    total:number
}


function generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
      const r = Math.random() * 16 | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }