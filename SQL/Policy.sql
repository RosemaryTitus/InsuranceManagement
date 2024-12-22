USE [InsuranceManagementDatabase]
GO

/****** Object:  Table [dbo].[Policy]    Script Date: 22-12-2024 9.28.07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Policy](
	[Id] [int] NOT NULL,
	[RGID] [uniqueidentifier] NULL,
	[PolicyNumber] [varchar](20) NOT NULL,
	[PolicyTypeId] [int] NOT NULL,
	[EffectiveFrom] [datetime] NOT NULL,
	[EffectiveTo] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[PremiumAmount] [decimal](30, 2) NOT NULL,
	[PaymentFrequency] [int] NOT NULL,
	[PolicyTerm] [varchar](200) NOT NULL,
	[LastUpdatedBy] [varchar](50) NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[PolicyType] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Policy] ADD  DEFAULT (newid()) FOR [RGID]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD FOREIGN KEY([PolicyTypeId])
REFERENCES [dbo].[Policy_Type] ([Id])
GO

