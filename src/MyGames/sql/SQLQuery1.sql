use MyGamesDb;

select * from UserRoles;

insert into Roles(Name) values ('User'), ('Developer'), ('Admin')

delete from Users;


--insert into Comments(GameId, Title, Text, Rate, CreationDate)
 --values
 --(1, 'awesome', 'awesome game!!', 4.5, '2022-05-20'),
 --(2, 'nice', 'nice game!!', 4, '2021-05-20'),
 --(3, 'bad', 'bad game!!', 2.5, '2019-05-20'),
 --(2, 'bad', 'bad game!!', 1.5, '2020-05-20')

 insert into Games(Name, Description, Price, CreationDate, ForAdultsOnly, PictureUrl)
 values
 ('Battlefront 2', 'coop shooter from StarWars universe part 2', 50, '2014-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
 ('Battlefront 1', 'coop shooter from StarWars universe', 30, '2010-05-19', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
 ('Terraria', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
 ('Minecraft', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
 ('Warzone', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
 ('Battlefield', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg'),
  ('POpa', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://imgc.allpostersimages.com/img/posters/star-wars-battlefront-2-game-cover_u-l-f9dh1t0.jpg')

  delete from Games;