USE [InsuranceManagementDatabase]
GO

/****** Object:  Table [dbo].[Policy_Type]    Script Date: 22-12-2024 9.27.45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Policy_Type](
	[Id] [int] NOT NULL,
	[RGID] [uniqueidentifier] NOT NULL,
	[TypeName] [varchar](200) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[EffectiveFrom] [datetime] NOT NULL,
	[EffectiveTo] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[LastUpdatedBy] [varchar](50) NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Policy_Type] ADD  DEFAULT (newid()) FOR [RGID]
GO

