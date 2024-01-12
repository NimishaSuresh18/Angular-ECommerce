using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Backend.Context;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext authContext)
        {
            _authContext=authContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj){
            if(userObj == null)
                return BadRequest();
                var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);
                if(user==null)
                return NotFound(new{Message="User Not Found!"});
                if(!PasswordHasher.VerifyPassword(userObj.Password,user.Password)){
                    return BadRequest(new {Message="Password is incorrect"});
                }
                user.Token = CreateJwt(user);
             
             return Ok(new{
                Token=user.Token,
                Message="Login Success!"
             });
            
        }
         [HttpPost("register")]
     public async Task<IActionResult> RegisterUser([FromBody] User userObj){
        if(userObj==null)
       return BadRequest();
       //username
       if(await CheckUserNameExistAsync(userObj.Username))
       return BadRequest(new{Message = "UserName Already Exist!"});
       //email
       if(await CheckEmailExistAsync(userObj.Email))
       return BadRequest(new{Message = " Email Already Exist!"});
       //password 
       var pass = CheckPasswordStrength(userObj.Password);
       if(!string.IsNullOrEmpty(pass))
       return BadRequest(new{Message=pass.ToString()});

       userObj.Password=PasswordHasher.HashPassword(userObj.Password);
       userObj.Role="User";
       userObj.Token="";
    //    userObj.carts=userObj.carts;
        await _authContext.Users.AddAsync(userObj);
        await _authContext.SaveChangesAsync();
         return Ok(new{
                Message="User Registered!"
             });
     }
     private Task<bool> CheckUserNameExistAsync(string username)
     => _authContext.Users.AnyAsync(x=> x.Username==username);
      private Task<bool> CheckEmailExistAsync(string email)
     => _authContext.Users.AnyAsync(x=> x.Email==email);
     private string CheckPasswordStrength(string password){
        StringBuilder stringBuilder = new StringBuilder();
        if(password.Length<8)
            stringBuilder.Append("Minimum password length should be 8"+ Environment.NewLine);
            if(!(Regex.IsMatch(password,"[a-z]") && Regex.IsMatch(password,"[A-Z]") && Regex.IsMatch(password,"[0-9]")))
            stringBuilder.Append("Password should be alphanumeric"+ Environment.NewLine);
            if(!(Regex.IsMatch(password,"[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]")))
            stringBuilder.Append("Password should contain special chars"+Environment.NewLine);
             return stringBuilder.ToString();      
     }
     private string CreateJwt(User user){
        var jwtTokenHandler= new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("veryverysecret.....");
        var identity= new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.Firstname}{user.Lastname}"),
            new Claim(ClaimTypes.Email,user.Email),
        
        });
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor  = new SecurityTokenDescriptor{
            Subject = identity,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);

     }
    
     [HttpGet]
     public async Task<ActionResult<User>> GetAllUsers(){
        return Ok(await _authContext.Users.ToListAsync());
     } 

      [HttpPatch]
     [Route("{id:int}/Patch")]
     public async Task<IActionResult>PatchCart(int id,[FromBody]JsonPatchDocument<User> user){
        if(user==null && id<=0){
            return BadRequest();
        }
        var usercart = _authContext.Users.Include(myuser=>myuser.carts).Where(myuser=>myuser.Id==id).FirstOrDefault();
        if(usercart==null){
            return NotFound();
        }
        user.ApplyTo(usercart);
        await _authContext.SaveChangesAsync();
        return NoContent();
     }
     [HttpPut("{id}")]
     public async Task<IActionResult>UpdateCart(int id,User user){
        if(id!=user.Id){
            return BadRequest();
        }
        var cart =_authContext.Users.Include(myuser=>myuser.carts).FirstOrDefault(car=>car.Id==id);
         if (cart == null)
                return NotFound();
                cart.Firstname=user.Firstname;
                cart.Lastname=user.Lastname;
                cart.Username=user.Username;
                cart.Password=user.Password;
                cart.Role=user.Role;
                cart.Token=user.Token;
                cart.Email=user.Email;
                cart.carts=user.carts;
            await _authContext.SaveChangesAsync();
            return Ok();
     }
     [HttpPost]
     public ActionResult<IEnumerable<User>>PostCart(User user){
        var cart = new User{
            Id=user.Id,
            Firstname=user.Firstname,
            Lastname=user.Lastname,
            Email=user.Email,
            Username=user.Username,
            Password=user.Password,
            Role=user.Role,
            Token=user.Token,
            carts = user.carts
        };
        _authContext.Users.Add(cart);
        _authContext.SaveChanges();
        return CreatedAtAction("Getuser",new{Id=user.Id},user);
     }
      [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _authContext.Carts.FindAsync(id);
            if (product == null) 
                return NotFound();
            _authContext.Carts.Remove(product);
            await _authContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            var product =  _authContext.Users.Include(myuser=>myuser.carts).FirstOrDefault(userid=>userid.Id==id);             
            return product;
        }
        
        [HttpGet("{email}")]
        
        public  ActionResult<User> GetEmail(string email){
          var user =  _authContext.Users.Include(user => user.carts).FirstOrDefault(user => user.Email == email);
          if(user == null){
            return NotFound();
          }
          return user;
        }
}
    }
