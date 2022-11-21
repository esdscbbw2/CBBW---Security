USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetPublicTransportType]    Script Date: 21-11-2022 15:39:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [MTR].[master_GetPublicTransportType]()
RETURNS @Return TABLE 
(
	ID		int,
	[Description] varchar(20)
)
AS
BEGIN   
   INSERT INTO @Return
   SELECT  [PublicTranTypeID],[PublicTranTypeDesc]  
   FROM [MTR].[PublicTransportType] 
   
   RETURN  			
END	


GO


