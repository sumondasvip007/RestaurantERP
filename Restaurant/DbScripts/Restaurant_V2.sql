----- Serially Get Product ---------
USE [RestaurantDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductSoldForDayShift]    Script Date: 10/27/2017 7:50:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ProductSoldForDayShift]  
@storeId int,
@shiftId int,
@fromDate dateTime
AS
BEGIN
SELECT pt.ProductId,pif.ProductName,pif.Unit,pif.UnitPrice,
(((((SELECT ISNULL(sum(p.Quantity),0) from tblProductTransfer p where p.StoreId = @storeId and p.ProductId= pt.ProductId and p.TransferDate<@fromDate and isIn = 'true') - 
(SELECT ISNULL(sum(t.Quantity),0) from tblProductTransfer t Where t.StoreId = @storeId and t.ProductId= pt.ProductId and t.TransferDate<@fromDate and isOut = 'true')) +
((SELECT ISNULL(sum(b.Quantity),0) from tblProductTransfer b where b.StoreId = @storeId and b.ProductId= pt.ProductId and b.ShiftId=@shiftId  and (cast (b.[TransferDate] as date))=@fromDate and isIn = 'true')))) -
((SELECT ISNULL(sum(c.Quantity),0) from tblProductTransfer c where c.StoreId = @storeId and c.ProductId= pt.ProductId and c.ShiftId=@shiftId  and (cast (c.[TransferDate] as date))=@fromDate and isOut = 'true')))AvailableProduct


FROM tblProductTransfer pt
inner join tblProductInformation pif on pif.ProductId=pt.ProductId

where(
(((SELECT ISNULL(sum(p.Quantity),0) from tblProductTransfer p where p.StoreId = @storeId and p.ProductId= pt.ProductId and p.TransferDate<@fromDate and isIn = 'true') - 
(SELECT ISNULL(sum(t.Quantity),0) from tblProductTransfer t Where t.StoreId = @storeId and t.ProductId= pt.ProductId and t.TransferDate<@fromDate and isOut = 'true')) >0

OR
(SELECT ISNULL(sum(b.Quantity),0) from tblProductTransfer b where b.StoreId = @storeId and b.ProductId= pt.ProductId and b.ShiftId=@shiftId  and (cast (b.[TransferDate] as date))=@fromDate and isIn = 'true') >0)
and
(((((SELECT ISNULL(sum(p.Quantity),0) from tblProductTransfer p where p.StoreId = @storeId and p.ProductId= pt.ProductId and p.TransferDate<@fromDate and isIn = 'true') - 
(SELECT ISNULL(sum(t.Quantity),0) from tblProductTransfer t Where t.StoreId = @storeId and t.ProductId= pt.ProductId and t.TransferDate<@fromDate and isOut = 'true')) +
((SELECT ISNULL(sum(b.Quantity),0) from tblProductTransfer b where b.StoreId = @storeId and b.ProductId= pt.ProductId and b.ShiftId=@shiftId  and (cast (b.[TransferDate] as date))=@fromDate and isIn = 'true')))) -
((SELECT ISNULL(sum(c.Quantity),0) from tblProductTransfer c where c.StoreId = @storeId and c.ProductId= pt.ProductId and c.ShiftId=@shiftId  and (cast (c.[TransferDate] as date))=@fromDate and isOut = 'true')))>0 
and (pif.ProductTypeId=2 OR pif.ProductTypeId=3))

group by pt.ProductId,pif.ProductName,pif.Unit,pif.UnitPrice
END
--------------------end-----------------------


