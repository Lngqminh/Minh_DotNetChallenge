CREATE TABLE "default_settings"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Name" NVARCHAR(255),
    "Value" NVARCHAR(255),
    "Code" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "default_settings_pkey" PRIMARY KEY ("Id")
)

