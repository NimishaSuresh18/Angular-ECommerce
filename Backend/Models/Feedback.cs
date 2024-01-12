using System.ComponentModel.DataAnnotations;

public class Feedback{
    [Key]
    public int Id{get;set;}
    public string Name{get;set;}
    public string Email{get;set;}
    public string Suggesstion{get;set;}
    public string Ratings{get;set;}
    public DateTime PostedOn{get;set;}

}