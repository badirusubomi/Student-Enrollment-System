use CMPT391Database
go

/*
exec enroll_class 1001,1,1,'Spring','2024'


delete from taken where studentID = 1001
 INSERT INTO [Section] values (1, 4, 'Spring', 2023, 20, 0, 16);

/*Add to shopping cart*/
insert into Taken values(1,1,4,'Spring','2023',1)
insert into Taken values(2,1,4,'Spring','2023',1)
insert into Taken values(3,1,4,'Spring','2023',1)
insert into Taken values(4,1,4,'Spring','2023',1)
insert into Taken values(5,1,4,'Spring','2023',1)
insert into Taken values(6,1,4,'Spring','2023',1)
insert into Taken values(7,1,4,'Spring','2023',1)
insert into Taken values(8,1,4,'Spring','2023',1)
insert into Taken values(9,1,4,'Spring','2023',1)
insert into Taken values(10,1,4,'Spring','2023',1)
insert into Taken values(11,1,4,'Spring','2023',1)
insert into Taken values(12,1,4,'Spring','2023',1)
insert into Taken values(13,1,4,'Spring','2023',1)
insert into Taken values(14,1,4,'Spring','2023',1)
insert into Taken values(15,1,4,'Spring','2023',1)
insert into Taken values(16,1,4,'Spring','2023',1)
insert into Taken values(17,1,4,'Spring','2023',1)
insert into Taken values(18,1,4,'Spring','2023',1)
insert into Taken values(19,1,4,'Spring','2023',1)
insert into Taken values(20,1,4,'Spring','2023',1)
insert into Taken values(21,1,4,'Spring','2023',1)
insert into Taken values(22,1,4,'Spring','2023',1)
insert into Taken values(23,1,4,'Spring','2023',1)
insert into Taken values(24,1,4,'Spring','2023',1)

/*Enroll in classes*/

exec enroll_class 1,1,4,'Spring','2023'
exec enroll_class 2,1,4,'Spring','2023'
exec enroll_class 3,1,4,'Spring','2023'
exec enroll_class 4,1,4,'Spring','2023'
exec enroll_class 5,1,4,'Spring','2023'
exec enroll_class 6,1,4,'Spring','2023'
exec enroll_class 7,1,4,'Spring','2023'
exec enroll_class 8,1,4,'Spring','2023'
exec enroll_class 9,1,4,'Spring','2023'
exec enroll_class 10,1,4,'Spring','2023'
exec enroll_class 11,1,4,'Spring','2023'
exec enroll_class 12,1,4,'Spring','2023'
exec enroll_class 13,1,4,'Spring','2023'
exec enroll_class 14,1,4,'Spring','2023'
exec enroll_class 15,1,4,'Spring','2023'
exec enroll_class 16,1,4,'Spring','2023'
exec enroll_class 17,1,4,'Spring','2023'
exec enroll_class 18,1,4,'Spring','2023'
exec enroll_class 19,1,4,'Spring','2023'
exec enroll_class 20,1,4,'Spring','2023'
exec enroll_class 21,1,4,'Spring','2023'
exec enroll_class 22,1,4,'Spring','2023'
exec enroll_class 23,1,4,'Spring','2023'
exec enroll_class 24,1,4,'Spring','2023'


update section
set enrolled = 0
where courseID = 1 AND secID = 4 AND sem = 'Spring' AND year = '2023';

delete from Taken
select * from Taken
select * from Taken t where t.secID= 3 and t.sem = 'Spring' and t.year = '2023'
select * from section s where s.courseID = 1 and s.secID = 4 and s.sem = 'Spring' and s.year = '2023'; 
*/


alter PROCEDURE enroll_class
    @studentID int, 
    @courseID int,
    @sectionID int,
    @semester nvarchar(32),
    @year nvarchar(8)
AS 
BEGIN 
    -- Check if the student is already enrolled in the specified class
    IF NOT EXISTS (SELECT 1 FROM Taken t WHERE t.studentID = @studentID AND t.courseID = @courseID AND t.secID = @sectionID AND t.sem = @semester AND t.year = @year and (t.progress = 2 or t.progress = 3))
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;

            -- Get the current enrollment and capacity of the section
            DECLARE @enrolled int;
            DECLARE @capacity int;
            SELECT @enrolled = s.enrolled, @capacity = s.cap
            FROM Section s
            WHERE s.courseID = @courseID AND s.secID = @sectionID AND s.sem = @semester AND s.year = @year;

            IF @enrolled < @capacity
            BEGIN
                -- Update the status of the existing enrollment
                UPDATE Taken
                SET progress = 2
                WHERE studentID = @studentID AND courseID = @courseID AND secID = @sectionID AND sem = @semester AND year = @year AND progress = 1;

                -- Increment the enrollment count for the section
                UPDATE Section
                SET enrolled = @enrolled + 1
                WHERE courseID = @courseID AND secID = @sectionID AND sem = @semester AND year = @year;

                COMMIT TRANSACTION;
            END
            ELSE
            BEGIN
                -- Rollback if the section is already at full capacity
                ROLLBACK TRANSACTION;
                RETURN -2;
            END
        END TRY
        BEGIN CATCH
            -- Rollback if an error occurs
            ROLLBACK TRANSACTION;
            RETURN -3;
        END CATCH
    END
    ELSE
    BEGIN 
        -- Return -1 if the student is already enrolled in the specified class
        RETURN -1;
    END
END
go



ALTER PROCEDURE show_classes
    @courseName nvarchar(32), 
    @sem nvarchar(32),
    @year nvarchar(8)
AS 
BEGIN 
	select c.courseID, c.courseName, s.secID, s.sem, s.year, s.cap, s.enrolled,
	ts.day, ts.startTime, ts.endTime
	from Course c
	INNER JOIN Section s 
		ON s.courseID = c.courseID 
	INNER JOIN Timeslot ts 
		ON ts.timeslotID = s.timeslotID
	WHERE UPPER(c.courseName) like UPPER('%' + @courseName + '%' )
	AND UPPER(s.sem) = UPPER(@sem) 
	AND s.year = @year;
	/*, Course c, Timeslot ts, Teaches tc, Instructor i
	WHERE s.courseID = c.courseID
	AND s.timeslotID = ts.timeslotID
	AND tc.courseID = s.courseID
	AND tc.instructorID = i.instructorID
	AND UPPER(c.courseName) like UPPER('%' + @courseName + '%' )
	AND s.sem = @sem AND s.year = @year;*/
END




