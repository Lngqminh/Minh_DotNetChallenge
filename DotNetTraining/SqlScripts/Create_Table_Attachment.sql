CREATE TABLE "attachments"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "FileName" varchar(255),
    "Path" varchar(255),
    "Extension" varchar(255),
    "UploadedBy" UNIQUEIDENTIFIER   default newid(),
    "UploadedAt" datetime,
    "CreatedAt" datetime,
    "UpdatedAt" datetime,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "attachments_pkey" PRIMARY KEY ("Id")
)

