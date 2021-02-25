using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly FabrikaContext _context;

        public ValuesController(FabrikaContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { Id = 19201, Name = "Lego Nexo Knights King I", UnitPrice = 45 });
                _context.Products.Add(new Product { Id = 23942, Name = "Lego Starwars Minifigure Jedi", UnitPrice = 55 });
                _context.Products.Add(new Product { Id = 30021, Name = "Star Wars çay takımı ", UnitPrice = 35.50 });
                _context.Products.Add(new Product { Id = 30492, Name = "Star Wars kahve takımı", UnitPrice = 24.40 });

                _context.SaveChanges();
            }
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Product newProduct)
        {
            if (newProduct == null)
                return BadRequest();

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
