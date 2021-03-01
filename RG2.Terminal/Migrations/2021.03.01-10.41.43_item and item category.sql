CREATE TABLE dbo.Item(
ID INT IDENTITY NOT NULL,
Ticks BIGINT NOT NULL,
CreatedOn DATETIME2 NOT NULL,
Description NVARCHAR(200) NULL,
Name NVARCHAR(200) NOT NULL,
CONSTRAINT PK_dbo_Item PRIMARY KEY CLUSTERED (ID ASC)
);

CREATE TABLE dbo.ItemItemCatagories(
ID INT IDENTITY NOT NULL,
ParentID INT NOT NULL,
ItemCategoryID INT NOT NULL,
CONSTRAINT PK_dbo_ItemItemCatagories PRIMARY KEY CLUSTERED (ID ASC)
);

CREATE TABLE dbo.ItemCategory(
ID INT IDENTITY NOT NULL,
Ticks BIGINT NOT NULL,
Name NVARCHAR(200) NOT NULL,
ItemID INT NULL,
CONSTRAINT PK_dbo_ItemCategory PRIMARY KEY CLUSTERED (ID ASC)
);
GO


ALTER TABLE dbo.ItemItemCatagories ADD CONSTRAINT FK_ItemItemCatagories_ParentID FOREIGN KEY (ParentID) REFERENCES dbo.Item(ID);
ALTER TABLE dbo.ItemItemCatagories ADD CONSTRAINT FK_ItemItemCatagories_ItemCategoryID FOREIGN KEY (ItemCategoryID) REFERENCES dbo.ItemCategory(ID);

ALTER TABLE dbo.ItemCategory ADD CONSTRAINT FK_ItemCategory_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.Item(ID);


CREATE INDEX IX_ItemItemCatagories_ParentID ON dbo.ItemItemCatagories(ParentID);
CREATE INDEX IX_ItemItemCatagories_ItemCategoryID ON dbo.ItemItemCatagories(ItemCategoryID);

CREATE INDEX IX_ItemCategory_ItemID ON dbo.ItemCategory(ItemID);


INSERT INTO framework.Type
  (TableName, CleanName, Namespace, ClassName)
VALUES
 ('dbo.Item', 'Item', 'RG2.Entities', 'Item');

INSERT INTO framework.Type
  (TableName, CleanName, Namespace, ClassName)
VALUES
 ('dbo.ItemCategory', 'ItemCategory', 'RG2.Entities', 'ItemCategory');


INSERT INTO framework.Operation
  ([Key])
VALUES
 ('ItemOperation.Save');

INSERT INTO framework.Operation
  ([Key])
VALUES
 ('ItemCategoryOperation.Save');


INSERT INTO framework.Query
  ([Key])
VALUES
 ('Item');

INSERT INTO framework.Query
  ([Key])
VALUES
 ('ItemCategory');