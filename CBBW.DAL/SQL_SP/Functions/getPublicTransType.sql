USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [RUL].[getPublicTransType]    Script Date: 21-11-2022 16:02:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [RUL].[getPublicTransType]() 
RETURNS @Return TABLE 
(
	ID int NULL,
	DisplayText [nvarchar](20) NULL
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT  [PublicTranTypeID],[PublicTranTypeDesc]   
	FROM [MTR].[PublicTransportType] 
   RETURN					  	
END

GO


