use master
go
create database CATALOGO_P3_DB
go
use CATALOGO_P3_DB
go
USE CATALOGO_P3_DB
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MARCAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_MARCAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CATEGORIAS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ARTICULOS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](150) NULL,
	[IdMarca] [int] NULL,
	[IdCategoria] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_ARTICULOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

create table IMAGENES(
	Id int IDENTITY(1,1) not null,
	IdArticulo int not null,
	ImagenUrl varchar(1000) not null
)
go

insert into MARCAS values ('Samsung'), ('Apple'), ('Sony'), ('Huawei'), ('Motorola')
insert into CATEGORIAS values ('Celulares'),('Televisores'), ('Media'), ('Audio')
insert into ARTICULOS values ('S01', 'Galaxy S10', 'Una canoa cara', 1, 1, 69.999),
('M03', 'Moto G Play 7ma Gen', 'Ya siete de estos?', 1, 5, 15699),
('S99', 'Play 4', 'Ya no se cuantas versiones hay', 3, 3, 35000),
('S56', 'Bravia 55', 'Alta tele', 3, 2, 49500),
('A23', 'Apple TV', 'lindo loro', 2, 3, 7850)

/*TRUNCATE TABLE IMAGENES*/
insert into imagenes values
(1, 'https://i.blogs.es/addc23/samsung-galaxy-s10/650_1200.jpg'),
(1, 'https://i.blogs.es/e5b8d4/galaxy-s10-camaras/450_1000.jpg'),
(1, 'https://img.global.news.samsung.com/ar/wp-content/uploads/2019/03/S10-FB-ESP-v2.jpg'),
(2, 'https://http2.mlstatic.com/D_NQ_NP_806739-MLA79776344638_102024-O.webp'),
(2, 'https://http2.mlstatic.com/D_Q_NP_2X_796177-CBT109115309977_032026-T.webp'),
(2, 'https://jumboargentina.vtexassets.com/arquivos/ids/548647-800-auto?v=637045464536970000&width=800&height=auto&aspect=true'),
(3, 'https://http2.mlstatic.com/D_NQ_NP_786815-MLA74779490077_022024-O.webp'),
(3, 'https://thumbs.ielectro.es/product/med/96278-1.webp'),
(4, 'https://images.fravega.com/f1000/e727b0ddef5598e45fd1410a021ee0cb.jpg'),
(4, 'https://http2.mlstatic.com/D_NQ_NP_893994-MLA89649715904_082025-O.webp'),
(4, 'https://images.fravega.com/f1000/524d7e931a1ae681c378faf17caee6e4.jpg'),
(5, 'https://cdn.eldestapeweb.com/eldestape/022026/1771420010961/the-morning-show-en-televisor-jpg..jpg?cw=1200&ch=675'),
(5, 'https://cdn.eldestapeweb.com/eldestape/022026/1771420009206/apple-tv-catalogo-jpg..webp?cw=720&ch=405&extw=jpg'),
(5, 'https://www.lg.com/ar/images/Microsite/apple-tv-plus-2025-1st/apple-tv-plus-2025-microsite-easy-to-redeem-m.jpg')

select * from ARTICULOS