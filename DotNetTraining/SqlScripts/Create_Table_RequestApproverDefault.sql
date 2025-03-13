CREATE TABLE "default_approvers"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "FlowCode" NVARCHAR(255),
    "RoleId" UNIQUEIDENTIFIER   default newid(),
    "UserId" UNIQUEIDENTIFIER   default newid(),
    "SortOrder" NVARCHAR(MAX),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "default_approvers_pkey" PRIMARY KEY ("Id")
)

