-- This file contains SQL statements that will be executed after the build script.

INSERT INTO [dbo].[User] (
    AccountUid,
    [Login],
    Folder,
    CreationTimestamp,
    ModificationTimestamp
) VALUES (NEWID(), 'aaron', 'aaron', GETUTCDATE(), GETUTCDATE());