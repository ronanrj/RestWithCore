CREATE TABLE books (
  id INT(10) IDENTITY(1,1) PRIMARY KEY,
  Author longtext,
  LaunchDate datetime(6) NOT NULL,
  Price decimal(65,2) NOT NULL,
  Title longtext
) ;
