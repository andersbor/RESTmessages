using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTmessages.Managers;
using RESTmessages.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTmessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesManagerList _manager = new MessagesManagerList();


        // GET: api/<MessagesController>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _manager.GetAll();
        }

        // GET api/<MessagesController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        [HttpGet("{messageId}/comments")]
        public List<Comment> GetComments(int messageId)
        {
            return _manager.GetComments(messageId);
        }

        // POST api/<MessagesController>
        [HttpPost]
        public Message PostMessage([FromBody] Message value)
        {
            return _manager.AddMessage(value);
        }

        [HttpPost("{messageId}/comments")]
        public Comment PostComment(int messageId, [FromBody] Comment value)
        {
            return _manager.AddComment(messageId, value);
        }

        // PUT api/<MessagesController>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        // TODO status code
        public Message DeleteMessage(int id)
        {
            return _manager.DeleteMessage(id);
        }

        [HttpDelete("{messageId}/comments/{commentId}")]
        // TODO status codes (2 of them!!)
        public Comment DeleteComment(int messageId, int commentId)
        {
            return _manager.DeleteComment(messageId, commentId);
        }
    }
}
