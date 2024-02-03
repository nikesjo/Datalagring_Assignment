CREATE PROCEDURE AddProduct
	@ArticleNumber nvarchar(250),
	@Title nvarchar(200),
	@Description nvarchar(max),
	@Price money,
	@Manufacture nvarchar(50),
	@CategoryName nvarchar(50),
	@Result nvarchar(max) OUTPUT
AS
BEGIN
	
	IF NOT EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber)
	BEGIN
		BEGIN TRANSACTION

		BEGIN TRY
			DECLARE @ManufactureId int
			EXEC GetOrCreateManufactureId @Manufacture, @ManufactureId OUTPUT

			DECLARE @CategoryId int
			EXEC GetOrCreateCategoryId @CategoryName, @CategoryId OUTPUT

			INSERT INTO Products VALUES (@ArticleNumber, @Title, @Description, @Price, @ManufactureId, @CategoryId)
			SET @Result = 'One new product was created!'
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SET @Result = 'Something went wrong. Try again.'
		END CATCH
	END
	ELSE
		SET @Result = 'It already exists a product with article number ' + @ArticleNumber + '.'
END


