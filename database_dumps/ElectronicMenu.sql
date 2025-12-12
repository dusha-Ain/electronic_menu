DROP TABLE OrderItems;
DROP TABLE Dishes;
DROP TABLE CategoriesDishes;
DROP TABLE Orders;
DROP TABLE Statuses;
DROP TABLE Users;
DROP TABLE Roles;





CREATE TABLE Roles
(
	ID SERIAL PRIMARY KEY,
	RoleName char(12) NOT NULL
);



CREATE TABLE CategoriesDishes
(
	ID SERIAL PRIMARY KEY,
	CategoryName CHAR(20) NOT NULL
);


CREATE TABLE Users
(
	ID SERIAL PRIMARY KEY,
	PasswordHash VARCHAR(100) NOT NULL,
	FirstName CHAR(50) NOT NULL,
	FecondName CHAR(50) NOT NULL,
	LastName CHAR(50),
	RegistrationDate TIMESTAMP CHECK (registrationDate > '2023-01-01 00:00:00'),
	FKRole INT REFERENCES Roles(ID)
);


CREATE TABLE Dishes
(
	ID SERIAL PRIMARY KEY,
	DishName VARCHAR(30) NOT NULL,
	Description TEXT,
	FKCategory INT REFERENCES CategoriesDishes(ID)
);



CREATE TABLE Statuses
(
	ID SERIAL PRIMARY KEY,
	StatusName CHAR(12) NOT NULL
);



CREATE TABLE Orders
(
	ID SERIAL PRIMARY KEY,
	OrderDate TIMESTAMP CHECK (orderDate > '2023-01-01 00:00:00'),
	FKstatus INT REFERENCES Statuses(ID),
	TotalAmount INT CHECK (TotalAmount > 0),
	FKUser INT REFERENCES Users(ID)
);



CREATE TABLE OrderItems
(
	FKOrder INT REFERENCES Orders(ID),
	FKDish INT REFERENCES Dishes(ID),
	PRIMARY KEY (FKOrder, FKDish),
	Quantity INT NOT NULL CHECK (quantity > 0),
	UnitPrice INT NOT NULL CHECK (unitPrice >= 0)
)


