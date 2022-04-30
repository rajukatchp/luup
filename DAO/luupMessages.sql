CREATE TABLE [LUUP].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[BodyLink] [nvarchar](max) NOT NULL,
	[RecipientsTo] [varchar](100) NOT NULL,
	[RecipientsCC] [varchar](100) NOT NULL,
	[RecipientsBCC] [varchar](100) NOT NULL,
 PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO