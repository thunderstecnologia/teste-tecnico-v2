## ⚠️ IMPORTANTE: CONFIGURAÇÃO DE AMBIENTE E DOCKER ⚠️  

Atualmente, o projeto **não possui um Dockerfile**, o que significa que **não há suporte nativo para execução em contêineres**.  

O ambiente é definido automaticamente como `Development` ao rodar via **Visual Studio ou `dotnet run`**, devido à configuração no `launchSettings.json`.  
No entanto, **caso o projeto seja executado via `dotnet publish` ou precise rodar em um ambiente de CI/CD, Docker ou servidor**, será necessário **definir manualmente a variável de ambiente `ASPNETCORE_ENVIRONMENT`**.

### 📌 O que isso significa?
- **Se rodar no Visual Studio ou via `dotnet run` → Tudo funciona normalmente (Development).**
- **Se rodar via `dotnet publish` e executar o `.dll` manualmente → Precisa definir `ASPNETCORE_ENVIRONMENT` antes da execução.**
- **Se quiser rodar em Docker no futuro → Será necessário criar um `Dockerfile` e configurar as variáveis de ambiente.**

### ✅ Ação futura:
Se houver a necessidade de rodar o projeto em contêineres ou ambientes distribuídos, será preciso adicionar um `Dockerfile` e configurar corretamente as variáveis de ambiente.

```bash
# Exemplo de como definir o ambiente manualmente antes de rodar
$Env:ASPNETCORE_ENVIRONMENT = "Development"  # (Windows PowerShell)
export ASPNETCORE_ENVIRONMENT=Development    # (Linux/macOS)
```
