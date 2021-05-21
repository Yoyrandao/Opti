CREATE PROCEDURE [dbo].[FileParts_Update]
  @FilePart VARCHAR(255),
  @CompressionHash VARCHAR(255),
  @EncryptionHash VARCHAR(255)
AS
  UPDATE [dbo].[FileParts]
  SET [CompressionHash] = @CompressionHash,
      [EncryptionHash] = @EncryptionHash,
      [ModificationTimestamp] = GETUTCDATE()
  WHERE [PartName] = @FilePart;
RETURN 0
