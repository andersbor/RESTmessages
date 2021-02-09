namespace RESTmessages.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Content { get; set; }

        public string User { get; set; }

        public override string ToString()
        {
            return Id + " " + MessageId+ " " + Content;
        }
    }
}
