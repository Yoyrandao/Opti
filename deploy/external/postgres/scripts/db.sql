--DB INITIALIZATION

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";


--TABLES
CREATE TABLE public.user (
  id                    SERIAL      NOT NULL,
  accountUid            UUID         NOT NULL,
  login                 VARCHAR(255) NOT NULL,
  folder                VARCHAR(255) NOT NULL,
  creationTimestamp     TIMESTAMP    NOT NULL,
  modificationTimestamp TIMESTAMP    NOT NULL,
  CONSTRAINT IX_USER_PK PRIMARY KEY(id),
  CONSTRAINT U_LOGIN    UNIQUE(login)
);

CREATE TABLE public.fileParts (
  id                    SERIAL       NOT NULL,
  folder                VARCHAR(255) NOT NULL,
  partName              VARCHAR(255) NOT NULL,
  compressionHash       VARCHAR(255) NOT NULL,
  encryptionHash        VARCHAR(255) NOT NULL,
  parentId              INTEGER,
  baseFileName          VARCHAR(255) NOT NULL,
  compressed            BOOLEAN      DEFAULT false,
  creationTimestamp     TIMESTAMP    NOT NULL,
  modificationTimestamp TIMESTAMP    NOT NULL,
  CONSTRAINT IX_FILEPARTS_PK PRIMARY KEY(id)
);


--PROGRAMMABILITY
CREATE PROCEDURE registerUser(
	_login VARCHAR(255),
	_folder VARCHAR(255)
) LANGUAGE plpgsql AS $$
BEGIN
	INSERT INTO public.user (
		accountuid,
		login,
		folder,
		creationtimestamp,
		modificationtimestamp
	) VALUES (uuid_generate_v4(), _login, _folder, now(), now());
END
$$;


CREATE FUNCTION addFilePart(
	_filePart        VARCHAR(255),
    _baseFileName    VARCHAR(255),
    _folder          VARCHAR(255),
    _compressionHash VARCHAR(255),
    _encryptionHash  VARCHAR(255),
	_parentId        INTEGER,
	_compressed      BOOLEAN
) RETURNS INTEGER LANGUAGE plpgsql AS $$
DECLARE
  _id INTEGER;
BEGIN
	INSERT INTO public.fileparts (
    	folder,
		partname,
    	compressionHash,
    	encryptionHash,
		parentid,
    	baseFileName,
		compressed,
		creationtimestamp,
		modificationtimestamp
	) VALUES (_folder, _filePart, _compressionHash, _encryptionHash, _parentId, _baseFileName, _compressed, now(), now())
  RETURNING id INTO _id;

  RETURN _id;
END
$$;


CREATE PROCEDURE appendFilePart(
	_filePart        VARCHAR(255),
	_baseFileName    VARCHAR(255),
	_folder          VARCHAR(255),
	_compressionHash VARCHAR(255),
	_encryptionHash  VARCHAR(255),
	_compressed      BOOLEAN
) LANGUAGE plpgsql AS $$
DECLARE
	_nextParentId INTEGER;
BEGIN
	SELECT MAX(fp.id) INTO _nextParentId FROM public.fileparts fp WHERE fp.basefilename = _baseFileName;
	
	INSERT INTO public.fileparts (
    folder,
		partname,
    compressionHash,
    encryptionHash,
		parentid,
    baseFileName,
		compressed,
		creationtimestamp,
		modificationtimestamp
	) VALUES (_folder, _filePart, _compressionHash, _encryptionHash, _nextParentId, _baseFileName, _compressed, now(), now());
END
$$;


CREATE PROCEDURE updateFilePart(
	_filePart VARCHAR(255),
	_compressionHash VARCHAR(255),
	_encryptionHash VARCHAR(255)
) LANGUAGE plpgsql AS $$
BEGIN
	UPDATE public.fileparts
	SET compressionHash = _compressionHash,
		encryptionHash = _encryptionHash,
		modificationtimestamp = now()
	WHERE partname = _filePart;
END
$$;


--DATA

INSERT INTO public.user(accountuid, login, folder, creationtimestamp, modificationtimestamp)
VALUES (uuid_generate_v4(), 'aaron', 'aaron', now(), now());

INSERT INTO public.fileparts(folder, partname, compressionHash, encryptionHash, parentid, basefilename, creationtimestamp, modificationtimestamp)
VALUES
	('aaron', 'firstPartName', '4752c38d26b83408af699cdf8841ac6c', '4752c38d26b83408af699cdf8841ac6c', NULL, 'filename.txt', now(), now()),
	('aaron', 'secondPartName', 'f3bc96df7ff110b25feafc47ebc87bb6', 'f3bc96df7ff110b25feafc47ebc87bb6', 1, 'filename.txt', now(), now()),
	('aaron', 'thirdPartName', 'd7d6b3d3b40e84296ee65bad9749eaff', 'd7d6b3d3b40e84296ee65bad9749eaff', 2, 'filename.txt', now(), now()),
	('aaron', 'fourthPartName', '053e58016773918f6bf7c368db303226', '053e58016773918f6bf7c368db303226', 3, 'filename.txt', now(), now());


--PERMISSIONS

GRANT SELECT, INSERT, UPDATE, DELETE
ON ALL TABLES IN SCHEMA public 
TO application; 
