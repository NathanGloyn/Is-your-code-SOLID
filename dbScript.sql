/****** Object:  StoredProcedure [dbo].[u_AddRecord_i]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_AddRecord_i]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_AddRecord_i]
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteFile1_u]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteFile1_u]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_DeleteFile1_u]
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteFile2_u]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteFile2_u]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_DeleteFile2_u]
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteRecord_d]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteRecord_d]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_DeleteRecord_d]
GO
/****** Object:  StoredProcedure [dbo].[u_GetRecordByID_s]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_GetRecordByID_s]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_GetRecordByID_s]
GO
/****** Object:  StoredProcedure [dbo].[u_UpdateRecord_u]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_UpdateRecord_u]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[u_UpdateRecord_u]
GO
/****** Object:  Table [dbo].[BoxDetails]    Script Date: 01/25/2011 22:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoxDetails]') AND type in (N'U'))
DROP TABLE [dbo].[BoxDetails]
GO
/****** Object:  Table [dbo].[BoxDetails]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoxDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BoxDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[ClientNumber] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[ClientLeader] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[ReviewDate] [smalldatetime] NOT NULL,
	[Comments] [ntext] COLLATE Latin1_General_CI_AS NOT NULL,
	[FileLocation] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[FileLocation2] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[SecureStorage] [bit] NOT NULL,
	[BoxDetails] [ntext] COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_BoxDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[BoxDetails] ON
INSERT [dbo].[BoxDetails] ([ID], [ClientName], [ClientNumber], [ClientLeader], [ReviewDate], [Comments], [FileLocation], [FileLocation2], [SecureStorage], [BoxDetails]) VALUES (1, N'abc', N'123', N'John', CAST(0x9E5E0000 AS SmallDateTime), N'', N'BT.pdf', N'', 0, N'')
INSERT [dbo].[BoxDetails] ([ID], [ClientName], [ClientNumber], [ClientLeader], [ReviewDate], [Comments], [FileLocation], [FileLocation2], [SecureStorage], [BoxDetails]) VALUES (2, N'abc', N'123', N'abc', CAST(0x9E5E0000 AS SmallDateTime), N'', N'', N'', 0, N'')
INSERT [dbo].[BoxDetails] ([ID], [ClientName], [ClientNumber], [ClientLeader], [ReviewDate], [Comments], [FileLocation], [FileLocation2], [SecureStorage], [BoxDetails]) VALUES (7, N'Dodgy Data', N'321', N'Geezer', CAST(0x9FCB0000 AS SmallDateTime), N'', N'', N'', 0, N'')
INSERT [dbo].[BoxDetails] ([ID], [ClientName], [ClientNumber], [ClientLeader], [ReviewDate], [Comments], [FileLocation], [FileLocation2], [SecureStorage], [BoxDetails]) VALUES (8, N'Safe Data', N'123456', N'Simon Safe', CAST(0x9FCB0000 AS SmallDateTime), N'Mr Safe has paid for full year.', N'', N'', 1, N'')
SET IDENTITY_INSERT [dbo].[BoxDetails] OFF
/****** Object:  StoredProcedure [dbo].[u_UpdateRecord_u]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_UpdateRecord_u]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_UpdateRecord_u]
	@ID int,
	@ClientName nvarchar(50),
    @ClientNumber nvarchar(50),
    @ClientLeader nvarchar(50),
    @ReviewDate smalldatetime,
    @Comments ntext,
    @FileLocation varchar(50),
    @FileLocation2 varchar(50),
    @SecureStorage bit,
    @BoxDetails ntext
AS
BEGIN

	SET NOCOUNT ON;

UPDATE [DDD].[dbo].[BoxDetails]
   SET [ClientName] = @ClientName,
      [ClientNumber] = @ClientNumber,
      [ClientLeader] = @ClientLeader,
      [ReviewDate] = @ReviewDate,
      [Comments] = @Comments,
      [FileLocation] = @FileLocation,
      [FileLocation2] = @FileLocation2,
      [SecureStorage] = @SecureStorage,
      [BoxDetails] = @BoxDetails
 WHERE ID = @ID


END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[u_GetRecordByID_s]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_GetRecordByID_s]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_GetRecordByID_s]
	@ID int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT [ID]
		  ,[ClientName]
		  ,[ClientNumber]
		  ,[ClientLeader]
		  ,[ReviewDate]
		  ,[Comments]
		  ,[FileLocation]
		  ,[FileLocation2]
		  ,[SecureStorage]
		  ,[BoxDetails]
	  FROM [dbo].[BoxDetails]
	  WHERE ID = @ID 
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteRecord_d]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteRecord_d]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_DeleteRecord_d]
	@ID int
AS
BEGIN

	SET NOCOUNT ON;

	DELETE [dbo].[BoxDetails]
    WHERE ID = @ID
      
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteFile2_u]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteFile2_u]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_DeleteFile2_u]
	@ID int
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE BoxDetails
	SET FileLocation2 = ''''
	WHERE ID = @ID
      
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[u_DeleteFile1_u]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_DeleteFile1_u]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_DeleteFile1_u]
	@ID int
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE BoxDetails
	SET FileLocation = ''''
	WHERE ID = @ID
      
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[u_AddRecord_i]    Script Date: 01/25/2011 22:18:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[u_AddRecord_i]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[u_AddRecord_i]
	@ClientName nvarchar(50),
    @ClientNumber nvarchar(50),
    @ClientLeader nvarchar(50),
    @ReviewDate smalldatetime,
    @Comments ntext,
    @FileLocation varchar(50),
    @FileLocation2 varchar(50),
    @SecureStorage varchar(50),
    @BoxDetails ntext
AS
BEGIN

	SET NOCOUNT ON;
	
	INSERT [dbo].[BoxDetails]
           ([ClientName]
           ,[ClientNumber]
           ,[ClientLeader]
           ,[ReviewDate]
           ,[Comments]
           ,[FileLocation]
           ,[FileLocation2]
           ,[SecureStorage]
           ,[BoxDetails])
     VALUES
           (
				@ClientName,
				@ClientNumber,
				@ClientLeader,
				@ReviewDate,
				@Comments,
				@FileLocation,
				@FileLocation2,
				@SecureStorage,
				@BoxDetails
			)

	RETURN SCOPE_IDENTITY()

END
' 
END
GO
