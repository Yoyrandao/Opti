CREATE TABLE [dbo].[FileParts]
(
  [Id] INT NOT NULL IDENTITY(1,1),
  [Folder] VARCHAR(255) NOT NULL,
  [PartName] VARCHAR(255) NOT NULL,
  [CompressionHash] VARCHAR(255) NOT NULL,
  [EncryptionHash] VARCHAR(255) NOT NULL,
  [ParentId] INT,
  [BaseFileName] VARCHAR(255) NOT NULL,
  [Compressed] BIT CONSTRAINT DF_FileParts_Compressed DEFAULT 0,
  [CreationTimestamp] DATETIME NOT NULL,
  [ModificationTimestamp] DATETIME NOT NULL,
  CONSTRAINT PK_FileParts_Id PRIMARY KEY CLUSTERED ([Id])
);