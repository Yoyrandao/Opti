CREATE PROCEDURE [dbo].[FileParts_AppendToExisting]
  @FilePart VARCHAR(255),
  @BaseFileName VARCHAR(255),
  @Folder VARCHAR(255),
  @CompressionHash VARCHAR(255),
  @EncryptionHash VARCHAR(255),
  @Compressed BIT
AS
  DECLARE @LastSequenceId INT = (SELECT MAX(fp.[Id]) FROM [dbo].[FileParts] fp WHERE fp.[BaseFileName] = @BaseFileName);

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
  ) VALUES (@Folder, @FilePart, @CompressionHash, @EncryptionHash, @LastSequenceId, @BaseFileName, @Compressed, GETUTCDATE(), GETUTCDATE());
RETURN 0
