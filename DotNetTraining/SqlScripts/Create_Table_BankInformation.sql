CREATE TABLE "bank_information"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "CountryKey" NVARCHAR(255),
    "Name" NVARCHAR(255),
    "Branch" NVARCHAR(255),
    "Address" NVARCHAR(255),
    "SwiftCode" NVARCHAR(255),
    "BankKey" NVARCHAR(255),
    "isDeleted" boolean,
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "bank_information_pkey" PRIMARY KEY ("Id")
)

