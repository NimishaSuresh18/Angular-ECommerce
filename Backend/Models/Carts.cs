using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Carts{

    public int Id{get;set;}
    public int Product_Id{get;set;}
    public string Name{get;set;}
    public string Price{get;set;}
    public string Details{get;set;}
    public string code{get;set;}
    public string Image{get;set;}
    public string Quantity{get;set;}
}