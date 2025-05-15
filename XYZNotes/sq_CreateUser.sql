--SQL--
-- Stored Procedure with Output Parameters
CREATE
OR ALTER PROCEDURE sp_CreateUser(
    @emailAddress VARCHAR(100),
    @passwordHash VARCHAR(100),
    @oidProvider VARCHAR(100),
    @oid VARCHAR(100),
    @Id int out
) AS BEGIN
INSERT INTO
    UserProfiles(
        EmailAddress,
        PasswordHash,
        OIdProvider,
        OId
    )
VALUES
(@emailAddress, @passwordHash, @oidProvider, @oid);

-- the state of the output param is set to the Id of the created user record
SELECT
    @Id = Id
FROM
    UserProfiles
WHERE
    EmailAddress = @emailAddress;

END






--#SQL#
declare @userId as int;

exec sp_CreateUser 'abc@123.com',
'xHefhRAndftKdhdtc',
'Legacy',
NULL,
@userId out
select
    @userId
