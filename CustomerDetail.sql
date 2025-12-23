USE [BankingOperationsSystem]
GO

/****** Object:  Table [core].[CustomerDetail]    Script Date: 12/23/2025 9:32:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [core].[CustomerDetail](
	[CustomerId] [int] NOT NULL,
	[PassportNumber] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [core].[CustomerDetail]  WITH CHECK ADD  CONSTRAINT [fk_CustomerDetail_Customer] FOREIGN KEY([CustomerId])
REFERENCES [core].[Customer] ([CustomerId])
ON DELETE CASCADE
GO

ALTER TABLE [core].[CustomerDetail] CHECK CONSTRAINT [fk_CustomerDetail_Customer]
GO


