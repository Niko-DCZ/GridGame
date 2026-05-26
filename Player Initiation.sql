--DELETE FROM Players;
--DBCC CHECKIDENT ('Players', RESEED, 0);
-- Remove "--" to exec the code, this will delete all the rows in the Player table and reset the counter to 0
INSERT INTO Players (PlayerName)
VALUES
('John'),
('Max')

Select * From Players;