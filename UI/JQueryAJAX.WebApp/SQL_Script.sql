
CREATE TABLE tbl_users(
ID INT IDENTITY(1,1) PRIMARY KEY,
Username VARCHAR(100),
Email VARCHAR(100),
[Password] Varchar(100))



CREATE PROCEDURE sp_register
	@Username VARCHAR(100),
	@Email VARCHAR(100),
	@Password VARCHAR(100),
	@ErrorMessage VARCHAR(300) OUTPUT
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbl_users WHERE Email = @Email)
		BEGIN
			INSERT INTO tbl_users(Username, Email, [Password])
			VALUES (@Username, @Email, @Password);
			SET @ErrorMessage = 'User Created Successfully';
		END
	ELSE
		BEGIN
			SET @ErrorMessage = 'User Already Exists with the same Email';
		END
	
END
GO




CREATE PROCEDURE sp_login
	@Email VARCHAR(100),
	@Password VARCHAR(100)
AS
BEGIN
	SELECT * FROM tbl_users
	WHERE Email = @Email and [Password] = @Password;
END
GO