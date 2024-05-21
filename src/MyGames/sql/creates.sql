 create database MyGames;

  use MyGames;

 create table Games(
     Id INT PRIMARY KEY IDENTITY(1, 1),
     Name NVARCHAR(60),
	 Description nvarchar(300) null,
	 Price money null
 );

 create table Comments(
     Id INT PRIMARY KEY IDENTITY(1, 1),
     Title NVARCHAR(60),
	 Text nvarchar(300),
	 GameId int foreign key references Games(Id)
 );

 insert into Games(Name, Description, Price)
 values
 ('Battlefront 2', 'coop shooter from StarWars universe part 2', 50),
 ('Battlefront 1', 'coop shooter from StarWars universe', 30),
 ('Terraria', 'sandbox from independent developers', 15)

 insert into Comments(GameId, Title, Text)
 values
 (1, 'awesome', 'awesome game!!'),
 (2, 'nice', 'nice game!!'),
 (2, 'bad', 'bad game!!')



 create table Logs(
	Id int primary key identity(1, 1),
	Url nvarchar(100) not null,
	RequestBody nvarchar(150),
	ResponsetBody nvarchar(max),
	CreationDate datetime2 not null,
	EndDate datetime2 not null,
	StatusCode int not null,
	HttpMethod nvarchar(20)
 )

 --use MyGames;

 --select * 
 --from Logs;

