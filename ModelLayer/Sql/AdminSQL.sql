use BookStore

create table Admin(
AdminId int primary key identity,
AdminName varchar(255) not null,
AdminEmailId varchar(255) unique not null,
AdminAddress varchar(255) not null,
AdminMobile bigint unique not null,
AdminPassword varchar(255) not null
)



insert into Admin(AdminName,AdminEmailId,AdminAddress,AdminMobile,AdminPassword) values('sahil bambhania','sahilsorathia@gmail','Bhuj,Kutch',9874563210,'sahil11')

select * from Admin


------------ss------------

use BookStore
--stored procedure for Admin Login
create procedure AdminLoginSP(
@AdminEmailId varchar(255),
@AdminPassword varchar(255)
)
As
Begin try
select * from Admin where @AdminEmailId=@AdminEmailId and @AdminPassword=@AdminPassword
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

