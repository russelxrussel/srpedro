USE [srpedroDB]
GO

/* User query for Item Data 
01/18/2017
*/

CREATE PROC [Master].[SP_GET_ITEM_LIST]
AS
BEGIN

 SELECT *
 FROM Master.Item_Data 

END