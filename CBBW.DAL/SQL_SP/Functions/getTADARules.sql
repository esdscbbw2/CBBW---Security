USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [RUL].[getTADARules]    Script Date: 21-11-2022 16:04:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [RUL].[getTADARules]() 
RETURNS @Return TABLE 
(
	ID int NULL,
	EntryDate date NULL,
	EntryTime nvarchar(15) NULL,
	EffectiveDate date NULL,
	ConnectingID nvarchar(max)    
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT  [ID],CONVERT(date, EntryDate,105) as EntDate,[EntryTime],[EffectiveDate],[ConnectingID]   
	FROM [RUL].[TADARules] ORDER BY ID DESC
   RETURN					  	
END

GO


