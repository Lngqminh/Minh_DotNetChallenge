CREATE TABLE "notifications"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "RequestId" UNIQUEIDENTIFIER   default newid(),
    "DocNo" NVARCHAR(255),
    "Message" NVARCHAR(255),
    "Status" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "notifications_pkey" PRIMARY KEY ("Id")
)

