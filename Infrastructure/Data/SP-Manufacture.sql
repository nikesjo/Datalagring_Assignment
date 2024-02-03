CREATE PROCEDURE GetOrCreateManufactureId
	@Manufacture nvarchar(50),
	@ManufactureId INT OUTPUT
AS
BEGIN
	SELECT @ManufactureId = Id FROM Manufactures WHERE Manufacture = @Manufacture

	IF @ManufactureId IS NULL
	BEGIN
		INSERT INTO Manufactures VALUES(@Manufacture)
		SELECT @ManufactureId = SCOPE_IDENTITY()
	END
END