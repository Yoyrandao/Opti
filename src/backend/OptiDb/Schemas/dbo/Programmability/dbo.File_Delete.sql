CREATE PROCEDURE [dbo].[File_Delete]
  @BaseFileName VARCHAR(255)
AS
  DELETE FROM [dbo].[FileParts] WHERE [BaseFileName] = @BaseFileName;
RETURN 0
