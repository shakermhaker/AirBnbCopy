# DotNet-Architecture-Template

Key Architectural Decisions & Refactoring
The Challenge: Core Layer Independence vs. Domain Relations
Initially, core identity entities like User, OperationClaim, and UserOperationClaim were located inside the Core layer. However, as the domain expanded (e.g., the User table needing to establish foreign key relationships with domain-specific tables like Businesses or Fields), a major structural problem emerged:

The Core layer must remain completely independent and universal (reusable across different projects).

Keeping domain-specific relations inside Core forced it to depend on the Entities layer, creating a forbidden Circular Dependency loop (Core ⟷ Entities).

The Solution: Dependency Inversion Pries (DIP)
To fix this without breaking Clean Architecture boundaries, the following steps were taken:

Entity Relocation: All concrete database models (User, OperationClaim, UserOperationClaim) were moved from Core to the Entities layer. This allows the User table to freely give or receive foreign keys from any other business domain table.

Abstractions in Core: To keep the JWT token generation and authentication mechanism inside Core completely decoupled, generic interfaces like ICoreUser and IOperationClaim were defined within the Core layer.

Loose Coupling via Interfaces: The concrete User class in the Entities layer implements ICoreUser. When the API or Business layer triggers token generation, the Core layer processes the request using these abstractions.

As a result, the Core layer remains 100% agnostic of the database relationships, successfully preserving architectural boundaries while maintaining a robust security infrastructure.

Setup & Configuration
1. Token Options (JWT)
Ensure your appsettings.json file in the WebAPI project contains a secure SecurityKey that is at least 64 characters (512 bits) long to satisfy the HMAC-SHA512 algorithm requirements:

2. NOTE TO MYSELF 
dotnet ef migrations add BLABLA --project DataAccess --startup-project WebAPI
dotnet ef database update --project DataAccess --startup-project WebAPI
