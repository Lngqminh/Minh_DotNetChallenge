CREATE TABLE "bpcustomercredit"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Rules" NVARCHAR(255),
    "BpId" UNIQUEIDENTIFIER   default newid(),
    "RiskClass" NVARCHAR(255),
    "CheckRule" NVARCHAR(255),
    "CreditSegment" NVARCHAR(255),
    "Limit" boolean,
    "ValidTo" NVARCHAR(255),
    "IsNewData" boolean,
    "SortOrder" INT,
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "bpcustomercredit_pkey" PRIMARY KEY ("Id")
)

