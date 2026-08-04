# Gestão de Pedidos
Projeto desenvolvido como solução para o **Teste Técnico – Desenvolvedor(a) Pleno Full-Stack (C# + Angular).**
<br>
<br>
# :rocket: Executando o Projeto
```bash
# Clonar o repositório
git clone https://github.com/MiBeDe/gestao-pedidos-api.git

# Restaurar os pacotes.
dotnet restore

# Executar
dotnet run
```
<br>

> [!IMPORTANT]
> Copie o endereço informado conforme imagem abaixo no brownser para abrir o Swagger. Guarde esse endereço para ser informado posteriormente no projeto Front-end para a comunicação com a API.

<p align="left">
  <img src="https://storagedemombd.blob.core.windows.net/documents/PortaAPI.png" width="800" alt="Porta da API">
</p>
<br>

# :computer: Configurar a Connection String
O projeto utiliza uma variável de ambiente para armazenar a Connection String.

```bash
Crie uma variável de ambiente chamada:

#Nome da Variável:
gestaoPedidosConnection

#Valor da variável: (Substituir **SERVER_NAME / **LOGIN / **PASSWORD - Normalmente localizados ao abrir o SQL Server conforme imagem abaixo.)
Server=<SERVER_NAME>;Initial Catalog=GestaoPedidosDb;User ID=<LOGIN>;Password=<PASSWORD>;Persist Security Info=False;MultipleActiveResultSets=True;application name=gestaopedidos;Encrypt=false
```
<br>
<p align="left">
  <img src="https://storagedemombd.blob.core.windows.net/documents/SqlServer.png" width="600" alt="Porta da API">
</p>

[Guia de ajuda para configuração de variável de ambiente](https://www.autodesk.com/br/support/technical/article/caas/sfdcarticles/sfdcarticles/PTB/How-to-manually-set-an-Environment-Variable-for-Fusion-360-on-Windows.html)
<br>
<br>
# :floppy_disk: Configurar o Banco de Dados:
Abra uma nova instância do SQL Server e execute o script localizado em:

/database/script.sql

O script criará todas as tabelas e dados necessários para execução da aplicação.


