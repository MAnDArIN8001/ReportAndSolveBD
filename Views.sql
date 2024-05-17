CREATE OR REPLACE VIEW UsersWithRole AS
SELECT public.role.name AS roleName, public.users.name AS userName
from users inner join role on role.id = users.role

SELECT * FROM UsersWithRole;

DROP VIEW UsersWithRole;

CREATE OR REPLACE VIEW AuthorizationUserData AS	
SELECT name, mail, password FROM USERS;

SELECT * FROM AuthorizationUserData;

DROP VIEW AuthorizationUserData;

CREATE OR REPLACE VIEW ReportsWithAuthors AS
SELECT
    public.report.ID AS report_id,
    public.report.Title AS report_title,
    public.report.Text AS report_text,
    public.users.Name AS author_name
FROM REPORT 
JOIN USERS ON public.report.Author = public.users.ID;

SELECT * FROM ReportsWithAuthors;

DROP VIEW ReportsWithAuthors;

CREATE OR REPLACE VIEW StatusHistoryWithReportInfo AS
SELECT
    public.statushistory.ID AS status_history_id,
    public.report.Title AS report_title,
    public.statushistory.Statuses
FROM STATUSHISTORY
JOIN REPORT ON public.statushistory.Report = public.report.ID;

DROP VIEW StatusHistoryWithReportInfo;