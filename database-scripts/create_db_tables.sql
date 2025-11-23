CREATE DATABASE [LocalDb]

CREATE TABLE [LocalDb].[dbo].[Team](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	PRIMARY KEY ([Id])
)

CREATE TABLE [LocalDb].[dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	PRIMARY KEY ([Id])
)

CREATE TABLE [LocalDb].[dbo].[UserTeam](
	[UserId] [uniqueidentifier] NOT NULL,
	[TeamId] [uniqueidentifier] NOT NULL,
	PRIMARY KEY ([UserId],[TeamId]),
	FOREIGN KEY ([TeamId]) REFERENCES [Team]([Id]),
	FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)


-- Populate data in tables --

declare @team_A uniqueidentifier = newid()
declare @team_B uniqueidentifier = newid()
 
insert into [LocalDb].[dbo].[Team] values
	(@team_A, 'Team_A'),
	(@team_B, 'Team_B')

declare @john uniqueidentifier = newid()
declare @sophie uniqueidentifier = newid()
declare @smith uniqueidentifier = newid()
declare @emma uniqueidentifier = newid()

insert into [LocalDb].[dbo].[User] values
	(@john, 'John', 'john@mail.com', '1992-05-10', 1),
	(@sophie, 'Sophie', 'sophie@mail.com', '1990-03-15', 1),
	(@smith, 'Smith', 'smith@mail.com', '2000-11-28', 1),
	(@emma, 'Emma', 'emma@mail.com', '1989-09-03', 1)

insert into [LocalDb].[dbo].[UserTeam] values
	(@john,@team_A),
	(@john,@team_B),
	(@sophie,@team_A),
	(@smith,@team_B),
	(@emma,@team_B)
