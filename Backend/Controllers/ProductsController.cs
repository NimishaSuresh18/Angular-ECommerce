using System.Collections;
using Backend.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase{
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context=context;            
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetActionAsync(){
            var product = await _context.Products.ToListAsync();
            return product;
        }
        
        [HttpPost("Add")]
        public  ActionResult<IEnumerable<Products>> AddProducts(Products products){   
            var create =new Products{
                code=products.code,
                Name=products.Name,
                Price=products.Price,
                Details=products.Details,
                Image=products.Image,
                Quantity=products.Quantity
            };    
                _context.Products.Add(create);
                _context.SaveChanges();
                 return Ok(); 
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();
            var product =  _context.Products.FirstOrDefault(m => m.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Products>> Put(int id,[FromBody]Products productData)
        {
            // if (productData == null || productData.Id == 0)
            //     return BadRequest();

            var product = await _context.Products.FirstOrDefaultAsync(products=>products.Id==id);
            if (product == null)
                return NotFound();
            product.Name = productData.Name;
            product.code=productData.code;
            product.Image=productData.Image;
            product.Details = productData.Details;
            product.Price = productData.Price;
            product.Quantity=productData.Quantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Products.FindAsync(id);
            if (product == null) 
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("search")]
        public IActionResult SearchProducts(string search){
        var products=_context.Products.Where(p=>p.Name.ToLower().Contains(search.ToLower()));
        return Ok(products);
        }

      
    }
}