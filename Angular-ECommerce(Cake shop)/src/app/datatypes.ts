import { Time } from "@angular/common"

export interface Products{
  name:string,
  price:number,
  details:string,
  image:string,
  code:string,
  id:number
}
export interface cart{
 id:number,
 total:number

}
export interface Feedback{
  name:string,
  id:number,
  suggesstion:string,
  ratings:string,
  postedOn:string
}

export interface Payment{
      address:string,
      state:string,
      city:string,
      pincode:string,
      paymentmode:string,
      nameincard: string,
      cardnumber:string,
      status:string,
      cvvnumber:string,
      upicode: string,
      userId:number,
      Id:number,
      paid_date:Date,
      delivery_time:Time,
      cart:[]
}
export const requiredRole: string='Admin';
