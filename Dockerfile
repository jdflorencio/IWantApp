# Use a imagem oficial do SQL Server
FROM mcr.microsoft.com/mssql/server As sql


# Defina as variáveis de ambiente para a configuração do SQL Server
ENV SA_PASSWORD=SA@123456
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Express

# Expõe a porta 1433 para acessar o SQL Server
EXPOSE 1433

# Crie um diretório para scripts de inicialização
WORKDIR /usr/src/app
#COPY ./meu_script_sql_server.sql .

# CMD é usado para fornecer o comando padrão para executar quando o contêiner é iniciado
CMD /opt/mssql/bin/sqlservr

# Construir a imagem Docker
#docker build -t sql .

# Executar o contêiner
#docker run -d -p 1433:1433 --name db sql


