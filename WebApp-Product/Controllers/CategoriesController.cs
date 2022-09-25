using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApp_Product.Data;
using WebApp_Product.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_Product.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            
            _context = context;

        }
        // GET: api/<ValuesController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()/// show
        {

            try
            {
                return Ok(_context.Categories.OrderBy(x => x.Name).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("GetById/{id}")]//// show
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == id);
                return Ok(category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<ValuesController>
        [HttpPost("Save")]//save
        public IActionResult Save([FromBody] Category model)// data will be posted to it
        {
            try
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("Edit/{id}")]//edit
        public IActionResult Edit(int id, [FromBody] Category category)
        {
            try
            {
                var Rusult= _context.Categories.FirstOrDefault(x => x.Id == id);
                if (Rusult != null)
                {
                    Rusult.Name=category.Name;
                    _context.Categories.Update(Rusult);//// بعمل ابديت لحجاجة بعينها
                    /// المادل هو داتا تايب شايل داتا 
                    _context.SaveChanges();

                return Ok(Rusult);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteCat/{id}")]
        public IActionResult DeleteCat(int id)
        {
            try
            {
                var Rusult = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (Rusult != null)
                {
                   
                    _context.Categories.Remove(Rusult);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
