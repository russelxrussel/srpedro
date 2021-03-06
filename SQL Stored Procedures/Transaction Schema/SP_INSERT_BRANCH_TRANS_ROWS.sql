USE [srpedroDB]
GO
/****** Object:  StoredProcedure [Trans].[SP_INSERT_BRANCH_TRANS_ROWS]    Script Date: 01/23/2017 15:34:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Insert New Supplier Transaction 
01/21/2017
Russel Vasquez
*/

ALTER PROC [Trans].[SP_INSERT_BRANCH_TRANS_ROWS]
@BRANCHCODE nvarchar(10),
@BSNUM nvarchar(10),
@ITEMCODE nvarchar(10),
@ITEMQTY float,
@ITEMPRICE float,
@UOM nvarchar(5),
@USERCODE nvarchar(50)
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION

	INSERT INTO Trans.Branch_Rows(BranchCode, BSNum, ItemCode, ItemQty, ItemPrice, UOM, UserCode)
	VALUES(@BRANCHCODE, @BSNUM, @ITEMCODE,@ITEMQTY, @ITEMPRICE,@UOM, @USERCODE)
	
	--INSERT ALSO THE TRANSACTION ITEM ON INVENTORY TRANSACTION FILES
	INSERT INTO Trans.Inventory(CustomerCode, TransactionNum,ItemCode,Quantity, ItemPrice,UOM,UserCode)
	VALUES(@BRANCHCODE,@BSNUM,@ITEMCODE,@ITEMQTY,@ITEMPRICE,@UOM,@USERCODE)
	
	--UPDATE STOCK INVENTORY BY BRANCH
	EXEC Trans.SP_UPDATE_STOCK_INVENTORY_FROM_BRANCH @ITEMCODE,@ITEMQTY
	

	COMMIT TRANSACTION
END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION -- Will not commit changes on all tables

END CATCH

END