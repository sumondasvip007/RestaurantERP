-------------Update PurchaseType and Both type Product Price For Main Store status---------------------




USE [RestaurantDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdatePurchaseTypeProductPriceForMainStore]

AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @firstDate dateTime,@lastDate dateTime
	SELECT @firstDate  =  DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) 
	SELECT @lastDate =  DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))
	
	SELECT ProductId, ISNULL(SUM(Quantity * UnitPrice) / (SUM(Quantity)), 0) as price   INTO #temp FROM [dbo].[tblProductTransfer] WHERE SupplierId is not null AND CreatedDateTime >= @firstDate AND CreatedDateTime <=@lastDate GROUP BY ProductId 
	--select * FROm #temp
	UPDATE  tpi 
	SET tpi.ProductionCost = t.price
	FROM [dbo].[tblProductInformation]  as tpi
	INNER JOIN #temp t on t.ProductId = tpi.ProductId
	WHERE ProductTypeId = 3 OR ProductTypeId=1

	DROP TABLE #temp

END



-------------- End-------------------------------