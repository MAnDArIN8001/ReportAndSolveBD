CREATE ROLE BaseUser;

GRANT SELECT, INSERT, UPDATE ON REPORT TO BaseUser;
GRANT SELECT ON USERS TO BaseUser;
GRANT SELECT, UPDATE ON STATUSHISTORY TO BaseUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON COMMENT TO BaseUser;

DROP ROLE BaseUser;

CREATE ROLE UserAdmin;

GRANT SELECT, INSERT, UPDATE, DELETE ON REPORT TO UserAdmin;
GRANT SELECT, UPDATE, DELETE ON USERS TO UserAdmin;
GRANT SELECT, UPDATE ON STATUSHISTORY TO UserAdmin;
GRANT SELECT, INSERT, UPDATE, DELETE ON COMMENT TO UserAdmin;

DROP ROLE UserAdmin;

CREATE USER user_admin WITH LOGIN PASSWORD '1234';
GRANT UserAdmin TO user_admin;

CREATE USER base_user WITH LOGIN PASSWORD '12345';
GRANT BaseUser TO base_user;

-- grant to base user

GRANT EXECUTE ON FUNCTION GetAllReports() TO base_user;
GRANT EXECUTE ON FUNCTION GetAllCommentsToReport(reportId integer) TO base_user;
GRANT EXECUTE ON FUNCTION GetAllReportsByAuthor(authorId integer) TO base_user;
GRANT EXECUTE ON FUNCTION GetAllUsers() TO base_user;
GRANT EXECUTE ON FUNCTION GetComment(commentId integer) TO base_user;
GRANT EXECUTE ON FUNCTION GetUserById(userId integer) TO base_user;
GRANT EXECUTE ON FUNCTION GetStatusHistoryToReport(reportId integer) TO base_user;

GRANT EXECUTE ON PROCEDURE CreateComment(userId integer, reportId integer, commentText text) TO base_user;
GRANT EXECUTE ON PROCEDURE CreateNewReport(authorId integer, title text, text text) TO base_user;
GRANT EXECUTE ON PROCEDURE DeleteComment(commentId integer) TO base_user;
GRANT EXECUTE ON PROCEDURE DeleteReport(reportId integer) TO base_user;

-- grants to admin user

GRANT EXECUTE ON FUNCTION GetAllReports() TO user_admin;
GRANT EXECUTE ON FUNCTION GetAllCommentsToReport(reportId integer) TO user_admin;
GRANT EXECUTE ON FUNCTION GetAllReportsByAuthor(authorId integer) TO user_admin;
GRANT EXECUTE ON FUNCTION GetAllUsers() TO user_admin;
GRANT EXECUTE ON FUNCTION GetComment(commentId integer) TO user_admin;
GRANT EXECUTE ON FUNCTION GetUserById(userId integer) TO user_admin;
GRANT EXECUTE ON FUNCTION GetStatusHistoryToReport(reportId integer) TO user_admin;

GRANT EXECUTE ON PROCEDURE CreateComment(userId integer, reportId integer, commentText text) TO user_admin;
GRANT EXECUTE ON PROCEDURE CreateNewReport(authorId integer, title text, text text) TO user_admin;
GRANT EXECUTE ON PROCEDURE DeleteComment(commentId integer) TO user_admin;
GRANT EXECUTE ON PROCEDURE DeleteReport(reportId integer) TO user_admin;
GRANT EXECUTE ON PROCEDURE DeleteUser(userId integer) TO user_admin;
