select top 1 * from twistermessage order by id desc;

select twistermessage.*, 
                (select count(*) from twistercomment 
                where twistercomment.messageId = twistermessage.id) as tot 
                from twistermessage order by twistermessage.id desc