select top 1 * from twistermessage;

select top 1 twistermessage.*, 
                (select count(*) from twistercomment 
                where twistercomment.messageId = twistermessage.id) as tot from twistermessage;
