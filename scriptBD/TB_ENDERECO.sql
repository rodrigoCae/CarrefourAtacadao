USE [Carrefour_Atacadao]
GO

/****** Object:  Table [dbo].[TB_ENDERECO]    Script Date: 15/05/2023 10:19:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_ENDERECO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RUA] [varchar](255) NULL,
	[BAIRRO] [varchar](50) NULL,
	[NUMERO] [varchar](50) NULL,
	[COMPLEMENTO] [varchar](100) NULL,
	[CEP] [varchar](10) NULL,
	[TIPO_ENDERECO] [int] NOT NULL,
	[CIDADE_ID] [int] NOT NULL,
 CONSTRAINT [PK_TB_ENDERECO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_ENDERECO]  WITH CHECK ADD  CONSTRAINT [FK_Cidade] FOREIGN KEY([CIDADE_ID])
REFERENCES [dbo].[TB_CIDADE] ([ID])
GO

ALTER TABLE [dbo].[TB_ENDERECO] CHECK CONSTRAINT [FK_Cidade]
GO

