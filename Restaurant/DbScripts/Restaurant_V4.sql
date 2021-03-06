------------- Add Production Cost  and Total Production Cost-----------------------------
-------------- 2017-11-6 By Sumon---------------------------

USE [RestaurantDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_MainStoreProductStatusQuery]    Script Date: 11/6/2017 2:44:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_MainStoreProductStatusQuery]  
@storeId int
AS
BEGIN
exec [dbo].[sp_UpdatePurchaseTypeProductPriceForMainStore]
SELECT pt.ProductId,pif.ProductName,pif.Unit,ISNULL(pif.ProductionCost,0)ProductionCost,
(SELECT ISNULL(sum(p.Quantity),0) from tblProductTransfer p where p.StoreId = @storeId and p.ProductId= pt.ProductId and isIn = 'true') - 
(SELECT ISNULL(sum(t.Quantity),0) from tblProductTransfer t Where t.StoreId = @storeId and t.ProductId= pt.ProductId and isOut = 'true')AvailableQuantity ,

(((SELECT ISNULL(sum(p.Quantity),0) from tblProductTransfer p where p.StoreId = @storeId and p.ProductId= pt.ProductId and isIn = 'true') - 
(SELECT ISNULL(sum(t.Quantity),0) from tblProductTransfer t Where t.StoreId = @storeId and t.ProductId= pt.ProductId and isOut = 'true')) *
(ISNULL(pif.ProductionCost,0)))TotalProductionCost

FROM tblProductTransfer pt
inner join tblProductInformation pif on pif.ProductId=pt.ProductId

where(((SELECT ISNULL(sum(n.Quantity),0) from tblProductTransfer n where n.StoreId = @storeId and n.ProductId= pt.ProductId and isIn = 'true') - 
(SELECT ISNULL(sum(g.Quantity),0) from tblProductTransfer g Where g.StoreId = @storeId and g.ProductId= pt.ProductId and isOut = 'true'))>0 and (pif.ProductTypeId=1 OR pif.ProductTypeId=3))

group by pt.ProductId,pif.ProductName,pif.Unit,ProductionCost
END


---------------- End----------------------------




