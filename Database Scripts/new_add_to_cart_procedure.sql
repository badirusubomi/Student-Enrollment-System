CREATE OR ALTER PROCEDURE add_to_cart
    @studentID int, 
    @courseID int,
    @sectionID int,
    @semester nvarchar(32),
    @year nvarchar(8),
	@timeslotID int

AS 
BEGIN
	-- Check if there is a prerequisite for the course --
	IF EXISTS (SELECT * FROM PreReq WHERE courseID = @courseID)
	BEGIN
		-- Check if prerequisites are met and if there are no time conflicts --
		IF EXISTS (
			SELECT 1
			FROM Taken t
			INNER JOIN PreReq pr ON t.courseID = pr.prereqID
			WHERE t.studentID = @studentID AND pr.courseID = @courseID
			AND NOT EXISTS (
				SELECT 1
				FROM Section s 
				INNER JOIN Taken t ON s.courseID = t.courseID
				WHERE t.studentID = @studentID AND s.timeslotID = @timeslotID AND s.sem = @semester AND s.year = @year
			)
		)
		BEGIN
			BEGIN TRY
				BEGIN TRANSACTION;

				INSERT INTO Taken (studentID, courseID, secID, sem, year, progress) 
				VALUES (@studentID, @courseID, @sectionID, @semester, @year, '1')

                COMMIT TRANSACTION;

			END TRY
			BEGIN CATCH
				-- Rollback if an error occurs
				ROLLBACK TRANSACTION;
				RETURN -1;
			END CATCH
		END
	END
	ELSE
	BEGIN
		-- Check if there are no time conflicts for courses without prerequisites --
		IF NOT EXISTS (
			SELECT 1
			FROM Section s 
			INNER JOIN Taken t ON s.courseID = t.courseID
			WHERE t.studentID = @studentID AND s.timeslotID = @timeslotID AND s.sem = @semester AND s.year = @year
		)
		BEGIN
			BEGIN TRY
				BEGIN TRANSACTION;

				INSERT INTO Taken (studentID, courseID, secID, sem, year, progress) 
				VALUES (@studentID, @courseID, @sectionID, @semester, @year, '1')

                COMMIT TRANSACTION;

			END TRY
			BEGIN CATCH
				-- Rollback if an error occurs
				ROLLBACK TRANSACTION;
				RETURN -1;
			END CATCH
		END
	END
END
