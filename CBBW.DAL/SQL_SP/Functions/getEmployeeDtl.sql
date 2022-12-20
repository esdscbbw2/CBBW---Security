USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [CTV].[getEmployeeDtl]    Script Date: 21-11-2022 16:15:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [CTV].[getEmployeeDtl](@EmployeeNumber INT) 
RETURNS @Return TABLE 
(
	CentreCode INT, CentreName VarChar(100),
	EmployeeNumber INT,EmployeeName VarChar(100)
)	 
AS
BEGIN
	INSERT INTO @Return
	SELECT A.CentreCode,B.CentreName,A.EmployeeNumber,A.Name 
	FROM dbo.viewALLEMployees A
	LEFT JOIN dbo.tblCentreMaster B
	ON A.CentreCode=B.CentreCode
	WHERE EmployeeNumber=@EmployeeNumber
   RETURN					  	
END

GO


