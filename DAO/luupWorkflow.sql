DROP TABLE IF EXISTS [LUUP].[Workflow];
GO
CREATE TABLE [LUUP].[Workflow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[workflowName] [varchar](50) NOT NULL,
	[workflowDesc] [varchar](256) NOT NULL,
	[frequency] [varchar](50) NOT NULL,
	[frequencyTime] [datetime] NULL,
	[lastExecutedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](256) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](256) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO
ALTER TABLE [LUUP].[Workflow] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [LUUP].[Workflow] ADD  DEFAULT (getutcdate()) FOR [ModifiedOn]
GO
ALTER TABLE [LUUP].[Workflow] ADD  DEFAULT ((1)) FOR [IsActive]
GO