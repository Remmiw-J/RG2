DROP INDEX IX_PlayerCharacters_ParentID ON dbo.PlayerCharacters;
DROP INDEX IX_PlayerCharacters_CharacterID ON dbo.PlayerCharacters;


ALTER TABLE dbo.PlayerCharacters DROP CONSTRAINT FK_PlayerCharacters_ParentID;
ALTER TABLE dbo.PlayerCharacters DROP CONSTRAINT FK_PlayerCharacters_CharacterID;


ALTER TABLE dbo.Character ADD PlayerID INT NOT NULL CONSTRAINT DF_TEMP_PlayerID DEFAULT 0;
ALTER TABLE dbo.Character DROP CONSTRAINT DF_TEMP_PlayerID;

ALTER TABLE dbo.Player ADD CharactersID INT NULL;

DROP TABLE dbo.PlayerCharacters;
GO


ALTER TABLE dbo.Character ADD CONSTRAINT FK_Character_PlayerID FOREIGN KEY (PlayerID) REFERENCES dbo.Player(ID);

ALTER TABLE dbo.Player ADD CONSTRAINT FK_Player_CharactersID FOREIGN KEY (CharactersID) REFERENCES dbo.Character(ID);


CREATE INDEX IX_Character_PlayerID ON dbo.Character(PlayerID);

CREATE INDEX IX_Player_CharactersID ON dbo.Player(CharactersID);