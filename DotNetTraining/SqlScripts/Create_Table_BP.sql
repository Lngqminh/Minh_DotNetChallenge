CREATE TABLE "bps"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "No" varchar(255),
    "RequestType" varchar(255),
    "BPType" varchar(255),
    "BPName" varchar(255),
    "CreatedBy" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" datetime,
    "UpdatedAt" datetime,
    CONSTRAINT "bps_pkey" PRIMARY KEY ("Id")
)

