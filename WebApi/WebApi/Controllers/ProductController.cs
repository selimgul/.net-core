using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Models;

namespace WebApi.Controllers
{
    //[Produces("application/xml")]
    //[Consumes("application/json")]
    //[Authorize(Policy = "AtLeast21")]
    [Route("api/[controller]")]    
    public class ProductController : Controller
    {
        private IMemoryCache _cache;

        public ProductController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private readonly List<Product> _productList = new List<Product> {
            new Product{ ID = 1, Name = "Monitor"},
            new Product{ ID = 2, Name = "Keyboard"},
            new Product{ ID = 3, Name = "Mouse"}
        };

        #region Get
        // GET: api/Product
        [HttpGet]        
        public IEnumerable<Product> Get()
        {
            IEnumerable<Product> productList = null;

            if (!_cache.TryGetValue<IEnumerable<Product>>("ProductListInCache", out productList))
            {
                productList = _productList;
                var cacheEntryOptions = new MemoryCacheEntryOptions()                
                                        .SetSlidingExpiration(TimeSpan.FromSeconds(20));

                _cache.Set("ProductListInCache", productList, cacheEntryOptions);
            }

            return productList;
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {            
            // asp.net core 2.1 => ActionResult<T>
            return Ok(_productList.FirstOrDefault<Product>(p => p.ID == id));
        }

        [HttpGet("/GetByActionResult/{id}", Name = "GetByActionResult")]
        public ActionResult GetByActionResult(int id)
        {            
            return Ok(_productList.FirstOrDefault<Product>(p => p.ID == id));
        }

        // GET: api/Product/GetByFromHeader
        [HttpGet("GetByFromHeader", Name = "GetByFromHeader")]
        public IActionResult GetByFromHeader([FromHeader(Name = "X-ProductID")] string id)
        {
            // FromHeader için parametre string tanımlanmalıdır.
            return Ok(_productList.FirstOrDefault<Product>(p => p.ID == Convert.ToInt32(id)));
        }

        // GET: api/Product/GetByFromQuery
        [HttpGet("GetByFromQuery", Name = "GetByFromQuery")]
        public IActionResult GetByFromQuery(int id)
        {                       
            return Ok(_productList.FirstOrDefault<Product>(p => p.ID == id));
        }

        // GET: api/Product/GetWithFormat
        [FormatFilter]
        [HttpGet("GetWithFormat/{id}.{format?}", Name = "GetWithFormat")]
        public IActionResult GetWithFormat(int id)
        {
            return Ok(_productList.FirstOrDefault<Product>(p => p.ID == id));
        }
        #endregion

        #region POST
        // POST: api/Product
        [HttpPost]
        public void Post([FromBody]Product product)
        {            
            _productList.Add(product);
        }
        #endregion

        #region PUT
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product product)
        {
        }
        #endregion

        #region DELETE
        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
