CREATE TABLE "feature_roles"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "FeatureID" UNIQUEIDENTIFIER   default newid(),
    "RoleID" UNIQUEIDENTIFIER   default newid(),
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    CONSTRAINT "feature_roles_pkey" PRIMARY KEY ("Id")
)

