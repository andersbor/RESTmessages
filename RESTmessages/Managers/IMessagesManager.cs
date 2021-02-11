using System.Collections.Generic;
using RESTmessages.Models;

namespace RESTmessages.Managers
{
    public interface IMessagesManager
    {
        List<Message> GetAll();
        List<Comment> GetComments(int messageId);
        Message AddMessage(Message message);
        Comment AddComment(int messageId, Comment comment);
        Message DeleteMessage(int messageId);
        Comment DeleteComment(int messageId, int commentId);
    }
}
