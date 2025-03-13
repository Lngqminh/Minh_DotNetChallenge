CREATE TABLE "default_flows"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "No" INT,
    "FlowCode" NVARCHAR(255),
    "Name" NVARCHAR(255),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "default_flows_pkey" PRIMARY KEY ("Id")
)

