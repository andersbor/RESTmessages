using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTmessages.Managers;
using RESTmessages.Models;

namespace RESTmessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesManager _manager = new MessagesManagerDatabase(); //  new MessagesManagerList();


        // GET: api/<MessagesController>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _manager.GetAllMessages();
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
