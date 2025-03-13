CREATE TABLE "identity_users"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "Username" varchar(255),
    "Email" varchar(255),
    "Password" varchar(255),
    "Passwordd" varchar(255),
    "AuthenticationType" int,
    "Salts" varchar(255),
    "Status" int,
    "FirstName" varchar(255),
    "LastName" varchar(255),
    "CreatedAt" datetime,
    "UpdatedAt" datetime
    CONSTRAINT "identity_users_pkey" PRIMARY KEY ("Id")
)

