CREATE TABLE "logs"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "ErrorCode" NVARCHAR(255),
    "ErrorMessage" NVARCHAR(255),
    "Username" NVARCHAR(255),
    "Feature" NVARCHAR(255),
    "IncidentTime" DATETIME,
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "logs_pkey" PRIMARY KEY ("Id")
)

