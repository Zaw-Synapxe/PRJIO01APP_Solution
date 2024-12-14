

BEGIN TRANSACTION;

CREATE TABLE "Courses" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Courses" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NULL
);

CREATE TABLE "Students" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Students" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL
);

CREATE TABLE "CoursesStudents" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_CoursesStudents" PRIMARY KEY AUTOINCREMENT,
    "StudentId" INTEGER NOT NULL,
    "CourseId" INTEGER NOT NULL,
    CONSTRAINT "FK_CoursesStudents_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CoursesStudents_Students_StudentId" FOREIGN KEY ("StudentId") REFERENCES "Students" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CoursesStudents_CourseId" ON "CoursesStudents" ("CourseId");

CREATE INDEX "IX_CoursesStudents_StudentId" ON "CoursesStudents" ("StudentId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241213180936_ManyToMany', '8.0.11');

COMMIT;