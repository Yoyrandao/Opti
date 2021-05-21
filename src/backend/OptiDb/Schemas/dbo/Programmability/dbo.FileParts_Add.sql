CREATE PROCEDURE [dbo].[FileParts_Add]
  @FilePart VARCHAR(255),
  @BaseFileName VARCHAR(255),
  @Folder VARCHAR(255),
  @CompressionHash VARCHAR(255),
  @EncryptionHash VARCHAR(255),
  @ParentId INT,
  @Compressed BIT
AS
  INSERT INTO [dbo].[FileParts] (
    Folder,
    PartName,
    CompressionHash,
    EncryptionHash,
    ParentId,
    BaseFileName,
    Compressed,
    CreationTimestamp,
    ModificationTimestamp
  ) VALUES (@Folder, @FilePart, @CompressionHash, @EncryptionHash, @ParentId, @BaseFileName, @Compressed, GETUTCDATE(), GETUTCDATE());
RETURN @@IDENTITY
