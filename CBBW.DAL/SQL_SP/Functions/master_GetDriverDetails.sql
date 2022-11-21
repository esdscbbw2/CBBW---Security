USE [CBBWDev]
GO
/****** Object:  UserDefinedFunction [MTR].[master_GetDriverDetails]    Script Date: 21-11-2022 15:35:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MTR].[master_GetDriverDetails](@EmpType smallint,@DesignationCode int,@IsActive smallint)
RETURNS @Return TABLE 
(
	EmpType				smallint,
	DriverName			varchar(150),
	DOB					datetime,
	DOJ					datetime,
	FName				varchar(50),
	DrivingLicence		varchar(25),
	Placeofissue		varchar(25),
	LicenceRenewalDate	datetime,
	SecDeposit			int,
	ToolsAmount			int,
	NetSD				int,
	TotalRecoverable	int,
	DesignationCode		int,
	IsInActive			bit   
)
AS
-- @EmpType : 0 For All
--@DesignationCode : 0 For All
--@IsActive : any number except 0 n 1 : For All
BEGIN   
   INSERT INTO @Return
   SELECT  EmpType,DriverName,DOB,DOJ,FName,DrivingLicence,Placeofissue,
    LicenceRenewalDate,SecDeposit,ToolsAmount,NetSD,TotalRecoverable,
	DesignationCode,IsInActive
   FROM [dbo].[tblDriverDetails] 
   WHERE 
   CASE WHEN @EmpType = 0 THEN @EmpType ELSE EmpType END = @EmpType AND
   CASE WHEN @DesignationCode = 0 THEN @DesignationCode ELSE DesignationCode END = @DesignationCode
    
	IF(@IsActive=1)
	BEGIN 
		DELETE FROM @Return WHERE (IsInActive=@IsActive) OR (IsInActive IS NULL)
	END
	ELSE IF(@IsActive=0)
	BEGIN
		DELETE FROM @Return WHERE (IsInActive=@IsActive)
	END
   RETURN  			
END	
