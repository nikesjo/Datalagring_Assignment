CREATE PROCEDURE GetOrCreateCategoryId
	@CategoryName nvarchar(50),
	@CategoryId int OUTPUT
AS
BEGIN
	SELECT @CategoryId = Id FROM Categories WHERE CategoryName = @CategoryName

	IF @CategoryId IS NULL
	BEGIN
		INSERT INTO Categories VALUES(@CategoryName)
		SELECT @CategoryId = SCOPE_IDENTITY()
	END
END