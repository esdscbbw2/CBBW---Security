USE [CBBWDev]
GO
/****** Object:  UserDefinedFunction [MTR].[master_GetVehicleTypeMaster]    Script Date: 21-11-2022 15:42:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MTR].[master_GetVehicleTypeMaster](@VehicleTypeCode int)
RETURNS @Return TABLE 
(
	VehicleTypeCode int,
	VehicleTypeDescription varchar(50),
	Nature varchar(50)   
)
AS
-- @VehicleTypeCode : 0 For All
BEGIN   
   INSERT INTO @Return
   SELECT  VehicleTypeCode, VehicleTypeDescription, Nature  
   FROM [dbo].[tblVehicleTypeMaster] 
   WHERE 
   CASE WHEN @VehicleTypeCode = 0 THEN @VehicleTypeCode ELSE VehicleTypeCode END = @VehicleTypeCode  
    
   RETURN  			
END	
