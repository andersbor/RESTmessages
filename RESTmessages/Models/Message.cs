using System.Collections.Generic;

namespace RESTmessages.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string User { get; set; }
        //public List<Comment> Comments { get; set; }
        public int TotalComments { get; set; }
    }
}
