# Estaciona Fácil

![GitHub repo size](https://img.shields.io/github/repo-size/guirms/site_estaciona_facil?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/guirms/site_estaciona_facil?style=for-the-badge)
![Bitbucket open issues](https://img.shields.io/bitbucket/issues/guirms/site_estaciona_facil?style=for-the-badge)


> Web site focado em logística de estacionamentos. Possui API própria em .NET Core 6, front-end em Angular 15 e persistência de dados em SQL Server

### Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas nas seguintes tarefas:

- [ ] Microsserviço de geração de relatório.
- [ ] Serviço de mensageria com RabbitMQ (Dockerizado).
- [ ] Deploy em plataforma Azure.

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:
* .NET Core 6 
* Angular 15 `Dependências: Node 16.7.1 / Python 3.10.7`
> Após instalar todas as dependências do Angular, Execute o comando no terminal da pasta raíz do front-end: *npm install*
* Microsoft SQL Server 2019 `Connection string em appsetting.json`
* Bootstrap 6 

## ☕ Usando o Estaciona Fácil

Para usar Estaciona Fácil, siga estas etapas:

### Angular
* Iniciar instância da aplicação no browser: ng serve -o
### Entity Framework
* Adicionar migration: dotnet ef migrations add NOME_MIGRATION --startup-project ../Web/
* Atualizar banco de dados: dotnet ef database update NOME_MIGRATION --startup-project ../Web/
* Remover migration: dotnet ef migrations remove NOME_MIGRATION --startup-project ../Web/


## 🤝 Contato

Qualquer dúvida ou sugestão específica, por favor não hesite em criar Issues, que serão respondidas o mais rápido possível.

- Caso deseje um contato mais direto, por favor me contate via Linkedin ou Gmail:
<div>
<a href="https://www.linkedin.com/in/guilherme-machado-santana-468174216/" target="_blank"><img src="https://img.shields.io/badge/-LinkedIn-%230077B5?style=for-the-badge&logo=linkedin&logoColor=white" target="_blank"></a> <a href = "mailto:guilherme.ms2003@aluno.ifsc.edu.br"><img src="https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white" target="_blank"></a>
</div>
