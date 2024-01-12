using Backend.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 [Route("api/[controller]")]
    [ApiController]
public class FeedbackController: ControllerBase{
    private AppDbContext _context;
    public FeedbackController(AppDbContext context)
    {
        _context=context;
    }
   [HttpGet]
     public async Task<ActionResult<Feedback>> GetAllFeedback(){
        return Ok(await _context.Feedback.ToListAsync());
     } 
     [HttpPost("Create")]
     public ActionResult<IEnumerable<Feedback>>PostCart(Feedback user){
        var feedback = new Feedback{
            Id=user.Id,
            Name=user.Name,
            Email=user.Email,          
            Suggesstion=user.Suggesstion,
            Ratings=user.Ratings,
            PostedOn = DateTime.Now
        };
        _context.Feedback.Add(feedback);
        _context.SaveChanges();
        return Ok();
     }
      [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Feedback.FindAsync(id);
            if (product == null) 
                return NotFound();
            _context.Feedback.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
}
