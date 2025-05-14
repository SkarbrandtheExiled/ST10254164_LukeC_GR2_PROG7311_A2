CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "products" (
    "ProductID" INTEGER NOT NULL CONSTRAINT "PK_products" PRIMARY KEY AUTOINCREMENT,
    "productName" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "productCreationDate" TEXT NOT NULL,
    "farmerName" TEXT NOT NULL,
    "dateAdded" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250512085305_sqlite.local_migration_latest', '9.0.5');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250512090828_sqlite.local_migration_new', '9.0.5');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250512141858_sqlite.local_migration_762', '9.0.5');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250514091943_sqlite.local_migration_794', '9.0.5');

CREATE TABLE "employees" (
    "employeeID" INTEGER NOT NULL CONSTRAINT "PK_employees" PRIMARY KEY AUTOINCREMENT,
    "farmerName" TEXT NOT NULL,
    "dateAdded" TEXT NOT NULL
);

CREATE TABLE "farmers" (
    "farmerID" INTEGER NOT NULL CONSTRAINT "PK_farmers" PRIMARY KEY AUTOINCREMENT,
    "productName" TEXT NOT NULL,
    "farmerName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "productCreationDate" TEXT NOT NULL,
    "employeeModelemployeeID" INTEGER NULL,
    CONSTRAINT "FK_farmers_employees_employeeModelemployeeID" FOREIGN KEY ("employeeModelemployeeID") REFERENCES "employees" ("employeeID")
);

CREATE INDEX "IX_farmers_employeeModelemployeeID" ON "farmers" ("employeeModelemployeeID");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250514101222_sqlite.local_migration_894', '9.0.5');

DROP INDEX "IX_farmers_employeeModelemployeeID";

ALTER TABLE "products" RENAME COLUMN "ProductID" TO "productID";

ALTER TABLE "employees" RENAME COLUMN "employeeID" TO "EmployeeID";

ALTER TABLE "employees" RENAME COLUMN "farmerName" TO "employeeName";

ALTER TABLE "employees" ADD "Password" TEXT NOT NULL DEFAULT '';

CREATE TABLE "ef_temp_farmers" (
    "farmerID" INTEGER NOT NULL CONSTRAINT "PK_farmers" PRIMARY KEY AUTOINCREMENT,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "farmerName" TEXT NOT NULL
);

INSERT INTO "ef_temp_farmers" ("farmerID", "Email", "Password", "farmerName")
SELECT "farmerID", "Email", "Password", "farmerName"
FROM "farmers";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;
DROP TABLE "farmers";

ALTER TABLE "ef_temp_farmers" RENAME TO "farmers";

COMMIT;

PRAGMA foreign_keys = 1;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250514142625_sqlite.local_migration_143', '9.0.5');

