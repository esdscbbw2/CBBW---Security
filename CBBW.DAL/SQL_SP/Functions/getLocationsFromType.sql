USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [CTV].[getLocationsFromType]    Script Date: 21-11-2022 16:24:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [CTV].[getLocationsFromType](@typeid INT) 
RETURNS @Return TABLE 
(
	ID int NULL,
	DisplayText [nvarchar](50) NULL
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT LocationCode,LocCodeName FROM dbo.tblLocations WHERE LocationType=@typeid
   RETURN					  	
END

GO


