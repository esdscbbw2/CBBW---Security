USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetDesignations]    Script Date: 21-11-2022 15:35:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [MTR].[master_GetDesignations](@DesignationCode smallint,@IsDeleted smallint)
RETURNS @Return TABLE 
(
	DesignationCode smallint,
	[Description] varchar(100),
	CategoryCode tinyint,
	ServiceType tinyint,
	EmployeeType tinyint,
	HigherDesgnCode smallint,
	AppraisalForm tinyint,
	ShortName varchar(150),
	IsDeleted bit,
	LeaveApplicable bit   
)
AS
-- @DesignationCode : 0 For All
-- @IsDeleted (any number except 0/1) : For ALL
BEGIN   
   INSERT INTO @Return
   SELECT  [DesignationCode],[Description],[CategoryCode],[ServiceType],
   [EmployeeType],[HigherDesgnCode],[AppraisalForm],[ShortName],[IsDeleted],
   [LeaveApplicable]   
   FROM [dbo].[tblDesignations] 
   WHERE 
   CASE WHEN @DesignationCode = 0 THEN @DesignationCode ELSE DesignationCode END = @DesignationCode  
    
	IF(@IsDeleted=0)
	BEGIN 
		DELETE FROM @Return WHERE (IsDeleted=1) 
	END
	ELSE IF(@IsDeleted=1) 
	BEGIN
		DELETE FROM @Return WHERE (IsDeleted=0) OR (IsDeleted IS NULL)
	END
   RETURN  			
END	

GO


