CREATE TABLE "trading_partners"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "No" varchar(255),
    "CompanyCode" varchar(255),
    "CompanyName" varchar(255),
    "UploadedBy" UNIQUEIDENTIFIER   default newid(),
    "UploadedAt" datetime,
    "CreatedAt" datetime,
    "UpdatedAt" datetime,
    CONSTRAINT "trading_partners_pkey" PRIMARY KEY ("Id")
)

