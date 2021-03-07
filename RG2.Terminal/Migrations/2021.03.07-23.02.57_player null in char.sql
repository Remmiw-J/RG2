DROP INDEX IX_Character_PlayerID ON dbo.Character;


ALTER TABLE dbo.Character ALTER COLUMN PlayerID INT NULL;
GO


CREATE INDEX IX_Character_PlayerID ON dbo.Character(PlayerID);