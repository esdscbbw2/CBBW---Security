USE [CBBWDev]
GO

/****** Object:  UserDefinedFunction [MTR].[master_GetEmployees]    Script Date: 21-11-2022 15:36:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [MTR].[master_GetEmployees](@EmployeeNumber int)
RETURNS @Return TABLE 
(	
	LetterNumber varchar(25),
	LetterDate datetime,
	DOJ datetime,
	EmployeeNumber int,
	CentreCode smallint,
	BranchCode int,
	CategoryCode tinyint,
	DepartmentCode smallint,
	SectionCode smallint,
	EmployeeType tinyint,
	ServiceType tinyint,
	ServiceStatus tinyint,
	OfficiatingDesignationCode smallint,
	FatherName varchar(100),
	DOB datetime,
	CertificateType tinyint,
	CertificateNo varchar(25),
	CertificateDate datetime,
	Age tinyint,
	Sex char(1),
	LicenceNumber varchar(25),
	ExpiryDate datetime,
	Name varchar(250),
	NickName varchar(50),
	MaritalStatus tinyint,
	Religion tinyint,
	Nationality tinyint,
	ExtraCurricum varchar(400),
	MotherLanguage tinyint,
	ApplicationNumber int,
	ApplicationDate datetime,
	LocationType tinyint,
	LocationCode int,
	FunctionalDesignation smallint,
	DesignationCode smallint,
	IsActive char(1),
	IsFDActive bit,
	ReportingToFD smallint,
	DeputationLocationType smallint,
	DeputationLocationCode int,
	ConfirmDate datetime,
	DivisionCode tinyint,
	SectorCode smallint,
	GodownCode smallint,
	AreaCode tinyint,
	PANNumber varchar(25),
	IssueDate datetime,
	JoiningBasic numeric(12, 2),
	PresentBasic numeric(12, 2),
	StatusCode tinyint,
	IsMPBuffer bit,
	OwnResidence bit,
	TerminationStatus bit,
	IsSuspended bit,
	RetirementDate datetime,
	SeqNo int,
	BloodGroup varchar(10),
	InActiveNNo varchar(25),
	DataEnteredByEmpNo int,
	DataEnteredBy varchar(100),
	DataEnteredDate datetime,
	HusbandName varchar(100),
	UpdatedReason varchar(500)
)
AS
-- @CentreCode : 0 For All
--@DesignationCode : 0 For All
BEGIN   
   INSERT INTO @Return
   SELECT  [LetterNumber],[LetterDate],[DOJ],[EmployeeNumber],[CentreCode],
   [BranchCode],[CategoryCode],[DepartmentCode],[SectionCode],[EmployeeType],
   [ServiceType],[ServiceStatus],[OfficiatingDesignationCode],[FatherName],
   [DOB],[CertificateType],[CertificateNo],[CertificateDate],[Age],[Sex],
   [LicenceNumber],[ExpiryDate],[Name],[NickName],[MaritalStatus],[Religion],
   [Nationality],[ExtraCurricum],[MotherLanguage],[ApplicationNumber],[ApplicationDate],
   [LocationType],[LocationCode],[FunctionalDesignation],[DesignationCode],
   [IsActive],[IsFDActive],[ReportingToFD],[DeputationLocationType],[DeputationLocationCode],
   [ConfirmDate],[DivisionCode],[SectorCode],[GodownCode],[AreaCode],[PANNumber],
   [IssueDate],[JoiningBasic],[PresentBasic],[StatusCode],[IsMPBuffer],[OwnResidence],
   [TerminationStatus],[IsSuspended],[RetirementDate],[SeqNo],[BloodGroup],[InActiveNNo],
   [DataEnteredByEmpNo],[DataEnteredBy],[DataEnteredDate],[HusbandName],[UpdatedReason]
   FROM [dbo].[tblEmployees] 
   WHERE 
   CASE WHEN @EmployeeNumber = 0 THEN @EmployeeNumber ELSE EmployeeNumber END = @EmployeeNumber
   
   RETURN  			
END	

GO


