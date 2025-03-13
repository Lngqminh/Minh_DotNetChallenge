CREATE TABLE "notification_token"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "UserId" UNIQUEIDENTIFIER   default newid(),
    "Token" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "notification_token_pkey" PRIMARY KEY ("Id")
)

