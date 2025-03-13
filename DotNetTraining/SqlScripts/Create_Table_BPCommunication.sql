CREATE TABLE "bpcommunication"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "BpId" UNIQUEIDENTIFIER   default newid(),
    "Code" NVARCHAR(255),
    "Telephone1" NVARCHAR(255),
    "Extension1" NVARCHAR(255),
    "Telephone2" NVARCHAR(255),
    "Extension2" NVARCHAR(255),
    "Telephone3" NVARCHAR(255),
    "Extension3" NVARCHAR(255),
    "MobilePhone1" NVARCHAR(255),
    "MobilePhone2" NVARCHAR(255),
    "MobilePhone3" NVARCHAR(255),
    "MobilePhone4" NVARCHAR(255),
    "Fax1" NVARCHAR(255),
    "FaxExtension1" NVARCHAR(255),
    "Fax2" NVARCHAR(255),
    "FaxExtension2" NVARCHAR(255),
    "Email1" NVARCHAR(255),
    "Email2" NVARCHAR(255),
    "Email3" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "bpcommunication_uq" UNIQUE  ("Code")CONSTRAINT "bpcommunication_pkey" PRIMARY KEY ("Id")
)

