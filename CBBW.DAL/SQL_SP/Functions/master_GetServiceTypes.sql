USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetServiceTypes]    Script Date: 21-11-2022 15:39:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [MTR].[master_GetServiceTypes](@ID INT)
RETURNS @Return TABLE 
(
ID					tinyint,
[Description]		varchar(15)    
)
AS
-- @ID : 0 For All
BEGIN   
   INSERT INTO @Return
   SELECT  [ServiceType],[Description]   
   FROM [dbo].[tblServiceTypes] 
   WHERE 
   CASE WHEN @ID = 0 THEN @ID ELSE ServiceType END = @ID  
    ORDER BY ServiceType
   RETURN  			
END		      



GO


