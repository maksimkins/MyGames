create database MyGames;

use MyGames;

create table Users(
	Id int primary key identity (1, 1),
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
)