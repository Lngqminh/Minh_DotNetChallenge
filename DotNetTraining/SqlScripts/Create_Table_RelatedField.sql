CREATE TABLE "related_field"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Name" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "related_field_pkey" PRIMARY KEY ("Id")
)

