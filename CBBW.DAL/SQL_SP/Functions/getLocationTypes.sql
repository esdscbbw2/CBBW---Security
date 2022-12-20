USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [CTV].[getLocationTypes]    Script Date: 21-11-2022 16:21:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [CTV].[getLocationTypes]() 
RETURNS @Return TABLE 
(
	ID int NULL,
	DisplayText [nvarchar](20) NULL
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT DISTINCT LocationType,LocTypeName FROM dbo.tblLocations ORDER BY LocationType
   RETURN					  	
END

GO


