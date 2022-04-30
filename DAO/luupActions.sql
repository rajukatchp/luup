CREATE TABLE [LUUP].[Actions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[AdaptiveJSON] [json](max) NOT NULL,
	[SuccessJSON] [json](max) NULL,
	[ErrorJSON] [json](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](256) NOT NULL,
	[ModifiedBy] [varchar](256) NOT NULL,
	[IncludeActionInBody] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [LUUP].[Actions] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [LUUP].[Actions] ADD  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO
ALTER TABLE [LUUP].[Actions] ADD  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
ALTER TABLE [LUUP].[Actions] ADD  DEFAULT (suser_sname()) FOR [ModifiedBy]
GO