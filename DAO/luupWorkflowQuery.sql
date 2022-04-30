CREATE TABLE [LUUP].[WorkflowQuery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[FilterJSON] [json](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO