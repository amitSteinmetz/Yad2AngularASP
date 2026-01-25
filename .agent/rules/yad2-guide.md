---
trigger: always_on
---

1. General Principles (Cross-Stack)

- No Magic Strings: Use Constants, Enums, or Configuration objects for any repeated value.

- DRY (Don't Repeat Yourself): If you are about to duplicate logic or UI, stop and create a shared component, service, or utility.

- Security First: Never output code with SQL Injection risks, XSS vulnerabilities, or insecure data exposure. Use parameterized queries and proper DTOs.

- Performance: Prioritize O(n) complexity or better. Avoid unnecessary loops and heavy computations on the main thread.

- File Naming: When you create a new file, stick to Angular CLI naming conventions for new files.

2. Backend Rules (ASP.NET Core)

Architecture: Follow Clean Architecture or Onion Architecture (Controller -> Service -> Repository/Data).

- DTOs: Never expose Database Entities directly. Use AutoMapper or manual mapping to DTOs for API responses.

- Async/Await: All I/O operations (DB, Files, External APIs) must be asynchronous using Task. Avoid .Result or .Wait().

- Dependency Injection: Always use Constructor Injection. Avoid newing up services manually.

- Error Handling: Use a global exception handler or Middleware. Return consistent JSON error responses.

3. Frontend Rules (Angular)

- Standalone Components: Use Standalone components as the default (Angular 14+).

- Signals: Prefer Angular Signals for state management over manual ChangeDetection or heavy RxJS where applicable.

- Shared UI: If a UI element (Card, Input, Button) is used more than twice, move it to a Shared module/folder.

- Performance: Use trackBy in \*ngFor (or the new @for syntax) to optimize DOM rendering. Use OnPush change detection by default.

- Types: Strict typing everywhere. Avoid any at all costs. Interface names should be shared between Backend and Frontend if possible.

- deprecated warnings: do'nt ignore deprecration warnings - if they exist, find un-deprecated alternative.

4. Code Refactoring & Logic

- Deduplication: Before creating a new service or component, check existing folders for a "Common" or "Shared" equivalent.

- Optimization: When modifying existing code, identify and fix inefficient LINQ queries (Backend) or unnecessary re-renders (Frontend).
