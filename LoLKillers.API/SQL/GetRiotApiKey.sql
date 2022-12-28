USE [LoLKillers]
GO

/****** Object:  StoredProcedure [dbo].[GetRiotApiKey]    Script Date: 2022/12/28 17:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRiotApiKey]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ConfigValue
	FROM Config
	WHERE ConfigKey = 'RiotAPIKey'
END
GO


