using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VendorManagement2.Models;

namespace VendorManagement2.Controllers
{
    public class CategoryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CategoryData/ListCategories
        [HttpGet]
        [Route("api/CategoryData/ListCategories")]
        public IHttpActionResult ListCategories()
        {
            var categories = db.Categories.ToList();
            return Ok(categories);
        }

        // GET: api/CategoryData/FindCategory/5
        [HttpGet]
        [Route("api/CategoryData/FindCategory/{id}")]
        public IHttpActionResult FindCategory(int id)
        {
            var category = db.Categories.Include("Events")
                                        .FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/CategoryData/AddCategory
        [HttpPost]
        [Route("api/CategoryData/AddCategory")]
        public IHttpActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryID }, category);
        }

        // POST: api/CategoryData/UpdateCategory/5
        [HttpPost]
        [Route("api/CategoryData/UpdateCategory/{id}")]
        public IHttpActionResult UpdateCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // POST: api/CategoryData/DeleteCategory/5
        [HttpPost]
        [Route("api/CategoryData/DeleteCategory/{id}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }
    }
}
