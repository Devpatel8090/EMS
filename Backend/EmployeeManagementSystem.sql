
USE EmployeeManagementSystem


CREATE TABLE tblDepartment(
Id BIGINT PRIMARY KEY IDENTITY(1,1),
DepartmentName NVARCHAR(20) NOT NULL UNIQUE,
)

ALTER TABLE tblDepartment
ADD UNIQUE (DepartmentName);


CREATE TABLE tblEmployee(

Id BIGINT PRIMARY KEY IDENTITY(1,1),
FirstName NVARCHAR(20) NOT NULL,
LastName NVARCHAR(20) ,
Email NVARCHAR(30) NOT NULL UNIQUE,
Salary Money ,
Tax INT,
DepartmentId BIGINT NOT NULL,
EmployeeNumber NVARCHAR(20) NOT NULL UNIQUE,
JoinDate DATE NOT NULL,
FOREIGN KEY(DepartmentId) REFERENCES tblDepartment(Id)
)

CREATE TABLE tblCredentials(

 Email NVARCHAR(30) NOT NULL UNIQUE,
 Password NVARCHAR(20) NOT NULL,
 Role NVARCHAR(10) CHECK(Role='Admin' or Role='Employee')
FOREIGN KEY(Email) REFERENCES tblEmployee(Email)
)

alter FUNCTION GetEmployeeNumber ()
RETURNS NVARCHAR(20) AS
BEGIN

	DECLARE @prevString nvarchar(20)
	Declare @startString nvarchar(20) = N'Emp'  
    Declare @StringRight nvarchar(20) 
    Declare @FinalString nvarchar(20)
    declare @finalno as nvarchar(20)

	SELECT @prevString =  EmployeeNumber	FROM tblEmployee  ORDER BY Id ASC
    SET @StringRight =  RIGHT(@prevString, 5)
	set @finalno = right('00000'+ CONVERT(VARCHAR,(@StringRight + 1)),5)

	set @FinalString = @startString + @finalno
    RETURN @FinalString
END;

select [dbo].GetEmployeeNumber()



CREATE TABLE tblLeaveRequest(

Id BIGINT PRIMARY KEY IDENTITY(1,1),
EmployeeNumber NVARCHAR(20) NOT NULL,
ReasonForLeave  NVARCHAR(200) NOT NULL,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
Duration int NOT NULL,
PhoneNumber BIGINT NOT NULL,
IsAvailableOnPhone BIT NOT NULL DEFAULT 0,
AlternateContactNumber BIGINT,
IsAvailableInCity  BIT NOT NULL DEFAULT 0,
Status NVARCHAR(10) NOT NULL  CHECK(Status='Pending' or Status='Approved' or Status='Denied')

FOREIGN KEY(EmployeeNumber) REFERENCES tblEmployee(EmployeeNumber)
)

DROP TABLE tblPayroll

CREATE TABLE tblPayroll(

Id BIGINT PRIMARY KEY IDENTITY(1,1),
EmployeeNumber NVARCHAR(20) NOT NULL ,
Salary money,
Tax int,
TaxDeducted money,
OnHandSalary money,
TotalLeave int,
AdditionalDeduction money,
month int,
year int,

FOREIGN KEY(EmployeeNumber) REFERENCES tblEmployee(EmployeeNumber)
)


alter PROCEDURE tblEmployee_GetAll
AS
BEGIN
	SELECT TE.Id,FirstName,LastName,Email,Salary,DepartmentId,EmployeeNumber,JoinDate,Tax,TD.DepartmentName
	FROM tblEmployee TE INNER JOIN tblDepartment TD ON TE.DepartmentId = TD.Id 

END




ALTER PROCEDURE tblEmployee_GetEmployeeById
@Email NVARCHAR(30)
AS
BEGIN
	SELECT TC.Password,TF.*
	FROM tblCredentials TC
	INNER JOIN 
	(SELECT TE.Id,FirstName,LastName,Email,Salary,DepartmentId,EmployeeNumber,JoinDate,Tax,TD.DepartmentName
	FROM tblEmployee TE INNER JOIN tblDepartment TD ON TE.DepartmentId = TD.Id 
	WHERE TE.Email = @Email) AS TF
	ON TF.Email = TC.Email

END

CREATE PROCEDURE tblEmployee_DeleteEmp
@EmpId AS BIGINT
AS
BEGIN
	DELETE 
	FROM tblEmployee
	WHERE Id= @EmpId
