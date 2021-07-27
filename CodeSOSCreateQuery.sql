CREATE DATABASE CodeSOSDatabase
GO

USE CodeSOSDatabase
GO

CREATE TABLE Categories(
CategoryID INT PRIMARY KEY IDENTITY(1,1),
CategoryName NVARCHAR(MAX))
GO

CREATE TABLE Users(
UserID INT PRIMARY KEY IDENTITY(1,1),
Email NVARCHAR(MAX),
PasswordHash NVARCHAR(MAX),
Name NVARCHAR(MAX),
Mobile NVARCHAR(MAX),
IsAdmin BIT DEFAULT(0))
GO

CREATE TABLE Questions(
QuestionID INT PRIMARY KEY IDENTITY(1,1),
QuestionName NVARCHAR(MAX),
QuestionDateAndTime DATETIME,
UserID INT REFERENCES Users(UserID) ON DELETE CASCADE,
CategoryID INT REFERENCES Categories(CategoryID) ON DELETE CASCADE,
VotesCount INT,
AnswersCount INT,
ViewsCount INT)
GO

CREATE TABLE Answers(
AnswerID INT PRIMARY KEY IDENTITY(1,1),
AnswerText NVARCHAR(MAX),
AnswerDateAndTime DATETIME,
UserID INT REFERENCES Users(UserID),
QuestionID INT REFERENCES Questions(QuestionID) ON DELETE CASCADE,
VotesCount INT)
GO

CREATE TABLE Votes(
VoteID INT PRIMARY KEY IDENTITY(1,1),
UserID INT REFERENCES Users(UserID),
AnswerID INT REFERENCES Answers(AnswerID) ON DELETE CASCADE,
VoteValue INT)
GO

USE CodeSOSDatabase
GO

INSERT INTO Users VALUES('admin@gmail.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin', '0000000000', 1)
GO
INSERT INTO Users VALUES('test@gmail.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'Test', '0000000000', 0)
GO

INSERT INTO Categories VALUES('HTML')
GO
INSERT INTO Categories VALUES('CSS')
GO
INSERT INTO Categories VALUES('JavaScript')
GO

INSERT INTO Questions VALUES('How to display icon in the browser titlebar using Html?', '2021-7-27 9:40 am', 2, 1, 0, 0, 0)
GO
INSERT INTO Questions VALUES('How to set background image in CSS', '2021-7-27 8:40 am', 2, 2, 0, 0, 0)
GO
