CREATE PROC add_to_cart
    @courseid int,
    @studentID int,
    @secId int
AS
BEGIN
    INSERT INTO [Taken] ([studentID], [courseID], [secID], [sem], [year], [progress])
    SELECT
        @studentID as [studentID],
        c.courseID as [courseID],
        s.secID as [secID],
        s.sem as [sem],
        s.year as [year],
        1 as [progress]  
    FROM
        Course c
        JOIN Section s ON c.courseID = s.courseID
    WHERE
        s.secID = @secId
END
