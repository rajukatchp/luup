CREATE TABLE [LUUP].[Template](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](256) NOT NULL,
	[ModifiedBy] [varchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TemplateType] [varchar](50) NULL,
	[ActionId] [int] NULL,
	[RecipientsTo] [varchar](100) NOT NULL,
	[RecipientsCC] [varchar](100) NOT NULL,
	[RecipientsBCC] [varchar](100) NOT NULL,
 PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO
ALTER TABLE [LUUP].[Template] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [LUUP].[Template] ADD  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO
ALTER TABLE [LUUP].[Template] ADD  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
ALTER TABLE [LUUP].[Template] ADD  DEFAULT (suser_sname()) FOR [ModifiedBy]
GO
ALTER TABLE [LUUP].[Template] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [LUUP].[Template]  WITH CHECK ADD CONSTRAINT FK_Template_ActionId FOREIGN KEY([ActionId])
REFERENCES [LUUP].[Actions] ([Id])
GO