-- STORED PROCEDURE
-- returns data from multiple entities
-- UserProfiles and UserRoles
CREATE OR ALTER PROCEDURE sp_GetUserRoles (
    @profileId int
) AS BEGIN
SELECT *
FROM UserProfiles up
INNER JOIN UserRoles ur ON up.UserProfileId = ur.UserProfileId
WHERE up.UserProfileId = @profileId;
END