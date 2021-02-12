CREATE TABLE [dbo].[Vouchers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[VoucherType] [nvarchar](max) NOT NULL,
	[Discount] [float] NOT NULL,
	[MinimumAmount] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
