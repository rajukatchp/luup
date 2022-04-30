CREATE TABLE [LUUP].[WorkflowSchedule](
	[scheduleId] [int] IDENTITY(1,1) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[workflowId] [int] NOT NULL,
	[startedOn] [datetime] NULL,
	[endedOn] [datetime] NULL,
 CONSTRAINT [PK_WorkflowSchedule] PRIMARY KEY CLUSTERED 
(
	[scheduleId] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
GO
ALTER TABLE [LUUP].[WorkflowSchedule] ADD  DEFAULT (getutcdate()) FOR [startedOn]
GO