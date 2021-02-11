select * from twistermessage left outer join twistercomment 
on twistermessage.id = twistercomment.messageId;

select twistermessage.*, 
(select count(*) from twistercomment where twistercomment.messageId=twistermessage.id) as tot 
from twistermessage;

SELECT A.*, (SELECT COUNT(*) FROM B WHERE B.a_id = A.id) AS TOT FROM A