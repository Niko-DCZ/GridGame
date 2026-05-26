ALTER TABLE Characters  -- In the table  selected
ADD CONSTRAINT FK_Characters_Players  -- Adding rule 
FOREIGN KEY (PlayerID)  -- Making playerID a foreign key in this table 
REFERENCES Players(PlayerID);  -- Linking it to the Player Table