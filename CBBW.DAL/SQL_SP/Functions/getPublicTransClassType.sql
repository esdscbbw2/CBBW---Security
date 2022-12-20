USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [RUL].[getPublicTransClassType]    Script Date: 21-11-2022 16:01:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [RUL].[getPublicTransClassType](@id int) 
RETURNS @Return TABLE 
(
	TransTypeID int NULL,
	ID int NULL,
	DisplayText nvarchar(15) NULL,
	IsSelected bit NULL
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT  [PublicTranTypeID],[ClassID],[ClassDescription],0 as IsSelected  
	FROM [MTR].[PublicTransportClassType]
	WHERE [PublicTranTypeID]=@id 
   RETURN					  	
END

GO


