using CM.Business;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CM.WebApi.Controllers
{
    public class ContactController : ApiController
    {
        private ContactManager _contactManager;

        public ContactController(ContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        /// <summary>  
        /// Get All Contact Details  
        /// </summary>  
        [HttpGet]
        [Route("api/contact/GetContacts")]
        public IHttpActionResult GetContacts()
        {
            var result = _contactManager.GetAll();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return Ok("No contacts found");
            }
        }


        /// <summary>
        /// Get Single Contact if found
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/contact/GetContact")]
        public IHttpActionResult GetContact(int Id)
        {
            var result = _contactManager.GetById(Id);
            if (result !=null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("No contact found");
            }
        }
        

        /// <summary>  
        /// Create Contact to database
        /// </summary>  
        [Route("api/contact/CreateContact")]
        [HttpPost]
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
        [Route("api/contact/UpdateContact")]
        [HttpPost]
        public IHttpActionResult UpdateContact([FromBody] Contact contact)
        {
            var result = _contactManager.Update(contact);
            if (result.Status == Interface.Business.Status.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(";", result.Errors));
            }
        }
    }
}
