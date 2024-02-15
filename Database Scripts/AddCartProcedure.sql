CREATE OR ALTER PROCEDURE add_cart
    @studentID int, 
    @courseID int,
    @sectionID int,
    @semester nvarchar(32),
    @year nvarchar(8),
	@timeslotID int
	
AS 
BEGIN
	-- Check if there is a prerequirement for the course --
	IF EXISTS (SELECT * FROM PreReq Where courseID = @courseID)
	BEGIN

		-- Check if prerequirements are met --
		IF EXISTS (SELECT *
			FROM Taken t
			INNER JOIN PreReq pr ON t.courseID = pr.prereqID
			WHERE t.studentID = @studentID AND pr.courseID = @courseID)

		-- Check if time conflicts --
		AND NOT EXISTS (SELECT *
			FROM Section s 
			INNER JOIN Taken t ON s.courseID = t.courseID
			WHERE t.studentID = @studentID AND s.timeslotID = @timeslotID AND UPPER(s.sem) =  UPPER(@semester) AND s.year = @year)

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

		-- Check if time conflicts --
		IF NOT EXISTS (SELECT *
			FROM Section s 
			INNER JOIN Taken t ON s.courseID = t.courseID
			WHERE t.studentID = @studentID AND s.timeslotID = @timeslotID AND UPPER(s.sem) = UPPER(@semester) AND s.year = @year)

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
