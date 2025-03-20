CREATE TABLE "Users"
(
    "Id" NVARCHAR(MAX) NOT NULL,
    "UserName" NVARCHAR(MAX),
    "FirstName" NVARCHAR(MAX),
    "LastName" NVARCHAR(MAX),
    "Roles" NVARCHAR(MAX),
    "Email" NVARCHAR(MAX),
    "Password" NVARCHAR(MAX),
    "FullName" NVARCHAR(MAX),
    "CreatedAt" NVARCHAR(MAX),
    "UpdatedAt" NVARCHAR(MAX),
    "Id" NVARCHAR(MAX),
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
)

