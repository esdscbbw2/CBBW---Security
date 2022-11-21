USE [CBBWDev]
GO
/****** Object:  UserDefinedFunction [MTR].[master_GetZoneMaster]    Script Date: 21-11-2022 15:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MTR].[master_GetZoneMaster](@ZoneCode smallint,@IsDeleted smallint)
RETURNS @Return TABLE 
(
	ZoneCode		smallint,
	ZoneName		varchar(25),
	IsDeleted		bit ,
	EffectiveFrom	datetime 
)
AS
-- @ZoneCode : 0 For All
--@IsDeleted : 3 For All
BEGIN   
   INSERT INTO @Return
   SELECT  ZoneCode, ZoneName, IsDeleted, EffectiveFrom  
   FROM [dbo].[tblZoneMaster] 
   WHERE 
   CASE WHEN @ZoneCode = 0 THEN @ZoneCode ELSE ZoneCode END = @ZoneCode  
    
	if(@IsDeleted=1)
	BEGIN
	DELETE FROM @Return WHERE IsDeleted=0
	END
	if(@IsDeleted=0)
	BEGIN
	DELETE FROM @Return WHERE IsDeleted=1
	END
   RETURN  			
END	

