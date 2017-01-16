USE [srpedroDB]
GO
/****** Object:  StoredProcedure [Trans].[SP_INSERT_SUPPLIER_TRANS]    Script Date: 01/16/2017 15:55:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Insert New Supplier Transaction 
01/16/2017
Russel Vasquez
*/

CREATE PROC [Trans].[SP_INSERT_SUPPLIER_TRANS_ROWS]
@SUPPLIERCODE nvarchar(6),
@SSNUM nvarchar(10),
@ITEMCODE nvarchar(10),
@ITEMQTY float,
@ITEMPRICE float,
@UOM nvarchar(5),
@USERCODE nvarchar(50)
AS
BEGIN

INSERT INTO Trans.Supplier_Rows(SupplierCode, SSNum, ItemCode, ItemQty, ItemPrice, UOM, UserCode)
VALUES(@SUPPLIERCODE, @SSNUM, @ITEMCODE,@ITEMQTY, @ITEMPRICE,@UOM, @USERCODE)

END