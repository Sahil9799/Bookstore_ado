Create database BookStore

use BookStore

create table Users(
UserId int primary key identity,
FullName varchar(255) not null,
EmailId varchar(255) unique not null,
Password varchar(255) not null,
MobileNo bigint unique not null
)

Select * from Users

use BookStore

--stored procedure for UserRegistration
create procedure UserRegistrationSP(
@FullName varchar(255),
@EmailId varchar(255),
@Password varchar(255),
@MobileNo bigint
)
As
Begin try
insert into Users(FullName,EmailId,Password,MobileNo) values(@FullName,@EmailId,@Password,@MobileNo)
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

select * from Users

--======================================================================

--stored procedure for GetAllUsers
create procedure GetAllUsersSP
As
Begin try
select * from Users
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for UserLogin
create procedure UserLoginSP(
@EmailId varchar(255),
@Password varchar(255)
)
As
Begin try
select * from Users where EmailId=@EmailId and Password=@Password
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for ForgotPassword
create procedure ForgotPasswordSP(
@EmailId varchar(255)
)
As
Begin try
select * from Users where EmailId=@EmailId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--======================================================================

--stored procedure for ResetPassword
create procedure ResetPasswordSP(
@EmailId varchar(255),
@Password varchar(255)
)
AS
BEGIN TRY
update Users set Password=@Password where EmailId=@EmailId
END TRY
BEGIN CATCH
SELECT
      ERROR_NUMBER() AS ErrorNumber,
	  ERROR_STATE() AS ErrorState,
	  ERROR_PROCEDURE() AS ErrorProcedure,
	  ERROR_LINE() AS ErrorLine,
	  ERROR_MESSAGE() AS ErrorMessage
END CATCH