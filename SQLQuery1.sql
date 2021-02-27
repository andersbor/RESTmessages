select * from twistermessage left outer join twistercomment 
on twistermessage.id = twistercomment.messageId;

select twistermessage.*, 
(select count(*) from twistercomment where twistercomment.messageId=twistermessage.id) as tot 
from twistermessage order by twistermessage.id desc;

SELECT A.*, (SELECT COUNT(*) FROM B WHERE B.a_id = A.id) AS TOT FROM A

select * from twistercomment where messageId = 1 order by id desc;

insert into twistermessage (content, [user]) values ('hi there', 'andersb@gmail.com')

insert into twistercomment (content, [user], messageId) values ('great message', 'andersb@gmail.com',1)

select * from twistercomment where messageid=1 and id=1;

select * from twistermessage;