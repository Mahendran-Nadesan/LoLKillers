USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetDataDragonVersion]    Script Date: 2022/12/28 17:22:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDataDragonVersion]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ConfigValue
	FROM Config
	WHERE ConfigKey = 'DataDragonVersion'
END
GO


