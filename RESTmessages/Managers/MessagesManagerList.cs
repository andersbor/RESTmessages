using System.Collections.Generic;
using RESTmessages.Models;

namespace RESTmessages.Managers
{
    public class MessagesManagerList //: IMessagesManager
    {
        // NOT USED at the moment!!
        // TODO implement IMessagesManager
        private static int _nextMessageId = 1;
        private static int _nextCommentId = 1;
        private static readonly List<Message> Data = new List<Message>
        {
            new Message {Id = _nextMessageId++, Content = "I love apple pie", User ="anders",
                /*Comments = new List<Comment>
                {
                    new Comment {Id = _nextCommentId++,Content = "So do I", MessageId = _nextMessageId-1, User = "anbo"},
                    new Comment{ Id = _nextCommentId++, Content = "Mums", MessageId = _nextMessageId-1, User = "Tump" }
                    }*/
                },
            new Message {Id=_nextMessageId++, Content = "Nice weather", User = "Anders", /* Comments = new List<Comment>() */
    }
};

        public List<Message> GetAllMessages()
        {
            List<Message> messages = new List<Message>(Data);
            messages.Sort((message, message1) => message1.Id - message.Id);
            return messages; // copy constructor
        }

       /* public List<Comment> GetComments(int messageId)
        {
            Message message = Data.Find(m => m.Id == messageId);
            if (message == null) return null;
            //List<Comment> comments = message.Comments;
            //comments.Sort((comment, comment1) => comment1.Id - comment.Id);
            //return comments;
        }*/

        public Message AddMessage(Message message)
        {
            message.Id = _nextMessageId++;
            //if (message.Comments == null) message.Comments = new List<Comment>();
            Data.Add(message);
            return message;
        }

        public Comment AddComment(int messageId, Comment comment)
        {
            Message message = Data.Find(m => m.Id == messageId);
            if (message == null) return null; // throw Exception??
            comment.Id = _nextCommentId++;
            //message.Comments.Add(comment);
            return comment;
        }

        public Message DeleteMessage(int messageId)
        {
            Message message = Data.Find(m => m.Id == messageId);
            if (message == null) return null; // throw Exception??
            Data.Remove(message);
            return message;
        }

        public Comment DeleteComment(int messageId, int commentId)
        {
            Message message = Data.Find(m => m.Id == messageId);
            if (message == null) return null; // throw Exception??
            //Comment comment = message.Comments.Find(c => c.Id == commentId);
            //message.Comments.Remove(comment);
            //return comment;
            return null;
        }
    }
}
