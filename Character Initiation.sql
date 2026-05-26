--DELETE FROM Characters
--DBCC CHECKIDENT ('Characters', RESEED, 0);
-- Remove "--" to exec the code, this will delete all the rows in the Player table and reset the counter to 0

INSERT INTO Characters  -- Adding into characters table 
(CharacterType, Name, Health, Damage, TravelRange, BlockVal, ImagePath, PlayerID, SplashRange, SplashVal, SpecialVal)
VALUES  -- the parameters of the characters 
('Character', 'Abraham', 5, 6, 2, 3, 'abraham.jpg', NULL, NULL, NULL, NULL),
('Character', 'Jesse', 5,6,3,2,'jesse.jpg',NULL,NULL,NULL,NULL),
('RangedK_DAWG', 'Malcom', 5, 50, 3, 1, 'Malcom.jpg', NULL, 2, 2, NULL),
('Ogre', 'Kendrick', 5, 2, 3, 3, 'kl.jpg', NULL, NULL, NULL, 4),
('Goblin', 'hariot', 5, 3, 3, 3, 'hariot.jpg', NULL, NULL, NULL, 2),
('Gremlin', 'mlk', 5, 4, 3, 4, 'mlk.jpg', NULL, NULL, NULL, 4);
--lines 4-9 is initiating the character

Select * from characters ;