END


alter PROCEDURE tblEmployee_AddOrUpdateEmployee
@FirstName NVARCHAR(20),
@LastName NVARCHAR(20),
@Email NVARCHAR(30),
@Salary MONEY,
@DepartmentId BIGINT,
@JoinDate DATE,
@Tax Int,
@Id  BIGINT = 0

AS
BEGIN
  IF(@Id > 0)
  BEGIN
	UPDATE tblEmployee
	SET FirstName = @FirstName,LastName= @LastName,Email=@Email,Salary=@Salary,DepartmentId= @DepartmentId,JoinDate= @JoinDate,Tax = @Tax
	WHERE Id=@ID 
  END 
  ELSE
  BEGIN
	INSERT INTO tblEmployee(FirstName,LastName,Email,Salary,Tax,DepartmentId,JoinDate)
		VALUES (@FirstName,@LastName,@Email,@Salary,@Tax,@DepartmentId,@JoinDate)
  END
END

CREATE PROCEDURE tblDepartment_GetAll
AS
BEGIN
	SELECT Id,DepartmentName
	FROM tblDepartment 

END


ALTER PROCEDURE tblCredentials_SignIn
@Email AS NVARCHAR(30),
@Password AS NVARCHAR(30)
--@Message AS NVARCHAR(30) OUTPUT
AS
Declare @Message AS NVARCHAR(100)
BEGIN
	IF(@Email IS NOT NULL AND @Password IS NOT NULL)
	BEGIN
		IF EXISTS(SELECT Role FROM tblCredentials WHERE Email = @Email AND Password = @Password)
		BEGIN			
			SELECT @Message = Role FROM tblCredentials WHERE  Email = @Email AND Password = @Password
		END
		ELSE 
		BEGIN
			SELECT @Message = 'InvalidInformation'
			
		END
	END
	ELSE
	BEGIN
		SELECT @Message = 'EmailDoesNotExist' 
	END

	SELECT @Message AS message
END

exec tblCredentials_SignIn 'j@gmail.com','j@123'


alter PROCEDURE tblLeaveRequest_AddLeave
@EmployeeNumber NVARCHAR(20),
@ReasonForLeave NVARCHAR(200),
@LeaveStartDate date,
@LeaveEndDate date,
@DurationOfLeave int,
@PhoneNumber bigint,
@IsAvailableOnCall bit,
@AlternatePhoneNumber bigint,
@IsAvailableInCity  bit


AS
BEGIN
	INSERT INTO tblLeaveRequest(EmployeeNumber,ReasonForLeave,LeaveStartDate,LeaveEndDate,DurationOfLeave,PhoneNumber,IsAvailableOnCall,AlternatePhoneNumber,IsAvailableInCity)
		VALUES (@EmployeeNumber,@ReasonForLeave,@LeaveStartDate,@LeaveEndDate,@DurationOfLeave,@PhoneNumber,@IsAvailableOnCall,@AlternatePhoneNumber,@IsAvailableInCity)
END


ALTER PROCEDURE tblLeaveRequest_GetEmployeeLeavesByEmail
@Email AS NVARCHAR(30)
AS
BEGIN
	SELECT TL.Id,TL.EmployeeNumber,TL.ReasonForLeave,TL.LeaveStartDate,TL.LeaveEndDate,TL.Status
	FROM tblLeaveRequest TL 
	INNER JOIN tblEmployee TE 
	ON TL.EmployeeNumber = TE.EmployeeNumber
	WHERE TE.Email = @Email
END


ALTER PROCEDURE tblLeaveRequest_GetAllPendingLeaves
AS
BEGIN
	SELECT Id,EmployeeNumber,ReasonForLeave,LeaveStartDate,LeaveEndDate,DurationOfLeave,PhoneNumber,IsAvailableInCity,AlternatePhoneNumber,IsAvailableOnCall,Status
	FROM tblLeaveRequest
	WHERE Status = 'Pending'
END





CREATE PROCEDURE tblLeaveRequest_ApproveLeave
@id BIGINT
AS
BEGIN
	UPDATE tblLeaveRequest
	SET Status = 'Approved'
	WHERE Id = @id
END



CREATE PROCEDURE tblLeaveRequest_DenyLeave
@id BIGINT
AS
BEGIN
	UPDATE tblLeaveRequest
	SET Status = 'Denied'
	WHERE Id = @id
