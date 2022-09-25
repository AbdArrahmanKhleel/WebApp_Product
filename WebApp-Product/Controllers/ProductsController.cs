
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Product.Data;
using WebApp_Product.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]

    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public ProductsController(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_ctx.Products.Include(x => x.Category).OrderBy(v=>v.Name).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var Rusult = _ctx.Products.Include(x => x.Category).FirstOrDefault(b=>b.Id==id);
                return Ok(Rusult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product productMdel)
        {
            try
            {
                if (productMdel != null)
                {
                _ctx.Products.Add(productMdel);
                _ctx.SaveChanges();
                return Ok(productMdel);

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product model)
        {
            try
            {
                var Rusult = _ctx.Products.FirstOrDefault(b => b.Id == id);
                if (Rusult != null)
                {
                    Rusult.CategoryId=model.CategoryId;

                    Rusult.Name=model.Name;
                    Rusult.Price = model.Price;
                    Rusult.Quantity = model.Quantity;
                    Rusult.Descount=model.Descount;

                    Rusult.Total=model.Total;
                    _ctx.Products.Update(Rusult);
                    _ctx.SaveChanges();
                return Ok(Rusult);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Rusult = _ctx.Products.FirstOrDefault(b => b.Id == id);
                if (Rusult != null)
                {
                    _ctx.Products.Remove(Rusult);
                    _ctx.SaveChanges();
                    return Ok(Rusult);
                }
                return BadRequest(); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
