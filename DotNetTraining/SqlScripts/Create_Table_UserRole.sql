CREATE TABLE "user_roles"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "UserID" UNIQUEIDENTIFIER   default newid(),
    "RoleID" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    CONSTRAINT "user_roles_pkey" PRIMARY KEY ("Id")
)

