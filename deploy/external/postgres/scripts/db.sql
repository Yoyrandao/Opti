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
  hash                  VARCHAR(255) NOT NULL,
  parentId              INTEGER,
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
	_filePart   VARCHAR(255),
  _folder     VARCHAR(255),
  _hash       VARCHAR(255),
	_parentId   INTEGER,
	_compressed BOOLEAN
) RETURNS INTEGER LANGUAGE plpgsql AS $$
DECLARE
  _id INTEGER;
BEGIN
	INSERT INTO public.fileparts (
    folder,
		partname,
    hash,
		parentid,
		compressed,
		creationtimestamp,
		modificationtimestamp
	) VALUES (_folder, _filePart, _hash, _parentId, _compressed, now(), now())
  RETURNING id INTO _id;

  RETURN _id;
END
$$;


--DATA

insert into public.user(accountuid, login, folder, creationtimestamp, modificationtimestamp)
values (uuid_generate_v4(), 'Aaron', 'aaron', now(), now());


--PERMISSIONS

GRANT SELECT, INSERT, UPDATE, DELETE
ON ALL TABLES IN SCHEMA public 
TO application; 
