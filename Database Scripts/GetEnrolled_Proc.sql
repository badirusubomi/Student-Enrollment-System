CREATE PROCEDURE show_enrolled_classes
    @username int,
    @sem NVARCHAR(32),
	@year NVARCHAR(8)
AS
BEGIN
   (SELECT c.courseName, t.sem, t.year FROM Course c, Taken t 
   WHERE   t.studentID = @username 
   AND UPPER(t.sem) = UPPER(@sem)
   AND t.year = @year)
   
   
   
END

exec  show_enrolled_classes 7, fall, 2023

