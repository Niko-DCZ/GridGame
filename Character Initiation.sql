DELETE FROM Characters
DBCC CHECKIDENT ('Characters', RESEED, 0);
-- Remove "--" to exec the code, this will delete all the rows in the Player table and reset the counter to 0

INSERT INTO Characters  -- Adding into characters table 
(CharacterType, Name, Health, Damage, TravelRange, BlockVal, ImagePath, PlayerID, SplashRange, SplashVal, SpecialVal,Leader)
VALUES  -- the parameters of the characters 
('Character', 'Abraham', 5, 6, 2, 3, 'abraham.jpg', NULL, 0, 0, 0,1),
('Character', 'Jesse', 5,6,3,2,'jesse.jpg',NULL,0,0,0,1),
('RangedK_DAWG', 'Malcom', 5, 50, 3, 1, 'Malcom.jpg', NULL, 2, 0, 0,0),
('Ogre', 'Kendrick', 5, 2, 3, 3, 'kl.jpg', NULL, 0, 0, 4,0), -- ELECTRIC CHAIN
('Goblin', 'hariot', 5, 10, 3, 3, 'hariot.jpg', NULL, 2, 0, 2,0), --ZAAHEN
('Gremlin', 'mlk', 5, 4, 3, 4, 'mlk.jpg', NULL, 2, 0, 4,0);  ---EXPLODE ON DEATH
--lines 4-9 is initiating the character

Select * from characters;