CREATE TABLE "useractions"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Username" NVARCHAR(255),
    "Action" NVARCHAR(255),
    "Description" NVARCHAR(255),
    "ActionDate" DATETIME,
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "useractions_pkey" PRIMARY KEY ("Id")
)

