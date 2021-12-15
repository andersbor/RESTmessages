using System.Collections.Generic;
using RESTmessages.Models;

namespace RESTmessages.Managers
{
    public interface IMessagesManager
    {
        List<Message> GetAllMessages(string user=null);
        Message GetLatestMessage();
        
        List<Comment> GetComments(int messageId, string user=null);
        Message AddMessage(Message message);
        Comment AddComment(int messageId, Comment comment);
        Message DeleteMessage(int messageId);
        Comment DeleteComment(int messageId, int commentId);
    }
}
