CREATE OR alter PROCEDURE show_enrolled_classes
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
   WHERE   t.studentID = @username and t.sem = @sem and t.year = @year and (t.progress = 2 or t.progress = 3))
   
END

