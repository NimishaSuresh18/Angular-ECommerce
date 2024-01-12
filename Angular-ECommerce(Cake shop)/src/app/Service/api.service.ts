import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl: string = "https://localhost:7112/api/User/";
  private apiUrl: string = "https://localhost:7112/api/Product";
  private feedbackUrl:string = "https://localhost:7112/api/Feedback";
  private paymentUrl:string ="https://localhost:7112/api/Checkout/";
  private checkoutUrl:string="https://localhost:7112/PaymentGateway";

constructor(private http:HttpClient) { }
getUsers()
{
   return this.http.get<any>(this.baseUrl);
}
 getUserbyId(id:any)
    {
      return this.http.get(this.baseUrl + id)
    }

//product crud
getProductsList(){
 return this.http.get<any[]>(this.apiUrl);
}
getProductsbyId(id:number):Observable<any>{
  return this.http.get<any>(`${this.apiUrl}/${id}`);
 }

addProducts(data:any) :Observable<any>{
  return this.http.post<any>(`${this.apiUrl}/Add`,data)
}
updateProducts(id:number,data:FormData):Observable<any>{
  return this.http.put<any>(`${this.apiUrl}/${id}`,data);
}
deleteProducts(id:number):Observable<any>{
  return this.http.delete<any>(`${this.apiUrl}/${id}`)
}
search(search:string):Observable<any[]>{
  return this.http.get<any[]>(`https://localhost:7112/api/Product/search?search=${search}`);
}
//feedback
getfeedback(){
  return this.http.get<any[]>(this.feedbackUrl);
}
Postfeedback(data:any): Observable<any>{
  return this.http.post(`${this.feedbackUrl}/Create`,data)
}
deletefeedback(id:number):Observable<any>{
  return this.http.delete<any>(`${this.feedbackUrl}/${id}`)
}

//payment
getPayment(){
   return this.http.get<any[]>(this.paymentUrl);
}
Postpayment(data:any): Observable<any>{
  return this.http.post<any>(`${this.paymentUrl}Add`,data)
}

//Checkout
getOrder(){
  return this.http.get<any[]>(this.checkoutUrl);
}
PostOrder(data:any):Observable<any>{
  return this.http.post<any>("https://localhost:7112/PaymentGateway/Post",data);
}
}
