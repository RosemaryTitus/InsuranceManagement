USE [InsuranceManagementDatabase]
GO

/****** Object:  Table [dbo].[Rules]    Script Date: 22-12-2024 9.26.57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rules](
	[Id] [int] NOT NULL,
	[RGID] [uniqueidentifier] NULL,
	[PolicyType] [varchar](200) NOT NULL,
	[ConditionType] [varchar](100) NOT NULL,
	[ConditionOperator] [varchar](10) NULL,
	[ConditionValue] [varchar](50) NULL,
	[ActionType] [varchar](100) NULL,
	[ActionValue] [decimal](10, 2) NULL,
	[Description] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rules] ADD  DEFAULT (newid()) FOR [RGID]
GO

