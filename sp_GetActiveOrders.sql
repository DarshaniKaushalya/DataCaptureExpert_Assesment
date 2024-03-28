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