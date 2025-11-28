# Ordem de Serviço Web

Aplicação web para gestão de ordens de serviço, com backend em ASP.NET Core.

---

## ✅ Pré-requisitos

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) local ou em container
- [Docker Desktop](https://www.docker.com/get-started/)
- Git instalado

---

## 📥 Clonar o repositório
https://github.com/SEU-USUARIO/ordem-servico-backend.git

## 🗄️ Backend (.NET API)
1. Entrar na pasta do backend:
cd ordem-servico-backend/ordem-servico-backend
 
2. Rode o comando: docker compose up --build

Por padrão, a API ja criará um usuário admin para acessar a aplicação.
User: admin
Pass: admin@123

3. A Api irá rodar automaticamente em: http://localhost:8080
4. Consulte a documentação da API em: http://localhost:8080/swagger/index.html
