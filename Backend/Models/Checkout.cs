using System.ComponentModel.DataAnnotations;

public class Checkout{
     [Key]
    public int Id{get;set;}
    
    public int UserId{get;set;}
    public string Address{get;set;}
    public string State{get;set;}
    public string City{get;set;}
    public string Pincode{get;set;}
    public string Paymentmode{get;set;}
    public string Nameincard{get;set;}
    public string Cardnumber{get;set;}
    public string Cvvnumber{get;set;}
    public string Upicode{get;set;}
    public int Totalamount{get;set;}
    public DateTime Paid_date{get;set;}
    public DateTime Delivery_time{get;set;}
    public string Status{get;set;}
    public List<Carts> cart{get;set;}
}