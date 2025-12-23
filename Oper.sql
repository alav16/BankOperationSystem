USE [BankingOperationsSystem]
GO

/****** Object:  Table [core].[Operations]    Script Date: 12/23/2025 9:32:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [core].[Operations](
	[OperationsId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[OperatonType] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OperationsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [core].[Operations]  WITH CHECK ADD  CONSTRAINT [fk_Operations_Customer] FOREIGN KEY([CustomerId])
REFERENCES [core].[Customer] ([CustomerId])
GO

ALTER TABLE [core].[Operations] CHECK CONSTRAINT [fk_Operations_Customer]
GO


