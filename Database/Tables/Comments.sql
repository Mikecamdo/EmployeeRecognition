CREATE TABLE Comments (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    KudoId INT NOT NULL,
    SenderId VARCHAR(50) NOT NULL,
    Message VARCHAR(200) NOT NULL
);