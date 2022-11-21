USE [CBBWDev]
GO
/****** Object:  UserDefinedFunction [RUL].[getToursRules]    Script Date: 21-11-2022 16:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [RUL].[getToursRules]() 
RETURNS @Return TABLE 
(
	ID int NULL,
	EntryDate date NULL,
	EntryTime nvarchar(15) NULL,
	EffectiveDate date NULL    
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT  [ID],[EntryDate],[EntryTime],[EffectiveDate]   
	FROM [RUL].[ToursRules] ORDER BY ID DESC
   RETURN					  	
END
