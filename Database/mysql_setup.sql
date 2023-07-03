USE railway;

CREATE TABLE users (
	Id VARCHAR(50) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Department VARCHAR(50) NOT NULL,
    AvatarURL VARCHAR(50) NOT NULL
);

CREATE TABLE kudos (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    Sender VARCHAR(50) NOT NULL,
    SenderId VARCHAR(50) NOT NULL,
    SenderAvatar VARCHAR(50) NOT NULL,
    Receiver VARCHAR(50) NOT NULL,
    ReceiverId VARCHAR(50) NOT NULL,
    ReceiverAvatar VARCHAR(50) NOT NULL,
    Title VARCHAR(50) NOT NULL,
    Message VARCHAR(200) NOT NULL,
    TeamPlayer BIT NOT NULL,
    OneOfAKind BIT NOT NULL,
    Creative BIT NOT NULL,
    HighEnergy BIT NOT NULL,
    Awesome BIT NOT NULL,
    Achiever BIT NOT NULL,
    Sweetness BIT NOT NULL,
    TheDate DATE NOT NULL
);

CREATE TABLE comments (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    KudosId INT NOT NULL,
    SenderId VARCHAR(50) NOT NULL,
    SenderName VARCHAR(50) NOT NULL,
    SenderAvatar VARCHAR(50) NOT NULL,
    Message VARCHAR(200) NOT NULL
);