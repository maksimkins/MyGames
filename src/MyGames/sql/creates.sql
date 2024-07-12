--create database MyGamesDb;

--use MyGamesDb;

--create table GamesSeries(
--	Id int primary key identity(1, 1),
--	Name nvarchar(max) not null
--);

--create table Developers(
--	Id int primary key identity(1, 1),
--	Name nvarchar(max) not null
--);

--create table Genres(
--	Id int primary key identity(1, 1),
--	Name nvarchar(max) not null
--);

--create table Games(
--     Id int primary key identity(1, 1),
--	 SerieId int foreign key references GamesSeries(Id) null,
--     Name nvarchar(100) unique not null,
--	 Description nvarchar(max) null,
--	 Price money not null,
--	 CreationDate datetime2 not null,
--	 DeveloperId int foreign key references GamesSeries(Id) null,
--	 Rate float default 0 not null,
--	 ForAdultsOnly bit default 0 not null,
--	 PictureUrl nvarchar(max) null
-- );

-- create table Genres_Games(
--	Id int primary key identity(1, 1),
--	GameId int foreign key references Games(Id) on delete cascade not null,
--	GenreId int foreign key references Genres(Id) on delete cascade not null,
-- );

-- create table Addons(
--	Id int primary key identity(1, 1),
--	Name nvarchar(100) not null unique,
--	Description nvarchar(max) null,
--	GameId int foreign key references Games(Id) on delete cascade not null,
-- );

-- create table Comments(
--     Id int primary key identity(1, 1),
--     Title nvarchar(60) not null,
--	 Text nvarchar(300) not null,
--	 GameId int foreign key references Games(Id) on delete cascade not null,
--	 Rate float not null,
--	 CreationDate datetime2 not null,
--	 ChangeDate datetime2 null,
-- );

-- create table Users(
--	Id int primary key identity(1, 1),
--	Login varchar(60) not null unique,
--	Password varchar(60) not null,
--	Email varchar(320) not null unique,
--	UserName nvarchar(60) not null,
--	Balance money null,
--	BirthDate datetime2 not null,
--	PictureUrl nvarchar(max) null
-- );

-- create table Logs(
--	Id int primary key identity(1, 1),
--	Url nvarchar(100) not null,
--	RequestBody nvarchar(150),
--	ResponsetBody nvarchar(max),
--	CreationDate datetime2 not null,
--	EndDate datetime2 not null,
--	StatusCode int not null,
--	HttpMethod nvarchar(20) not null
-- );

 --use MyGamesDb;
 --insert into Games(Name, Description, Price, CreationDate, ForAdultsOnly)
 --values
 --('Battlefront 2', 'coop shooter from StarWars universe part 2', 50, '2014-05-20', 0),
 --('Battlefront 1', 'coop shooter from StarWars universe', 30, '2010-05-19', 0),
 --('Terraria', 'sandbox from independent developers', 15, '2011-05-20', 0)

 --insert into Comments(GameId, Title, Text, Rate, CreationDate)
 --values
 --(1, 'awesome', 'awesome game!!', 4.5, '2022-05-20'),
 --(2, 'nice', 'nice game!!', 4, '2021-05-20'),
 --(3, 'bad', 'bad game!!', 2.5, '2019-05-20'),
 --(2, 'bad', 'bad game!!', 1.5, '2020-05-20')

use MyGamesDb;
delete from Comments;