create table Position (
	PositionId int unsigned not null auto_increment,
	name varchar(100) not null,
	primary key(PositionId)
) CHARACTER SET cp1251;

create table Teams (
	TeamId int unsigned not null auto_increment,
	name varchar(100) not null,
	primary key(TeamId)
) CHARACTER SET cp1251;


create table Coaches (
	ID int unsigned not null,
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Nationality varchar(80),	
	primary key(ID)
	) CHARACTER SET cp1251;
	
create table Players (
	ID int unsigned not null,
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Nationality varchar(80),
	Height varchar(10),
	Weight varchar(10),
	PlayerPosition int unsigned,	
	primary key(ID)
	) CHARACTER SET cp1251;
	
create table Rating (
	PlayerID int unsigned not null,
	CoachID int unsigned not null,
	TeamID int unsigned not null,
	PlayerRating double unsigned not null,		
	primary key(PlayerID, CoachID, TeamID)
	) CHARACTER SET cp1251;
	
insert into Position (PositionId, name) values (1, 'Goalkeeper');
insert into Position (PositionId, name) values (2, 'Defender');
insert into Position (PositionId, name) values (3, 'Midfilder');
insert into Position (PositionId, name) values (4, 'Forward');

insert into Coaches (ID,FirstName, LastName, Nationality) values (100, 'Paul', 'Lambert', 'Scottish');
insert into Coaches (ID,FirstName, LastName, Nationality) values (101, 'Martin', 'O Neill', 'Northern Irish');

insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (201, 'Andreas', 'Weimann', 'Austrian', '188 cm', '76 kg', 4);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (202, 'Christian', 'Benteke', 'Congolese', '190 cm', '83 kg', 4);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (203, 'Fabian', 'Delph', 'English', '174 cm', '60 kg', 3);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (204, 'Jack', 'Grealish', 'English', '175 cm', '68 kg', 3);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (205, 'Ron', 'Vlaar', 'Dutch', '189 cm', '79 kg', 2);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (206, 'Ciaran', 'Clark', 'English', '188 cm', '76 kg', 2);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (207, 'Ciaran', 'Clark', 'English', '188 cm', '76 kg', 2);
insert into Players (ID,FirstName, LastName, Nationality, Height, Weight, PlayerPosition) values (208, 'Bradley', 'Guzan', 'American', '193 cm', '95 kg', 1);

insert into Teams (TeamId, name) values (1, 'Mancherster United');
insert into Teams (TeamId, name) values (2, 'Mancherster City');
insert into Teams (TeamId, name) values (3, 'Chelsea');
insert into Teams (TeamId, name) values (4, 'Burnley');
insert into Teams (TeamId, name) values (5, 'West Ham');
insert into Teams (TeamId, name) values (6, 'Spurs');
insert into Teams (TeamId, name) values (7, 'Newcastle');
insert into Teams (TeamId, name) values (8, 'Arsenal');
insert into Teams (TeamId, name) values (9, 'Everton');     
insert into Teams (TeamId, name) values (10, 'QPR');
insert into Teams (TeamId, name) values (11, 'Stoke City');
insert into Teams (TeamId, name) values (12, 'Liverpool');
insert into Teams (TeamId, name) values (13, 'Swansea');
insert into Teams (TeamId, name) values (14, 'Sunderland');
insert into Teams (TeamId, name) values (15, 'Southampton');

insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (200,100,1,7.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (200,100,2,8.5 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (200,100,14,9.5 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (200,100,7,5.7 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (202,100,2,8.3 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (202,100,3,9.5 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (202,100,4,4.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (202,100,5,3.2 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (207,100,6,3.2 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (207,100,7,7.9 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (207,100,8,8.4 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (207,100,9,6.4 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (205,100,5,7.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (205,100,8,5.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (205,100,12,7.9 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (205,100,3,9.1 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (204,101,10,6.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (204,101,11,9.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (204,101,12,4.6 );
insert into Rating (PlayerID,CoachID,TeamID, PlayerRating) values (204,101,13,7.1 );
