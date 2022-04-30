CREATE TABLE [LUUP].[WorkflowTemplateMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowId] INT NULL,
	[TemplateId] INT NULL,
 PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO
ALTER TABLE [LUUP].[WorkflowTemplateMapping]  WITH CHECK ADD CONSTRAINT FK_WorkflowTemplateMapping_WorkflowId FOREIGN KEY([WorkflowId])
REFERENCES [LUUP].[Workflow] ([Id])
GO
ALTER TABLE [LUUP].[WorkflowTemplateMapping]  WITH CHECK ADD CONSTRAINT FK_WorkflowTemplateMapping_TemplateId FOREIGN KEY([TemplateId])
REFERENCES [LUUP].[Template] ([Id])
GO