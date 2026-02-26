# PortfolioBlog

A modern ASP.NET Core blog application showcasing a progressive architectural modernization — from a simple Razor Pages CRUD to an enterprise-grade, API-first platform.

## Quick Start

```bash
# Clone & run
git clone https://github.com/aphaysom/portfolio-blog.git
cd portfolio-blog

# API (creates SQLite database automatically)
cd src/PortfolioBlog && dotnet run

# Client (in another terminal)
cd src/PortfolioBlog.Web && dotnet run
```

- API: `http://localhost:5196` (Scalar docs at `/scalar`)
- Client: `http://localhost:5156/posts`

> **Note:** The project uses **SQLite** locally — no database setup required.
> The Git history demonstrates a PostgreSQL migration path, but the `develop` branch
> is configured with SQLite for zero-friction testing.

## Architecture

```
PortfolioBlog/
├── src/
│   ├── PortfolioBlog/          → API (FastEndpoints + Mediator CQRS)
│   └── PortfolioBlog.Web/      → Client (Blazor Server + Refit)
└── PortfolioBlog.slnx
```

## Tech Stack

| Layer | Technology |
|---|---|
| API Framework | FastEndpoints (Vertical Slice) |
| CQRS | Mediator (Source Generator) |
| Validation | FluentValidation |
| Database | SQLite (local) / PostgreSQL (production) |
| Caching | HybridCache (L1 In-Memory + L2 Redis Cloud) |
| Resilience | Polly v8 + Rate Limiting |
| Auth | JWT Bearer + Role-based |
| Client | Blazor Server + Refit (typed HTTP client) |
| API Docs | OpenAPI + Scalar |
| Formatting | CSharpier |

## Milestones

The Git history is structured as a progressive modernization, each milestone merged as a `--no-ff` bubble:

1. **PostgreSQL Foundation** — Migrate from SQLite to PostgreSQL with TIMESTAMPTZ
2. **API & Core Refinement** — MVC Controllers, DTOs, Repository & Service patterns, Scalar
3. **Minimal APIs Transition** — Replace MVC with `MapGroup` + `Results`
4. **FastEndpoints** — Vertical Slice architecture
5. **CQRS & Validation** — Mediator (Source Gen) + FluentValidation pipeline
6. **Security & Resilience** — Rate Limiting + Polly retry/circuit-breaker
7. **Redis Caching** — HybridCache with Redis Cloud (L1+L2)
8. **JWT Authentication** — Bearer tokens, role-based write protection
9. **Blazor Client + Refit** — Separate Blazor Server app consuming the API via typed HTTP client with auto-auth `DelegatingHandler`

## Key Patterns Demonstrated

- **Progressive modernization** — Clean Git history showing architectural evolution
- **Vertical Slice Architecture** — Features organized by domain, not by layer
- **CQRS** — Commands and Queries separated via Mediator
- **DelegatingHandler** — Auto-authentication with JWT token injection and 401 retry
- **Optimistic UI updates** — Blazor inserts created items locally without re-fetching
- **Two-tier caching** — HybridCache with in-memory L1 and Redis Cloud L2
