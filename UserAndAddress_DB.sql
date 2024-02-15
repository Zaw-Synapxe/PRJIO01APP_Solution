﻿CREATE TABLE [dbo].[TB_ADDRESS] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Address]  VARCHAR (100) NOT NULL,
    [UserCode] INT           NULL,
    [Enabled]  BIT           NOT NULL,
    CONSTRAINT [PK_TB_ADDRESS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TB_ADDRESS_TB_USER] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[TB_USER] ([ID])
);

﻿CREATE TABLE [dbo].[TB_USER] (
    [ID]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NOT NULL,
    [Nickname] VARCHAR (50) NOT NULL,
    [RG]       VARCHAR (50) NOT NULL,
    [CPF]      VARCHAR (50) NOT NULL,
    [Enabled]  BIT          NOT NULL,
    CONSTRAINT [PK_TB_USER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UK_TB_USER_CPF] UNIQUE NONCLUSTERED ([CPF] ASC),
    CONSTRAINT [UK_TB_USER_RG] UNIQUE NONCLUSTERED ([RG] ASC)
);


--6 - Database/dbo/User Defined Types/TVP_ADDRESS.sql
CREATE TYPE [dbo].[TVP_ADDRESS] AS TABLE (
    [ID]       INT           NULL,
    [Address]  VARCHAR (100) NULL,
    [UserCode] INT           NULL,
    [Enabled]  BIT           NULL);

--6 - Database/dbo/Stored Procedures/SP_DROP_IF_EXISTS.sql
CREATE PROCEDURE SP_DROP_IF_EXISTS
(
	@Name	VARCHAR(255)
)
AS
BEGIN

	DECLARE @TYPE CHAR(2) = (SELECT TYPE FROM SYS.objects WHERE NAME = @Name)

	SET @TYPE = REPLACE(@TYPE, ' ', '')

	IF(LTRIM(RTRIM(@TYPE)) IN('U','P'))
	BEGIN
		
		DECLARE @CMD VARCHAR(255) =''

		IF(@TYPE = 'U')
		BEGIN
			IF EXISTS(SELECT NAME FROM sys.objects WHERE name=@Name AND type=@TYPE)
			BEGIN
				SET @CMD += 'DROP TABLE '+ @Name
			END
		END
		
		IF(@TYPE = 'P')
		BEGIN
			IF EXISTS(SELECT NAME FROM sys.objects WHERE name=@Name AND type=@TYPE)
			BEGIN
				SET @CMD += 'DROP PROCEDURE '+ @Name
			END
		END

		-- See the output
		PRINT @CMD

		-- Execute the t-sql statement
		EXEC(@CMD)
		
	END
	ELSE
	BEGIN

		IF(@TYPE IS NULL)
		BEGIN
			PRINT 'The current object does not exist in the current database'
		END
		ELSE
		BEGIN
			PRINT 'SQL DBType not allowed at the moment. Type: ' + @TYPE
		END
	END

END

--6 - Database/dbo/Stored Procedures/SP_USER_D.sql
﻿CREATE PROCEDURE SP_USER_D
(
	@P_ID INT
)
AS
BEGIN
 
	DELETE FROM TB_ADDRESS
	WHERE USERCODE = @P_ID

	DELETE FROM TB_USER
	WHERE ID = @P_ID

END

--6 - Database/dbo/Stored Procedures/SP_USER_I.sql
﻿CREATE PROCEDURE SP_USER_I
(
	@P_ID		INT = NULL OUTPUT,
	@P_Name		varchar(50),
	@P_Nickname	varchar(50),
	@P_RG		varchar(50),
	@P_CPF		varchar(50),
	@P_Enabled	bit,	
	@P_TVP_ADDRESS TVP_ADDRESS READONLY
 )
 AS
 BEGIN
 
	IF EXISTS
	(
		SELECT 1 FROM TB_USER WITH(NOLOCK)
		WHERE CPF = @P_CPF
	)
	BEGIN
		RAISERROR ('There is already a user with this CPF.Please, try another one.', 16, 1);
	END
	ELSE
	BEGIN
	
		BEGIN TRY

			BEGIN TRANSACTION

			INSERT TB_USER
			(
				Name,
				Nickname,
				RG,
				CPF,
				Enabled
			)
			VALUES
			(
				@P_Name,
				@P_Nickname,
				@P_RG,
				@P_CPF,
				@P_Enabled
			)

			SET @P_ID = @@IDENTITY

			-- Insert the new ones
			INSERT TB_ADDRESS
			SELECT 
				Address,
				@P_ID,
				Enabled
			FROM @P_TVP_ADDRESS

			COMMIT TRANSACTION

		END TRY
		BEGIN CATCH

			IF(@@TRANCOUNT !=0)
			BEGIN
				ROLLBACK TRANSACTION
			END
			
		END CATCH

	END

END


--6 - Database/dbo/Stored Procedures/SP_USER_S.sql
CREATE PROCEDURE SP_USER_S
AS
BEGIN
 
	SELECT 
		ID,
		Name,
		Nickname,
		RG,
		CPF,
		Enabled
	FROM TB_USER A WITH(NOLOCK)
	ORDER BY Name


 END


 --6 - Database/dbo/Stored Procedures/SP_USER_S_BY_ID.sql
 CREATE PROCEDURE SP_USER_S_BY_ID
(
	@P_ID INT
)
AS
BEGIN
 
	SELECT 
		ID,
		Name,
		Nickname,
		RG,
		CPF,
		Enabled
	FROM TB_USER A WITH(NOLOCK)

	WHERE A.ID = @P_ID

	-- Get all address
	SELECT 
		A.ID,
		A.Address,
		A.Enabled
	FROM TB_ADDRESS A WITH(NOLOCK)

	WHERE A.UserCode = @P_ID

END

--6 - Database/dbo/Stored Procedures/SP_USER_U.sql
CREATE PROCEDURE SP_USER_U
(
	@P_ID INT,
	@P_Name		varchar(50),
	@P_Nickname	varchar(50),
	@P_RG		varchar(50),
	@P_CPF		varchar(50),
	@P_Enabled	bit,	
	@P_TVP_ADDRESS TVP_ADDRESS READONLY
 )
 AS
 BEGIN
 
	BEGIN TRY

		BEGIN TRANSACTION

		UPDATE TB_USER SET
			Name		= @P_Name,
			Nickname	= @P_Nickname,
			RG			= @P_RG,
			CPF			= @P_CPF,			
			Enabled		= @P_Enabled
		WHERE ID = @P_ID
		
		-- Delete the previous address
		DELETE FROM TB_ADDRESS WHERE USERCODE = @P_ID
	
		-- Insert the new ones
		INSERT TB_ADDRESS
		SELECT 
			Address,
			UserCode,
			Enabled
		FROM TVP_ADDRESS			

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		
		IF(@@TRANCOUNT>0)
		BEGIN
			ROLLBACK TRANSACTION
		END

	END CATCH

 END









