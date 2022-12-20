USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetCategoryMaster]    Script Date: 21-11-2022 15:29:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [MTR].[master_GetCategoryMaster](@ID INT)
RETURNS @Return TABLE 
(
ID					tinyint,
[Description]		varchar(25)    
)
AS
-- @ID : 0 For All
BEGIN   
   INSERT INTO @Return
   SELECT  CategoryCode,[Description]   
   FROM [dbo].[tblCategoryMaster] 
   WHERE 
   CASE WHEN @ID = 0 THEN @ID ELSE CategoryCode END = @ID  
    
   RETURN  			
END


GO


