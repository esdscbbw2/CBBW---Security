USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [CTV].[GetDistanceinKm]    Script Date: 21-11-2022 16:14:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- SELECT [dbo].[fn_PurChangedType](Null,Null)

-- SELECT dbo.fn_PurchaseType('L',default)
-- SELECT dbo.fn_PurchaseType('L','N')

CREATE FUNCTION [CTV].[GetDistanceinKm](@FromLocation SMALLINT,@ToLocationType SMALLINT,@ToLocation SMALLINT)
RETURNS @Return TABLE 
(
Distance Float
)
BEGIN
	DECLARE @Distance NUMERIC(7, 2);
	SELECT @Distance=Distance FROM [dbo].[tblCentreDistance] 
	WHERE CentreCode=@FromLocation AND LocationCode=@ToLocation
	AND LocationType=@ToLocationType;

	IF @Distance >0 BEGIN SET @Distance=@Distance END
	ELSE BEGIN SET @Distance=0 END

	INSERT INTO @Return (Distance) VALUES (@Distance);

RETURN
END  



GO


