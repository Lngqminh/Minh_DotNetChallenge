CREATE TABLE "roles"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Name" NVARCHAR(255),
    "Description" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    CONSTRAINT "roles_pkey" PRIMARY KEY ("Id")
)

