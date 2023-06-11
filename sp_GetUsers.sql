-- STORED PROCEDURE
CREATE
OR ALTER PROCEDURE sp_GetUsers(
    @emailAddress VARCHAR(100),
    @passwordHash VARCHAR(100)
) AS BEGIN
SELECT
    *
FROM
    UserProfiles
WHERE
    EmailAddress = @emailAddress
    AND PasswordHash = @passwordHash;

END