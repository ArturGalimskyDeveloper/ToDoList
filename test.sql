DROP TABLE IF EXISTS "tasks";
DROP TABLE IF EXISTS "users";

PRAGMA FOREIGN_KEYS=On;

CREATE TABLE "tasks"
(
    "task_id" INTEGER PRIMARY KEY AUTOINCREMENT,
    "text" TEXT NOT NULL,
    "user_id" INTEGER NOT NULL,
    FOREIGN KEY (user_id) 
        REFERENCES "users" (user_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE "users"
(
    "user_id" INTEGER PRIMARY KEY NOT NULL,
    "user_name" TEXT NOT NULL
);

INSERT INTO "users" (user_id, user_name)
VALUES (4444, "hans");

INSERT INTO "tasks" (text, user_id)
VALUES ('Make homework', 4444);

INSERT INTO "tasks" (text, user_id)
VALUES ('Find job', 4444);

