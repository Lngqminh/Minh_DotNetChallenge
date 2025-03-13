CREATE TABLE "request_approvers"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "RequestId" UNIQUEIDENTIFIER   default newid(),
    "RoleId" UNIQUEIDENTIFIER   default newid(),
    "UserId" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME
    CONSTRAINT "request_approvers_pkey" PRIMARY KEY ("Id")
)

