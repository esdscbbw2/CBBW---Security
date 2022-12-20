USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetVehicleMaster]    Script Date: 21-11-2022 15:41:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [MTR].[master_GetVehicleMaster](@VehicleCode int,@Isactive smallint,@LCVMCVOnly smallint)
RETURNS @Return TABLE 
(
	VehicleCode			int ,
	VehicleType			varchar(50),
	Nature				varchar(50),
	Capacity			numeric(12, 3),
	Manufacturer		varchar(50),
	VehicleNumber		varchar(20),
	ChasisNumber		varchar(100),
	RegistrationDate	datetime,
	RCBookNumber		varchar(50),
	Place				varchar(50),
	MaxServicePeriod	numeric(12, 2),
	LoadType			varchar(50),
	VehicleCost			numeric(18, 2),
	ServiceDuaration	numeric(18, 0),
	FuelType			varchar(50),
	EngineNo			varchar(50),
	TankCapacity		numeric(12, 2),
	IsAtNZB				bit,
	RequiredKMPL		numeric(12, 2),
	EngineOilCapacity	numeric(12, 2),
	IsApproved			bit,
	[Status]			varchar(20),
	TransportMode		int,
	IsScheduled			bit   
)
AS
-- @VehicleCode : 0 For All
--@IsApproved : Any number except 0/1 For ALL
--@IsScheduled : Any number except 0/1 For ALL
--@IsatNZB : Any number except 0/1 For ALL
BEGIN   
   INSERT INTO @Return
   SELECT Vehicle_PKCode,VehicleType,Nature,Capacity,Manufacturer,VehicleNumber,
   ChasisNumber,RegistrationDate,RCBookNumber,Place,MaxServicePeriod,
   LoadType,VehicleCost,ServiceDuaration,FuelType,EngineNo,TankCapacity,
   IsAtNZB,RequiredKMPL,EngineOilCapacity,IsApproved,[Status],TransportMode,
   IsScheduled
   FROM [dbo].[tblVehicleMaster] 
   WHERE 
   CASE WHEN @VehicleCode = 0 THEN @VehicleCode ELSE Vehicle_PKCode END = @VehicleCode  
	
	if(@LCVMCVOnly=1)
	BEGIN
		DELETE FROM @Return WHERE (VehicleType!='LCV')
	END

   RETURN  			
END	


GO


