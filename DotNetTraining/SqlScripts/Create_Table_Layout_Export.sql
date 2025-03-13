CREATE TABLE "layout_export"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Name" NVARCHAR(255),
    "Export" NVARCHAR(255),
    "CreatedBy" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "layout_export_pkey" PRIMARY KEY ("Id")
)

