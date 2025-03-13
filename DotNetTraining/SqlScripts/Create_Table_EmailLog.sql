CREATE TABLE "Email_Log"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "LogID" NVARCHAR(255),
    "Email" NVARCHAR(255),
    "Subject" NVARCHAR(255),
    "Status" NVARCHAR(255),
    "CreatedBy" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "Email_Log_pkey" PRIMARY KEY ("Id")
)

