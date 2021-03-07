DROP INDEX IX_Character_PlayerID ON dbo.Character;


ALTER TABLE dbo.Character DROP CONSTRAINT FK_Character_PlayerID;


ALTER TABLE dbo.Character DROP COLUMN PlayerID;
GO