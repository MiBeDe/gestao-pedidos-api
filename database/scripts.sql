CREATE DATABASE GestaoPedidosDb
GO
USE GestaoPedidosDb
GO

CREATE TABLE Clientes (
	IdCliente int IDENTITY(1,1) NOT NULL,
	NomeCompleto NVARCHAR(100) NOT NULL,
	Cpf VARCHAR(11) NOT NULL,
	CONSTRAINT PK_IdCliente PRIMARY KEY CLUSTERED(
		IdCliente ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO

CREATE TABLE Produtos (
	IdProduto INT IDENTITY(1,1) NOT NULL,
	NomeProduto NVARCHAR(1000) NOT NULL,
	Descricao VARCHAR(MAX) NOT NULL,
	Preco DECIMAL(18,2) NOT NULL,
	Quantidade INT NOT NULL,
	CONSTRAINT PK_IdProduto PRIMARY KEY CLUSTERED(
		IdProduto ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)

------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO

CREATE TABLE StatusPedido (
	IdStatus INT IDENTITY(1,1) NOT NULL,
	Descricao NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_IdStatus PRIMARY KEY CLUSTERED(
		IdStatus ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO
INSERT INTO StatusPedido (Descricao)
VALUES ('Criado'),('Confirmado'),('Cancelado'),('Finalizado')

------------------------------------------------------------------------------------------------------------------------------------------------------------------------
GO

CREATE TABLE Pedidos (
	IdPedido INT IDENTITY(1,1) NOT NULL,
	IdCliente INT NOT NULL,
	IdStatus INT NOT NULL,
	ValorTotalPedido DECIMAL(18,2) NOT NULL
	CONSTRAINT PK_IdPedido PRIMARY KEY CLUSTERED(
		IdPedido ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO
ALTER TABLE Pedidos WITH CHECK ADD CONSTRAINT [FK_Pedidos_Clientes] FOREIGN KEY([IdCliente])
REFERENCES Clientes ([IdCliente])
GO
ALTER TABLE Pedidos WITH CHECK ADD CONSTRAINT [FK_Pedidos_StatusPedido] FOREIGN KEY([IdStatus])
REFERENCES StatusPedido ([IdStatus])

------------------------------------------------------------------------------------------------------------------------------------------------------------------------

GO
CREATE TABLE PedidoProdutos (
	IdPedidoProduto INT IDENTITY(1,1) NOT NULL,
	IdPedido INT NOT NULL,
	IdProduto INT NOT NULL,
	ValorUnitario DECIMAL(18,2) NOT NULL,
	Quantidade INT NOT NULL,
	SubTotal Decimal(18,2) NOT NULL,
	CONSTRAINT PK_IdPedidoProduto PRIMARY KEY CLUSTERED(
		IdPedidoProduto ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
GO
ALTER TABLE PedidoProdutos WITH CHECK ADD CONSTRAINT [FK_PedidoProdutos_Pedidos] FOREIGN KEY(IdPedido)
REFERENCES Pedidos ([IdPedido])
GO
ALTER TABLE PedidoProdutos WITH CHECK ADD CONSTRAINT [FK_PedidoProdutos_Produtos] FOREIGN KEY(IdProduto)
REFERENCES Produtos ([IdProduto])