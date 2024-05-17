CREATE OR REPLACE VIEW UsersWithRole AS
SELECT public.role.name AS roleName, public.users.name AS userName
from users inner join role on role.id = users.role

SELECT * FROM UsersWithRole;

DROP VIEW UsersWithRole;

CREATE OR REPLACE VIEW AuthorizationUserData AS	
SELECT name, mail, password FROM USERS;

SELECT * FROM AuthorizationUserData;

DROP VIEW AuthorizationUserData;