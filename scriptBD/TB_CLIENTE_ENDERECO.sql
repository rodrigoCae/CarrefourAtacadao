USE [Carrefour_Atacadao]
GO

/****** Object:  Table [dbo].[TB_CLIENTE_ENDERECO]    Script Date: 15/05/2023 10:18:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_CLIENTE_ENDERECO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CLIENTE_ID] [int] NOT NULL,
	[ENDERECO_ID] [int] NOT NULL,
 CONSTRAINT [PK_TB_CLIENTE_ENDERECO] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_CLIENTE_ENDERECO]  WITH CHECK ADD  CONSTRAINT [FK_CLIENTE] FOREIGN KEY([CLIENTE_ID])
REFERENCES [dbo].[TB_CLIENTE] ([ID])
GO

ALTER TABLE [dbo].[TB_CLIENTE_ENDERECO] CHECK CONSTRAINT [FK_CLIENTE]
GO

ALTER TABLE [dbo].[TB_CLIENTE_ENDERECO]  WITH CHECK ADD  CONSTRAINT [FK_ENDERECO] FOREIGN KEY([ENDERECO_ID])
REFERENCES [dbo].[TB_ENDERECO] ([ID])
GO

ALTER TABLE [dbo].[TB_CLIENTE_ENDERECO] CHECK CONSTRAINT [FK_ENDERECO]
GO

