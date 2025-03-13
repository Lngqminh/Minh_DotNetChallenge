CREATE TABLE "request_attachments"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "AttachmentID" UNIQUEIDENTIFIER   default newid(),
    "RequestID" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" datetime,
    "UpdatedAt" datetime,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "request_attachments_pkey" PRIMARY KEY ("Id")
)

