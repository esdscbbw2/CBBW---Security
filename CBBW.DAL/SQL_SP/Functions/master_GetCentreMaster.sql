USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetCentreMaster]    Script Date: 21-11-2022 15:34:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [MTR].[master_GetCentreMaster](@CentreCode int)
RETURNS @Return TABLE 
(
	RefNoteNumber				char(25),
	CentreCode					int,
	CentreName					varchar(50),
	StartDate					datetime ,
	DistrictCode				smallint,
	SectorCode					smallint,
	ZoneCode					smallint,
	CentreCategory				smallint,
	CentreType					varchar(50),
	PFNumber					varchar(25),
	StorageCapacity				int,
	LicenceNumber				varchar(25),
	IssueDate					datetime,
	AddressCode					smallint,
	AgreementCode				varchar(15),
	CompanyCode					smallint,
	DivisionCode				tinyint,
	AreaCode					tinyint,
	LeafAvailability			varchar(10),
	Accomodation				bit,
	ESIApplicable				bit,
	IsOwnCentre					bit,
	ThumbAttendance				bit,
	Period						tinyint,
	CentreSize					varchar(10),
	PackingType					char(1),
	RollerWiseBidiType			char(1),
	CentreClosed				tinyint,
	CENVATRegNo					varchar(50),
	IsCAPOnline					bit,
	CAPEffectiveFrom			datetime,
	CAPEffectiveTo				datetime,
	BioEffectiveFrom			datetime,
	BioEffectiveTo				datetime,
	CentreCloseEffectiveFrom	datetime 
)
AS
-- @CentreCode : 0 For All
--@DesignationCode : 0 For All
BEGIN   
   INSERT INTO @Return
   SELECT  RefNoteNumber,CentreCode,CentreName,StartDate,DistrictCode,
   SectorCode,ZoneCode,CentreCategory,CentreType,PFNumber,StorageCapacity,
   LicenceNumber,IssueDate,AddressCode,AgreementCode,CompanyCode,DivisionCode,
   AreaCode,LeafAvailability,Accomodation,ESIApplicable,IsOwnCentre,ThumbAttendance,
   Period,CentreSize,PackingType,RollerWiseBidiType,CentreClosed,CENVATRegNo,
   IsCAPOnline,CAPEffectiveFrom,CAPEffectiveTo,BioEffectiveFrom,BioEffectiveTo,
   CentreCloseEffectiveFrom
   FROM [dbo].[tblCentreMaster] 
   WHERE 
   CASE WHEN @CentreCode = 0 THEN @CentreCode ELSE CentreCode END = @CentreCode
   
   RETURN  			
END	


GO


