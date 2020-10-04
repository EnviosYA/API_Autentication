CREATE DATABASE DbAutenticacion
GO
USE DbAutenticacion
GO

CREATE TABLE [dbo].[Cuenta](
	[IdCuenta] [int] NOT NULL IDENTITY PRIMARY KEY,
	[Mail] [varchar](100) NOT NULL,
	[Contraseña] [nvarchar] (MAX)NOT NULL,
	[IdEstado] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdTipoCuenta] [int] NOT NULL,
	[FecAlta] [datetime] NULL)
GO

CREATE TABLE [dbo].[TipoCuenta](
	[IdTipoCuenta] [int] NOT NULL IDENTITY PRIMARY KEY,
	[DescTipCuenta] [varchar](50) NULL)
GO

CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] NOT NULL IDENTITY PRIMARY KEY,
	[DescEstado] [varchar](50) NULL)
GO

ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO

ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_TipoCuenta] FOREIGN KEY([IdTipoCuenta])
REFERENCES [dbo].[TipoCuenta] ([IdTipoCuenta])
GO


-- Scaffold-DbContext "Data Source= DESKTOP-Q70Q68R\SQLEXPRESS; Initial Catalog= DbAutenticacion; user=Lean; password=123123; Integrated Security= true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Migration -v