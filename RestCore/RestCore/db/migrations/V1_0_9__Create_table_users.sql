CREATE TABLE users (
	ID INT(10) NOT NULL IDENTITY(1,1),
	Login VARCHAR(50) UNIQUE NOT NULL,
	AccessKey VARCHAR(50) NOT NULL,
	PRIMARY KEY (ID)
);