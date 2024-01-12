using System.Collections;
using Backend.Context;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("[controller]")]
public class PaymentGatewayController : ControllerBase{
    private AppDbContext _context;
    public PaymentGatewayController(AppDbContext context)
    {
        _context=context;
    }
    [HttpGet]
    public async Task<ActionResult> Get(){
        var product = await _context.Checkout.Include(order=>order.cart).ToListAsync();
        return Ok(product);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id){
        var Getbyid = await _context.Checkout.Include(order=>order.cart).FirstOrDefaultAsync(order=>order.Id==id);
        return Ok(Getbyid);
    }
    [HttpPost("Post")]
           public  ActionResult<IEnumerable<Checkout>> Add(Checkout products){  
            var create =new Checkout{
                Id=products.Id,
                UserId=products.UserId,
                Paymentmode=products.Paymentmode,
                Nameincard=products.Nameincard,
                Cardnumber=products.Cardnumber,
                Cvvnumber=products.Cvvnumber,
                Status="",
                Upicode=products.Upicode,
                Totalamount=products.Totalamount,
                Paid_date=DateTime.Now,
                Delivery_time=products.Delivery_time ,
                Address=products.Address,
                City=products.City,
                Pincode=products.Pincode,
                State=products.State ,
                cart=products.cart            
            };   
            _context.Checkout.Add(create);
            _context.SaveChanges();
        return Ok();
        }
    [HttpPatch]
    [Route("{id:int}/Patch")]
     public async Task<IActionResult>PatchCart(int id,[FromBody]JsonPatchDocument<Checkout> user){
        if(user==null && id<=0){
            return BadRequest();
        }
        var usercart = _context.Checkout.Include(myuser=>myuser.cart).Where(myuser=>myuser.Id==id).FirstOrDefault();
        if(usercart==null){
            return NotFound();
        }
        user.ApplyTo(usercart);
        await _context.SaveChangesAsync();
        return NoContent();
     }

}