/*01 - [sp_CreateCustomer] */

Create procedure sp_CreateCustomer
(
	@UserId uniqueidentifier,
	@Username nvarchar(30),
	@Email nvarchar(20),
	@FirstName nvarchar(20),
	@LastName nvarchar(20),
	@CreatedOn datetime,
	@IsActive bit
)
as
begin
  begin try
    begin tran
		INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive) 
		VALUES (@UserId, @Username, @Email, @FirstName, @LastName, @CreatedOn, @IsActive)
    commit tran
  end try
begin catch
  rollback tran
  select ERROR_MESSAGE()
end catch
end

/*02 - [sp_GetAllCustomer] */

Create procedure sp_GetAllCustomer
as
begin
    select UserId, UserName,Email,FirstName,LastName,CreatedOn,IsActive from [dbo].[Customer]
end

/*03 - [sp_UpdateCustomer] */

Create procedure sp_UpdateCustomer
(
	@UserId uniqueidentifier,
	@Username nvarchar(30),
	@Email nvarchar(20),
	@FirstName nvarchar(20),
	@LastName nvarchar(20),
	@CreatedOn datetime,
	@IsActive bit
)
as
begin
  begin try
    begin tran
		UPDATE Customer SET Username = @UserName, Email = @Email, FirstName = @FirstName, LastName = @LastName, CreatedOn = @CreatedOn, IsActive = @IsActive WHERE UserId = @UserId
    commit tran
  end try
begin catch
  rollback tran
  select ERROR_MESSAGE()
end catch
end

/*04- [sp_Delete_ECommerceDb_Customer]  this is for delete customer*/

Create procedure sp_Delete_ECommerceDb_Customer
    @UserId uniqueidentifier
as
begin
    Delete [dbo].[Customer] where UserId = @UserId
end

/*05 - [sp_Check_Customer_Exsists] */
Create procedure sp_Check_Customer_Exsists
    @UserId uniqueidentifier
as
begin
	SELECT COUNT(*) FROM Customer WHERE UserId = @UserId
end

/*06 - [sp_Check_Customer_Has_Orders] */
Create procedure sp_Check_Customer_Has_Orders
    @UserId uniqueidentifier
as
begin
	SELECT COUNT(*) FROM [Order] WHERE [OrderBy] = @UserId
end

/*07 - [sp_CreateOrder] */
Create procedure sp_CreateOrder
(
	@OrderId uniqueidentifier,
	@ProductId uniqueidentifier,
	@OrderStatus int,
	@OrderType int,
	@OrderBy uniqueidentifier,
	@OrderedOn dateTime,
	@ShippedOn datetime,
	@IsActive bit
)
as
begin
  begin try
    begin tran
		INSERT INTO [Order] (OrderId, ProductId, OrderStatus, OrderType, OrderBy, OrderedOn,ShippedOn, IsActive) 
		VALUES (@OrderId, @ProductId, @OrderStatus, @OrderType, @OrderBy, @OrderedOn, @ShippedOn, @IsActive)
    commit tran
  end try
begin catch
  rollback tran
  select ERROR_MESSAGE()
end catch
end

/*08 - [sp_GetActiveOrders] */
Create Procedure sp_GetActiveOrders
(
   @UserId uniqueidentifier
)
as
begin 
  select ord.*, pro.*,sup.*
  from [Order] ord
  Inner Join Product pro On ord.ProductId = pro.ProductId
  Inner Join Supplier sup On pro.SupplierId = sup.SupplierId
  Where ord.IsActive = 1 and ord.OrderBy = @UserId
end