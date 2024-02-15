CREATE OR ALTER PROCEDURE show_cart
 @username int
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
   WHERE   t.studentID = @username  and t.progress =1)
   
END





select * from taken where studentID = 1 AND progress = 1 and year = '2024' and sem = 'Spring'

exec  show_cart 1