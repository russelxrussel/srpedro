USE [srpedroDB]
GO
/****** Object:  StoredProcedure [Trans].[SP_INSERT_BRANCH_TRANS_HDR]    Script Date: 01/21/2017 12:16:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Insert New Branch Transaction 
01/21/2017
Russel Vasquez
*/

ALTER PROC [Trans].[SP_INSERT_BRANCH_TRANS_HDR]
@BRANCHCODE nvarchar(10),
@DOCUMENTDATE datetime,
@RELEASEDATE datetime,
@BSNUM nvarchar(10),
@REMARKS nvarchar(250),
@USERCODE nvarchar(50)
AS
BEGIN

INSERT INTO Trans.Branch_Hdr(BranchCode, DocumentDate, ReleaseDate, BSNum, Remarks, UserCode)
VALUES(@BRANCHCODE, @DOCUMENTDATE, @RELEASEDATE,@BSNUM, @REMARKS, @USERCODE)

END