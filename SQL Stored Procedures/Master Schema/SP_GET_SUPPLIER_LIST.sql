USE [srpedroDB]
GO
/****** Object:  StoredProcedure [Master].[SP_Search_Supplier]    Script Date: 01/18/2017 10:30:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* User query for Supplier Data 
01/18/2017
*/

CREATE PROC [Master].[SP_GET_SUPPLIER_LIST]
AS
BEGIN

 SELECT *
 FROM Master.Supplier_Data 

END