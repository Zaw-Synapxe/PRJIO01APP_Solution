CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "UserName" TEXT NULL
);

CREATE TABLE "Profiles" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Profiles" PRIMARY KEY AUTOINCREMENT,
    "Bio" TEXT NULL,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_Profiles_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Profiles_UserId" ON "Profiles" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241212150222_OneToOne', '8.0.11');

COMMIT;