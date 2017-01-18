USE [srpedroDB]
GO

/* UPDATE SERIES NUMBER 
01/18/2017
Russel Vasquez
*/

CREATE PROC [xSys].[SP_UPDATE_SERIES_NUMBER]
@PREFIXCODE nvarchar(5)
AS
BEGIN

UPDATE xSys.SeriesNumber
SET Series = Series + 1
WHERE PrefixCode=@PREFIXCODE

END