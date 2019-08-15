# simple-npgsql-test
### Purpose 
* To test AgensGraph using npgsql driver.

### How To do a setup for the test

* CREATE ROLE AND DATABASE
    * CREATE DATABASE test;
    * CREATE ROLE test WITH PASSWORD 'test';

* ALTER ROLE AND DATABASE 
    * ALTER DATABASE test OWNER TO test;
    * ALTER USER test superuser createdb createrole replication login bypassrls;

* CREATE GRAPH
    * CREATE GRAPH test;
