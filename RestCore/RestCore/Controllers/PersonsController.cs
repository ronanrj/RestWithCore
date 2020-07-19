using Microsoft.AspNetCore.Mvc;
using RestCore.Model;
using RestCore.Business;
using RestCore.Data.Converters;
using RestCore.Data.VO;
using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Authorization;

namespace RestCore.Controllers
{

    [ApiVersion("1.0")]
    //[Route("api/[controller]/v{version:apiVersion}")] 
    [Route("api/[controller]")]    
    public class PersonsController : ControllerBase
    {
        //declaração do serviço usado
        private IPersonBusiness _personBusiness;
       

        //Injeção de uma instancia de IpersonService ao criar uma instancia de PersonController
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }


        // GET api/values
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")] //path paramams
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindId(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] PersonVO person)
        {            
            if (person == null) return NotFound();
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT api/values/5
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Put(int id, [FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null) return BadRequest();
            return new ObjectResult(updatedPerson);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
