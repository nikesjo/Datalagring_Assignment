DROP TABLE ProductPrices
DROP TABLE Products
DROP TABLE Currencies
DROP TABLE Categories
DROP TABLE Manufactures

CREATE TABLE Manufactures
(
	Id int not null identity primary key,
	Manufacture nvarchar(50) not null unique
)

CREATE TABLE Categories
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique
)

CREATE TABLE Products
(
	ArticleNumber nvarchar(250) not null primary key,
	Title nvarchar(200) not null,
	Description nvarchar(max) null,
	Specification nvarchar(max) null,
	ManufactureId int not null references Manufactures(Id),
	CategoryId int not null references Categories(Id)
)

CREATE TABLE Currencies
(
	Code char(3) not null primary key,
	Currency nvarchar(20) not null unique
)

CREATE TABLE ProductPrices
(
	ArticleNumber nvarchar(250) not null primary key references Products(ArticleNumber),
	Price money not null,
	CurrencyCode char(3) not null references Currencies(Code)
)