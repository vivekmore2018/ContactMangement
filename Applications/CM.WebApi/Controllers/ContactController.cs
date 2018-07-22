using CM.Business;
using CM.Models;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CM.WebApi.Controllers
{
    public class ContactController : ApiController
    {
        private ContactManager _contactManager;
        /// <summary>
        /// Constructor injected with depedency of contact Manager BI class
        /// </summary>
        /// <param name="contactManager"></param>
        public ContactController(ContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        /// <summary>  
        /// Get All Contact Details  
        /// </summary>  
        /// <response code="200">Record found</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("api/contact/GetContacts")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Contact>))]
        public IHttpActionResult GetContacts()
        {
            var result = _contactManager.GetAll();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return  NotFound();
            }
        }


        /// <summary>
        /// Get Single Contact if found
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/contact/GetContact")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Contact))]
        /// <response code="200">return object</response>
        /// <response code="404">Not Found</response>
        public IHttpActionResult GetContact(int Id)
        {
            var result = _contactManager.GetById(Id);
            if (result !=null)
            {
                return Ok(result);
            }
            else
            {
                return  NotFound();
            }
        }


        /// <summary>  
        /// Create Contact to database
        /// </summary>  
        /// <response code="200">Created</response>
        /// <response code="400">Bad request</response>
        [Route("api/contact/CreateContact")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Contact))]
        public IHttpActionResult CreateContact(Contact contact)
        {
            var result = _contactManager.AddContact(contact);
            if(result.Status == Interface.Business.Status.Success)
            {
                return Ok(contact);
            }
            else
            {
                return BadRequest(string.Join(";", result.Errors));
            }
        }

        /// <summary>  
        /// Create Contact to database
        /// </summary>  
        /// <response code="200">Updated</response>
        /// <response code="400">Bad request</response>
        [Route("api/contact/UpdateContact")]
        [HttpPut]
        public IHttpActionResult UpdateContact([FromBody] Contact contact)
        {
            var result = _contactManager.Update(contact);
            if (result.Status == Interface.Business.Status.Success)
            {               
                return Ok(contact.Id);
            }
            else
            {
                return BadRequest(string.Join(";", result.Errors));
            }
        }
        /// <summary>
        /// Delete contact by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <response code="200">Contact deleted</response>
        /// <response code="404">Not found</response>
        [Route("api/contact/DeleteContact")]
        [HttpDelete]
        public IHttpActionResult DeleteContact(int Id)
        {
            var result = _contactManager.DeleteContact(Id);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
