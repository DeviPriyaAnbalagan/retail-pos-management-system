# Retail POS Management System

A backend Retail POS API built using **ASP.NET Core Web API**, **C#**, **SQL Server**, **Entity Framework Core**, **Swagger**, **Repository Pattern**, **Service Layer**, and **DTOs**.

This project demonstrates a basic retail POS transaction flow including product management, barcode search, sales transaction creation, VAT calculation, payment recording, stock deduction, inventory transaction tracking, and reports.

---

## Why This Project Was Created

This project was created to understand and demonstrate the core backend flow of a retail POS system.

The main focus is on:

- Clean API design
- Separation of concerns
- Repository Pattern
- Service Layer Pattern
- SQL Server integration
- Entity Framework Core
- POS transaction consistency
- Stock deduction
- Payment recording
- Negative scenario handling

---

## Tech Stack

- C#
- ASP.NET Core Web API
- .NET 8 / .NET 9
- SQL Server LocalDB
- Entity Framework Core
- Swagger / Swashbuckle
- Repository Pattern
- Service Layer
- DTOs

---

## Architecture

The project follows clean separation of concerns:

```text
Client / Swagger
        ↓
Controller Layer
        ↓
Service Layer
        ↓
Repository Layer
        ↓
Entity Framework Core DbContext
        ↓
SQL Server
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| Controllers | Handle HTTP requests and responses |
| Services | Handle business logic and validations |
| Repositories | Handle database access logic |
| DTOs | Define API request and response models |
| Models | Define database entities |
| DbContext | Manage database mapping and EF Core operations |

---

## Project Structure

```text
RetailPosSystem
│
├── Controllers
│   ├── ProductsController.cs
│   ├── SalesController.cs
│   └── ReportsController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── DTOs
│   ├── CreateProductDto.cs
│   ├── UpdateProductDto.cs
│   ├── ProductResponseDto.cs
│   ├── CreateSaleDto.cs
│   ├── SaleResponseDto.cs
│   └── ReportDtos.cs
│
├── Models
│   ├── Product.cs
│   ├── Sale.cs
│   ├── SaleItem.cs
│   ├── Payment.cs
│   └── InventoryTransaction.cs
│
├── Repositories
│   ├── Interfaces
│   │   ├── IProductRepository.cs
│   │   ├── ISaleRepository.cs
│   │   └── IReportRepository.cs
│   │
│   ├── ProductRepository.cs
│   ├── SaleRepository.cs
│   └── ReportRepository.cs
│
├── Services
│   ├── Interfaces
│   │   ├── IProductService.cs
│   │   ├── ISaleService.cs
│   │   └── IReportService.cs
│   │
│   ├── ProductService.cs
│   ├── SaleService.cs
│   └── ReportService.cs
│
└── Program.cs
```

---

## Features

### Product Management

- Create product
- Update product
- Soft delete product
- Get all active products
- Get product by ID
- Search product by barcode
- Duplicate barcode validation
- Product price, VAT, and stock management

### Sales Transaction

- Create sale with multiple products
- Generate receipt number
- Calculate subtotal
- Calculate VAT amount
- Calculate total amount
- Validate stock availability
- Validate payment amount
- Save payment details
- Deduct product stock after sale
- Record inventory transaction
- Use database transaction rollback for consistency

### Reports

- Daily sales report
- Low-stock product report

---

## API Endpoints

### Products

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | Get all active products |
| GET | `/api/products/{id}` | Get product by ID |
| GET | `/api/products/barcode/{barcode}` | Get product by barcode |
| POST | `/api/products` | Create a new product |
| PUT | `/api/products/{id}` | Update product |
| DELETE | `/api/products/{id}` | Soft delete product |

### Sales

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/sales` | Create a sale transaction |
| GET | `/api/sales/{id}` | Get sale by ID |
| GET | `/api/sales/receipt/{receiptNumber}` | Get sale by receipt number |

### Reports

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/reports/daily-sales` | Get daily sales summary |
| GET | `/api/reports/low-stock` | Get low-stock products |

---

## Sample Product Request

```json
{
  "barcode": "890100001",
  "name": "Milk 1L",
  "price": 4.50,
  "vatPercentage": 5,
  "stockQuantity": 50
}
```

---

## Sample Sale Request

```json
{
  "items": [
    {
      "productId": 1,
      "quantity": 2
    },
    {
      "productId": 2,
      "quantity": 1
    }
  ],
  "paymentMethod": "Card",
  "amountPaid": 20,
  "transactionReference": "CARD-TEST-001"
}
```

---

## POS Sale Flow

1. Cashier selects or scans products.
2. System validates product availability.
3. System checks stock quantity.
4. System calculates subtotal, VAT, and total amount.
5. System validates payment amount.
6. System creates sale and sale items.
7. System records payment details.
8. System deducts product stock.
9. System records inventory movement.
10. If any step fails, the database transaction is rolled back.

---

## Database Transaction Handling

A POS sale involves multiple database updates:

- Sale
- Sale items
- Payment
- Product stock deduction
- Inventory transaction

These operations must stay consistent.

For example:

- Sale saved but payment failed = invalid transaction
- Payment saved but stock not reduced = inventory mismatch
- Stock reduced but sale not saved = audit issue

To avoid partial updates, the sale creation process is handled inside a database transaction. If any step fails, the full transaction is rolled back.

---

## Negative Scenarios Handled

The project includes validation for common failure scenarios:

- Duplicate barcode
- Product not found
- Quantity is zero or negative
- Insufficient stock
- Amount paid is less than total amount
- Negative low-stock threshold
- Inactive product handling through soft delete

---

## Database Models

Main entities:

- Product
- Sale
- SaleItem
- Payment
- InventoryTransaction

---

## How to Run the Project

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/retail-pos-management-system.git
```

### 2. Open the project folder

```bash
cd RetailPosSystem
```

### 3. Update the connection string if required

In `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=RetailPosDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 4. Apply database migration

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

### 6. Open Swagger

```text
http://localhost:YOUR_PORT/swagger
```

---

## Design Patterns and Principles Used

### Repository Pattern

Used to separate database access logic from business logic.

### Service Layer Pattern

Used to keep business rules and validations separate from API controllers.

### DTO Pattern

Used to separate API request and response models from database entities.

### Dependency Injection

Used to inject services and repositories through interfaces, reducing tight coupling and improving testability.

### Separation of Concerns

Each layer has a clear responsibility, making the project easier to maintain, test, and extend.

---

## Future Enhancements

- React POS frontend
- JWT authentication
- Role-based access for Admin, Manager, and Cashier
- Refund and return module
- Unit testing with xUnit or NUnit
- Docker support
- Azure deployment
- CI/CD pipeline
- Bulk product upload
- Store and terminal management
- Receipt printing support

---

## Summary

This project demonstrates a clean backend implementation of a basic Retail POS system using ASP.NET Core Web API and SQL Server.

The most important part of the project is the sale transaction flow, where sale creation, payment recording, stock deduction, and inventory tracking are handled together using database transaction rollback to maintain consistency.