CREATE TABLE "request_comment"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "RequestID" UNIQUEIDENTIFIER   default newid(),
    "UserID" UNIQUEIDENTIFIER   default newid(),
    "Message" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "request_comment_pkey" PRIMARY KEY ("Id")
)

