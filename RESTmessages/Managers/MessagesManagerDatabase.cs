using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RESTmessages.Models;

namespace RESTmessages.Managers
{
    public class MessagesManagerDatabase : IMessagesManager
    {
        private const string ConnectionString = Secrets.ConnectionString;

        public List<Message> GetAllMessages()
        {
            string selectString =
                @"select twistermessage.*, 
                (select count(*) from twistercomment 
                where twistercomment.messageId = twistermessage.id) as tot from twistermessage 
                order by twistermessage.id desc";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Message> result = new List<Message>();
                        while (reader.Read())
                        {
                            Message message = ReadMessage(reader);
                            result.Add(message);
                        }
                        return result;
                    }
                }
            }
        }

        private Message ReadMessage(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string content = reader.GetString(1);
            string user = reader.GetString(2);
            int totalComments = reader.GetInt32(3);
            Message message = new Message
            {
                Id = id,
                Content = content,
                User = user,
                TotalComments = totalComments
            };
            return message;
        }

        public List<Comment> GetComments(int messageId)
        {
            string selectString =
                "select * from twistercomment where messageId = @messageId order by id desc";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@messageId", messageId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Comment> result = new List<Comment>();
                        while (reader.Read())
                        {
                            Comment comment = ReadComment(reader);
                            result.Add(comment);
                        }
                        return result;
                    }
                }
            }
        }

        private Comment ReadComment(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string content = reader.GetString(1);
            string user = reader.GetString(2);
            Comment comment = new Comment
            {
                Id = id,
                Content = content,
                User = user,
            };
            return comment;
        }

        public Message AddMessage(Message message)
        {
            string insertString = "insert into twistermessage(content, [user]) values(@content, @user)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@content", message.Content);
                    command.Parameters.AddWithValue("@user", message.User);
                    //command.Parameters.AddWithValue("@publisher", value.Publisher);
                    //command.Parameters.AddWithValue("@price", value.Price);
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected; 
                    int newId = GetLatestId(conn, "twistermessage");
                    message.Id = newId;
                    return message;
                }
            }
        }

        private int GetLatestId(SqlConnection connection, string tableName)
        {
            // https://www.munisso.com/2012/06/07/4-ways-to-get-identity-ids-of-inserted-rows-in-sql-server/
            string selectString = "select IDENT_CURRENT(@tableName)";
            using (SqlCommand command = new SqlCommand(selectString, connection))
            {
                command.Parameters.AddWithValue("@tableName", tableName);
                // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand
                decimal id = (decimal)command.ExecuteScalar();
                return Decimal.ToInt32(id);
            }
        }

        public Comment AddComment(int messageId, Comment comment)
        {
            string insertString =
                "insert into twistercomment (content, [user], messageId) values (@content, @user,@messageid)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(insertString, conn))
                {
                    command.Parameters.AddWithValue("@content", comment.Content);
                    command.Parameters.AddWithValue("@user", comment.User);
                    command.Parameters.AddWithValue("@messageid", comment.MessageId);
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected; 
                    int newId = GetLatestId(conn, "twistercomment");
                    comment.Id = newId;
                    return comment;
                }
            }
        }

        public Message DeleteMessage(int messageId)
        {
            Message message = null;
            // https://stackoverflow.com/questions/13677318/how-to-run-multiple-sql-commands-in-a-single-sql-connection
            string selectString = @"select twistermessage.*, 
                (select count(*) from twistercomment 
                where twistercomment.messageId = twistermessage.id) as tot from twistermessage where id=@id";
            string deleteString = "delete from twistermessage where id = @id";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@id", messageId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            message = ReadMessage(reader);
                        }
                        else return null;
                    }
                }

                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@id", messageId);
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected; // should be 1
                }
            }
            return message;
        }

        public Comment DeleteComment(int messageId, int commentId)
        {
            Comment comment = null;
            // https://stackoverflow.com/questions/13677318/how-to-run-multiple-sql-commands-in-a-single-sql-connection
            string selectString = "select * from twistercomment where messageid=@messageId and id=@commentId;";
            string deleteString = "delete from twistercomment where id = @commentId";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@messageId", messageId);
                    command.Parameters.AddWithValue("@commentId", commentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            comment = ReadComment(reader);
                        }
                        else return null;
                    }
                }

                using (SqlCommand command = new SqlCommand(deleteString, conn))
                {
                    command.Parameters.AddWithValue("@commentId", commentId);
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected; // should be 1
                }
            }
            return comment;
        }
    }
}
