Create Database TechnicalQandAForum
go
Use TechnicalQandAForum
go

Create Table Categories(
CategoryId int primary key Identity(1,1),
CategoryName nvarchar(max))
go

Create Table Users(
UserId int primary key Identity(1,1),
Email nvarchar(max),
PasswordHash nvarchar(max),
Name nvarchar(max),
Mobile nvarchar(max),
IsAdmin bit default(0))
go

Create Table Questions(
QuestionId int primary key identity(1,1),
QuestionName nvarchar(max),
QuestionDateAndTime datetime,
UserId int references Users(UserId) on delete cascade,
CategoryId int references Categories(Categoryid) on delete cascade,
VotesCount int,
AnswersCount int,
ViewsCount int)
go

Create Table Answers(
AnswerId int primary key identity(1,1),
AnswerText nvarchar(max),
AnswerDateAndTime datetime,
UserId int references Users(UserId),
QuestionId int references Questions(QuestionId) on delete cascade,
VotesCount int)
go

Create Table Votes(
VoteId int primary key identity(1,1),
UserId int references Users(UserId),
AnswerId int references Answers(AnswerId) on delete cascade,
VoteValue int)
go