# README
## Como rodar a aplicação

### Requisitos
* Docker
---
### Passos :
1) Abra um terminal, acesse o seguinte diretório :

    `.\teste-bernhoeft\inventario.command-api`


2) Execute o comando o seguinte comando:
    ```
    docker-compose up -d --build
    ```

3) Conecte-se ao banco de dados:
    ```
    Host: localhost
    Port: 14333
    User: sa
    Password: yourStrong(!)Password
    ```

4) Executar os comandos para criação do banco de dados:
   
    `.\teste-bernhoeft\inventario.command-api\sql\01-criacao-tabelas.sql`

---
## A aplicação pode ser acessada através da seguinte URL: http://localhost:8080/swagger/index.html