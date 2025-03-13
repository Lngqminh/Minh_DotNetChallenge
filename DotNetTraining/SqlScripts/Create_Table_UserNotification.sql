CREATE TABLE "user_notification"
(
    "Id" UNIQUEIDENTIFIER   default newid() NOT NULL,
    "UserId" UNIQUEIDENTIFIER   default newid(),
    "NotificationId" UNIQUEIDENTIFIER   default newid(),
    "IsSeen" boolean,
    "CreatedAt" DATETIME,
    "UpdatedAt" DATETIME,
    "Id" UNIQUEIDENTIFIER   default newid(),
    CONSTRAINT "user_notification_pkey" PRIMARY KEY ("Id")
)