END


Alter PROCEDURE tblPayroll_GetDataIntoPayroll
@Month int,
@Year int
AS 
BEGIN
	Declare @TableName nvarchar(20);
	IF EXISTS (SELECT * FROM tblPayroll WHERE Month = @Month AND Year = @Year)
	BEGIN
	--do what you need if exists
		--DECLARE @LastMonth int;
		--SELECT TOP 1 @LastMonth =  MONTH FROM tblPayroll ORDER BY MONTH DESC;

		SELECT Id,EmployeeNumber,Salary,TaxDeducted,OnHandSalary,Month,Year,Tax,TotalLeave,AdditionalDeduction
		FROM tblPayroll
		WHERE Month = @Month AND Year = @Year

		SELECT @TableName = 'PayrollTable'
	END
	ELSE
	BEGIN
	--do what needs to be done if not

		--SELECT COUNT(*) AS TotalLeave,EmployeeNumber
		--INTO #TempLeaveCount
		--FROM tblLeaveRequest
		--WHERE Status = 'Approved' && Month(d)
		--GROUP BY EmployeeNumber
		
		
		 SELECT TE.EmployeeNumber,TE.Salary,TE.Tax,ISNULL(SUBQUERY.TotalLeaves,0) AS TotalLeaves,@Month As Month,@Year As Year
		 FROM tblEmployee TE
		 LEFT JOIN (
		 SELECT SUM(TL.DurationOfLeave) AS TotalLeaves,TL.EmployeeNumber
		 FROM tblLeaveRequest TL RIGHT JOIN tblEmployee TE2
		 ON TL.EmployeeNumber = TE2.EmployeeNumber
		 WHERE YEAR(TL.LeaveStartDate) = @Year AND MONTH(TL.LeaveStartDate) = @Month
		 GROUP BY TL.EmployeeNumber) AS SUBQUERY
		 ON SUBQUERY.EmployeeNumber = TE.EmployeeNumber

		SELECT @TableName = 'EmployeeTable'
	END 
	SELECT @TableName AS TableName
END

EXECUTE tblPayroll_GetDataIntoPayroll 9,2023



ALTER PROCEDURE tblPayroll_InsertDataIntoPayroll
@Month int,
@Year int
AS 
BEGIN 
	INSERT INTO tblPayroll(EmployeeNumber,Salary,Tax,TaxDeducted,OnHandSalary,TotalLeave,AdditionalDeduction,month,year)
	SELECT TE.EmployeeNumber,TE.Salary,TE.Tax,
	-- Tax Deducted
	 ((((TE.Salary/12) - ((TE.Salary/12/28)*ISNULL(TL.TotalLeaves,0))) * TE.Tax)/100) ,
	 -- On hand Salary
	 (((TE.Salary/12) - ((TE.Salary/12) - ((TE.Salary/12/28)*ISNULL(TL.TotalLeaves,0))) * TE.Tax)/100),
	-- total leaves
		ISNULL(TL.TotalLeaves,0),
	-- additionalDeduction
	 ((TE.Salary/12) - ((TE.Salary/12/28)*ISNULL(TL.TotalLeaves,0))),
	
	
	@Month ,@Year 
	FROM tblEmployee TE
	LEFT JOIN (
	SELECT ISNULL(SUM(LR.DurationOfLeave),0) As TotalLeaves,LR.EmployeeNumber
	FROM tblLeaveRequest LR	
	right JOIN tblEmployee te2 ON LR.EmployeeNumber=te2.EmployeeNumber				
    WHERE YEAR(LR.LeaveStartDate) = @Year AND MONTH(LR.LeaveStartDate) = @Month
	GROUP BY LR.EmployeeNumber)
	AS TL ON TE.EmployeeNumber = TL.EmployeeNumber

END

--DECLARE @i int = 0

--WHILE @i < 20
--BEGIN
--    SET @i = @i + 1
--    /* do some work */
--END



ALTER PROCEDURE tblPayroll_GetMonthOfLastSalryPaid
@Year int
AS 
BEGIN 
--	SELECT ROW_NUMBER() OVER (
--     PARTITION BY month
--     ORDER BY month DESC
--) as rowNumber,*
	SELECT top 1 month AS LastPaidMonth
	FROM tblPayroll
	WHERE year=@Year
	ORDER BY month DESC;
END

EXECUTE tblPayroll_GetMonthOfLastSalryPaid 2023


