CREATE TABLE "request_history"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "RequestID" UNIQUEIDENTIFIER   default newid(),
    "Content" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "request_history_pkey" PRIMARY KEY ("Id")
)

