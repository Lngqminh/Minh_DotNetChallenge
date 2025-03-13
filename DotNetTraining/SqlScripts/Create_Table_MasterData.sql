CREATE TABLE "master_data"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Name" NVARCHAR(255),
    "Code" NVARCHAR(255),
    "Type" NVARCHAR(255),
    "Value" NVARCHAR(255),
    "Description" NVARCHAR(255),
    "Child" NVARCHAR(255),
    "CreatedBy" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "Parent" NVARCHAR(255),
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "master_data_uq" UNIQUE  ("Code")CONSTRAINT "master_data_pkey" PRIMARY KEY ("Id")
)

