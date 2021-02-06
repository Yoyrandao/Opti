DO $$
DECLARE
  db_name varchar(255) := 'opti_db';
BEGIN
  IF EXISTS (SELECT * FROM pg_database WHERE datname = db_name) THEN
    UPDATE pg_database
      SET datallowconn = 'false'
    WHERE datname = db_name;

    PERFORM pg_terminate_backend(pid)
    FROM pg_stat_activity
    WHERE datname = db_name;
  END IF;
END;
$$;


DROP DATABASE IF EXISTS opti_db;
CREATE DATABASE opti_db;