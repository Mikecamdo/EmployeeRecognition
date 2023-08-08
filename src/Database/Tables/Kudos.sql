CREATE TABLE Kudos (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    SenderId NVARCHAR(50),
    ReceiverId NVARCHAR(50) NOT NULL,
    Title NVARCHAR(50) NOT NULL,
    Message NVARCHAR(200) NOT NULL,
    TeamPlayer BIT NOT NULL,
    OneOfAKind BIT NOT NULL,
    Creative BIT NOT NULL,
    HighEnergy BIT NOT NULL,
    Awesome BIT NOT NULL,
    Achiever BIT NOT NULL,
    Sweetness BIT NOT NULL,
    TheDate DATE NOT NULL
);