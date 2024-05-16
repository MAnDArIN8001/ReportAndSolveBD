-- Reports --

CREATE OR REPLACE PROCEDURE CreateNewReport(authorId integer, title text, text text) 
LANGUAGE plpgsql
AS $$ 
DECLARE 
	lastId integer;
BEGIN
	INSERT INTO PUBLIC.REPORT(Author, Title, Text) VALUES (authorId, title, text);
END;
$$

DROP PROCEDURE CreateNewReport;

CREATE OR REPLACE PROCEDURE UpdateReport(reportId integer, newTitle text, newText text)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE REPORT 
	SET "title" = newTitle, "text" = newText
	WHERE REPORT.id = reportId;
END;
$$

DROP PROCEDURE UpdateReport;

CREATE OR REPLACE PROCEDURE DeleteReport(reportId integer)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE FROM REPORT 
	WHERE "id" = reportId;
END;
$$

DROP PROCEDURE DeleteReport;

-- StatusHistory --

CREATE OR REPLACE PROCEDURE CreateStatusHistory(reportId integer)
LANGUAGE plpgsql 
AS $$ 
BEGIN 
	INSERT INTO STATUSHISTORY(Report, Statuses) VALUES (reportId, '{}');
END;
$$

DROP PROCEDURE CreateStatusHistory;

CREATE OR REPLACE PROCEDURE UpdateStatusHistory(reportId integer, newStatus text)
LANGUAGE plpgsql
AS $$ 
DECLARE 
	previousStatuses text[];
BEGIN
	SELECT statuses 
	INTO previousStatuses
	FROM STATUSHISTORY
	WHERE "report" = reportId;
	
	previousStatuses := previousStatuses || newStatus;
	
	UPDATE STATUSHISTORY
	SET "statuses" = previousStatuses
	WHERE "report" = reportId;
END;
$$

DROP PROCEDURE UpdateStatusHistory

-- Users --

CREATE OR REPLACE PROCEDURE CreateNewUser(roleId integer, name text, mail text, password text)
LANGUAGE plpgsql
AS $$ 
BEGIN 
	INSERT INTO USERS(Role, Name, Mail, Password) VALUES(roleId, name, mail, password);
END;
$$

DROP PROCEDURE CreateNewUser;

CREATE OR REPLACE PROCEDURE UpdateUser(userId integer, newRoleId integer, newName text, newMail text, newPassword text)
LANGUAGE plpgsql
AS $$
BEGIN 
	UPDATE USERS
	SET "role" = newRoleId, "name" = newName, "mail" = newMail, "password" = newPassword
	WHERE "id" = userId;
END;
$$

DROP PROCEDURE UpdateUser;

CREATE OR REPLACE PROCEDURE DeleteUser(userId integer)
LANGUAGE plpgsql
AS $$ 
BEGIN
	DELETE FROM STATUSHISTORY
	WHERE "report" IN (SELECT Id FROM REPORT WHERE "author" = userId);
	
	DELETE FROM COMMENT
	WHERE "author" = userId;

	DELETE FROM REPORT
	WHERE "author" = userId;

	DELETE FROM Users
	WHERE "id" = userId;
END;
$$

DROP PROCEDURE DeleteUser;

-- Comment --

CREATE OR REPLACE PROCEDURE CreateComment(userId integer, reportId integer, commentText text)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO PUBLIC."comment"(author, report, text) VALUES(userId, reportId, commentText);
END;
$$ 

DROP PROCEDURE CreateComment;

CREATE OR REPLACE PROCEDURE DeleteComment(commentId integer)
LANGUAGE plpgsql
AS $$ 
BEGIN 
	DELETE FROM PUBLIC.COMMENT WHERE "id" = commentId;
END;
$$

DROP PROCEDURE DeleteComment;