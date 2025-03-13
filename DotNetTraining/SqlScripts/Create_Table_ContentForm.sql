CREATE TABLE "content_form"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "ElementFormId" UNIQUEIDENTIFIER   default newid(),
    "Name" NVARCHAR(255),
    "Code" NVARCHAR(255),
    "Active" boolean,
    "Required" boolean,
    "SapTechnicalName" NVARCHAR(255),
    "RelatedField" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "content_form_uq" UNIQUE  ("Code")CONSTRAINT "content_form_pkey" PRIMARY KEY ("Id")
)

