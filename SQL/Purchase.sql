USE [InsuranceManagementDatabase]
GO

/****** Object:  Table [dbo].[Purchase]    Script Date: 22-12-2024 9.27.26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Purchase](
	[Id] [int] NOT NULL,
	[RGID] [uniqueidentifier] NULL,
	[CustomerId] [int] NOT NULL,
	[PolicyId] [int] NOT NULL,
	[PolicyType] [varchar](200) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[TotalPremiumAmount] [decimal](30, 2) NOT NULL,
	[Status] [bit] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[RuleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Purchase] ADD  DEFAULT (newid()) FOR [RGID]
GO

ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policy] ([Id])
GO

