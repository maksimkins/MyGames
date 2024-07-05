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

 use MyGamesDb;

select * from AspNetUsers;

 insert into Games(Rate, Name, Description, Price, CreationDate, ForAdultsOnly, PictureUrl)
 values
 (3.5,'Hogwarts Legacy', 'Хогвартс. Наследие – это ролевая игра с открытым миром. Теперь вы можете контролировать свои действия и стать центральным героем собственных приключений в волшебном мире.', 50, '2012-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/990080/header.jpg?t=1717689083'),
 (4.5, 'PAYDAY 2', 'PAYDAY 2 - это кооперативный экшн-шутер для четверых игроков, который снова позволяет игрокам надеть маски оригинальной банды PAYDAY - Даллас, Хокстон, Чейнс и Вулф, которые прибыли в Вашингтон для новой крутой серии преступлений.', 5, '2017-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/218620/header_alt_assets_1.jpg?t=1718698928'),
 (3.2, 'PAYDAY 3', 'PAYDAY 3 is the much anticipated sequel to one of the most popular co-op shooters ever. Since its release, PAYDAY-players have been reveling in the thrill of a perfectly planned and executed heist. That’s what makes PAYDAY a high-octane, co-op FPS experience without equal.', 50, '2005-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1272080/header_alt_assets_4.jpg?t=1720106055'),
 (4.6, 'Metro Exodus', 'Оставьте позади руины московского метро и снова отправляйтесь в путешествие по постапокалиптическим землям России. Вас ждут большие нелинейные уровни, открытый мир и захватывающая сюжетная линия.', 17, '2007-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/412020/header.jpg?t=1706778291'),
 (3.2, 'Baldur Gate 3', 'Соберите отряд и вернитесь в Забытые Королевства. Вас ждет история о дружбе и предательстве, выживании и самопожертвовании, о сладком зове абсолютной власти.', 50, '2024-07-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/header.jpg?t=1713271288'),
 (5, 'LEGO Star Wars', 'Kick Some Brick in I through VI! Play through all six Star Wars movies in one videogame! Adding new characters, new levels, new features and for the first time ever, the chance to build and battle your way through a fun Star Wars galaxy on your PC!', 50, '2003-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/32440/header.jpg?t=1604517910'),
 (4, 'Devil May Cry 5', 'Лучший охотник на демонов возвращается в новом стильном боевике.', 18, '2024-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/601150/header.jpg?t=1701395090')




 ('ELDEN RING Shadow of the Erdtree', 'iga-bombma', 50, '2014-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2778580/header_alt_assets_1.jpg?t=1720108810'),
 ('Sons Of The Forest', 'Sent to find a missing billionaire on a remote island, you find yourself in a cannibal-infested hellscape. Craft, build, and struggle to survive, alone or with friends, in this terrifying new open-world survival horror simulator.', 30, '2010-05-19', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1326470/header.jpg?t=1708624856'),
 ('Terraria', 'sandbox from independent developers', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/105600/header.jpg?t=1666290860'),
 ('The Elder Scrolls V: Skyrim Special Edition', 'В игре The Elder Scrolls V: Skyrim Special Edition, получившей более 200 наград «Игра года», вас ждет удивительный мир, воссозданный с потрясающей детализацией. В издание Special Edition вошли базовая игра и дополнения, добавляющие в нее ряд новых возможностей.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/489830/header.jpg?t=1717972262'),
 ('Battlefield 2042', 'Сезон 7: «Точка перелома». Всё или ничего. Battlefield™ 2042 — это шутер от первого лица, в котором серия возвращается к легендарным масштабным сражениям.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1517290/header.jpg?t=1718115687'),
 ('Grand Theft Auto V', 'Grand Theft Auto V для PC позволяет игрокам исследовать знаменитый мир Лос-Сантоса и округа Блэйн в разрешении до 4k и выше с частотой 60 кадров в секунду.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/271590/header.jpg?t=1716224849'),
 ('Sid Meier’s Civilization VI', 'Сыграйте за одного из 20 лидеров – например за Петра Великого, российского императора.', 15, '2011-05-20', 0, 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/289070/header.jpg?t=1719520634')

  delete from Games;

select * from Games;
