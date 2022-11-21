USE [CBBWDev]
GO
/****** Object:  UserDefinedFunction [MTR].[master_GetPublicTransportClassType]    Script Date: 21-11-2022 15:37:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [MTR].[master_GetPublicTransportClassType](@PublicTranTypeID int)
RETURNS @Return TABLE 
(
	PublicTranTypeID int NULL,
	ClassID int NULL,
	ClassDescription nvarchar(50) NULL,
	IsActive bit NULL
)
AS
BEGIN   
   INSERT INTO @Return
   SELECT  [PublicTranTypeID],[ClassID],[ClassDescription],[IsActive]  
   FROM [MTR].[PublicTransportClassType] 
   WHERE CASE WHEN @PublicTranTypeID = 0 THEN @PublicTranTypeID ELSE PublicTranTypeID END = @PublicTranTypeID
   RETURN  			
END	

