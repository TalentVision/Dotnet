# TalentVision API

### Triagem Inteligente de Talentos com IA

#### Global Solution --- FIAP \| 2TDS \| 2025-2

## Visão Geral

O **TalentVision** é uma solução de triagem inteligente de talentos,
construída para a **Global Solution da FIAP**, cujo objetivo é
modernizar e automatizar a triagem de currículos entre empresas e trabalhadores.

## Arquitetura da Solução

    TalentVision
     ├── TalentVision.Domain
     ├── TalentVision.Application
     ├── TalentVision.Infrastructure
     ├── TalentVision.API
     └── TalentVision.API.Tests

## Tecnologias Utilizadas

-   .NET 8\
-   ASP.NET Core Web API\
-   Oracle Database\
-   Entity Framework Core\
-   NUnit + Moq\
-   Swagger\
-   API Key


Swagger (Autorizar):

    http://40.82.191.129:8080/swagger/index.html


End-points com API-KEY na URL:

    http://40.82.191.129:8080/api/v1/Candidaturas?apiKey=talentVision-secret-key



## Endpoints Principais *(Usar API-KEY acima)*

-   GET /api/v1/usuarios
-   POST /api/v1/usuarios
-   GET /api/v1/vagas
-   POST /api/v1/vagas
-   GET /health

## Integrantes

-   Nicolas Barutti -- RM554944
-   Kleber da Silva -- RM557887
-   Lucas Rainha -- RM558471
