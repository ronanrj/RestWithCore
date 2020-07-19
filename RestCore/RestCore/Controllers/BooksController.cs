using Microsoft.AspNetCore.Mvc;
using RestCore.Business;
using RestCore.Data.VO;

namespace RestCore.Controllers
{
    [ApiVersion("1.0")]
    //[Route("api/[controller]/v{version:apiVersion}")] 
    [Route("api/[controller]")]
    public class BooksController: ControllerBase
    {

        private IBookBusiness _bookBusiness;

        //Injeção de uma instancia de IpersonService ao criar uma instancia de PersonController
        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")] //path paramams
        public IActionResult Get(int id)
        {
            var person = _bookBusiness.FindId(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return NotFound();
            return new ObjectResult(_bookBusiness.Create(book));
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(int id, [FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            var updatedPerson = _bookBusiness.Update(book);
            if (updatedPerson == null) return BadRequest();
            return new ObjectResult(updatedPerson);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}

