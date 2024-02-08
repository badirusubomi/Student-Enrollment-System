use CMPT391Database;

CREATE PROCEDURE check_login 
    @username int,
    @password NVARCHAR(128)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Student s WHERE s.studentId = @username AND s.[password] = @password)
    BEGIN
        return (SELECT studentID FROM Student WHERE studentId = @username AND [password] = @password)
    END
    ELSE
    BEGIN
        return -1
    END
END
select * from Student

CREATE PROCEDURE find_courses
	@username int,
AS 
BEGIN 
	IF (SELECT 1 FROM Student s WHERE s.studentId = @username)
	BEGIN
		return (select * from courses as c where )
	END
	ELSE
	BEGIN 
		return -1
	END
END