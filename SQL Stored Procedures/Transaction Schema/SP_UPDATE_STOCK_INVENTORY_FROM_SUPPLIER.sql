USE [srpedroDB]
GO

/* UPDATE STOCK ITEM IN INVENTORY FROM SUPPLIER 
01/18/2017
Russel Vasquez
*/

ALTER PROC [Trans].[SP_UPDATE_STOCK_INVENTORY_FROM_SUPPLIER]
@ITEMCODE nvarchar(6),
@QTY float
AS
BEGIN

UPDATE master.Inventory_Data
SET InStock = InStock + @QTY, 
RunningStock = RunningStock + @QTY, dateUpdate=GETDATE()
WHERE ItemCode=@ITEMCODE

END