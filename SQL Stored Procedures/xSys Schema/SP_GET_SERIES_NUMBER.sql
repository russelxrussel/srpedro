USE [srpedroDB]
GO

/* GET SERIES NUMBER 
01/18/2017
Russel Vasquez
*/

CREATE PROC [xSys].[SP_GET_SERIES_NUMBER]
@PREFIXCODE nvarchar(5)
AS
BEGIN

SELECT PrefixCode, Series 
FROM xSys.SeriesNumber 
WHERE PrefixCode = @PREFIXCODE

END