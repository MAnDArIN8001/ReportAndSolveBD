-- Report --

CREATE TRIGGER CreateStatusHistoryTrigger
    AFTER INSERT ON REPORT
    FOR EACH ROW
    EXECUTE FUNCTION CreateStatusHistory();
	
DROP TRIGGER CreateStatusHistoryTrigger ON REPORT;
	
CREATE TRIGGER DeleteStatusHistoryTrigger
    BEFORE DELETE ON REPORT
    FOR EACH ROW
    EXECUTE FUNCTION DeleteStatusHistory();
	
DROP TRIGGER DeleteStatusHistoryTrigger ON REPORT;
	
CREATE TRIGGER DeleteCommentsTrigger
	BEFORE DELETE ON REPORT
	FOR EACH ROW
	EXECUTE FUNCTION DeleteComment();
	
DROP TRIGGER DeleteCommentsTrigger ON REPORT;

	