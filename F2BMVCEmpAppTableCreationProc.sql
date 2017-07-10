-- ================================================
-- Create Procedure F2BMVCEmpAppTableCreationProc.SQL

-- ================================================
USE [Deepa]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Deepa>
-- Create date: <6/18/2017>
-- Description:	<Employee Table Creation>
-- =============================================
CREATE PROCEDURE [dbo].[F2BMVCEmpAppTableCreationProc]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	CREATE TABLE [dbo].EmployeeDetail (Id INT IDENTITY(1,1) PRIMARY Key, Name NVARCHAR(50) null,Number INT null,EmailId NVARCHAR(100) null,PhoneNumber INT)

	CREATE TABLE [dbo].EmployeeAddress (AddressID INT ,FlatNo INT,street  NVARCHAR(Max),city NVARCHAR(100),State Nvarchar(100),Country NvarChar(100),Pincode INT)

	CREATE TABLE [dbo].EmployeeSalary (SalaryId INT IDENTITY(1,1) ,Salary NVARCHAR(MAX),BankName Nvarchar(50),Currency Nvarchar(50))

	ALTER TABLE [dbo].EmployeeAddress  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAddress_EmployeeDetail] FOREIGN KEY(AddressID)
REFERENCES [dbo].EmployeeDetail ([Id])

	ALTER TABLE [dbo].EmployeeSalary  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalary_EmployeeDetail] FOREIGN KEY(SalaryId)
REFERENCES [dbo].EmployeeDetail ([Id])

ALTER TABLE EmployeeAddresses ADD StateID int not null
ALTER TABLE EmployeeAddresses ADD CityID int not null

ALTER TABLE [dbo].EmployeeAddresses  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAddress_State] FOREIGN KEY(StateID)
REFERENCES [dbo].State ([StateID])

ALTER TABLE [dbo].EmployeeAddresses  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAddress_City] FOREIGN KEY(CityID)
REFERENCES [dbo].city ([CityID])

ALTER TABLE [dbo].EmployeeAddresses  WITH CHECK ADD  CONSTRAINT [PK_EmployeeAddress] Primary Key(AddressID)

END
GO
