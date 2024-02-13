CREATE PROCEDURE show_enrolled_classes
    @username int,
    @sem NVARCHAR(32),
	@year NVARCHAR(8)
AS
BEGIN
   (SELECT c.courseName, t.sem, t.year, s.secID, ts.day, ts.startTime, ts.endTime
   FROM Course c, Taken t, Section s, Timeslot ts
   WHERE   t.studentID = @username 
   AND t.courseID = c.courseID
   AND t.secID = s.secID
   AND s.courseID = c.courseID
   AND s.timeslotID = ts.timeslotID
   AND UPPER(t.sem) = UPPER(@sem)
   AND t.year = @year)
   
   
   
END
go


alter PROCEDURE show_enrolled_classes
    @username int,
    @sem NVARCHAR(32),
	@year NVARCHAR(8)
AS
BEGIN
   (SELECT c.courseID, c.courseName, t.sem, t.year, s.secID, ts.day, ts.startTime, ts.endTime
   FROM
    Section s
JOIN
    Taken t ON s.courseID = t.courseID AND s.secID = t.secID AND s.sem = t.sem AND s.year = t.year
JOIN
    Course c ON s.courseID = c.courseID
JOIN
    Timeslot ts ON s.timeslotID = ts.timeslotID
   WHERE   t.studentID = @username and t.sem = @sem and t.year = @year)
   
   
END
