DROP TABLE IF EXISTS "tasks";
DROP TABLE IF EXISTS "users";

PRAGMA FOREIGN_KEYS=On;

CREATE TABLE "tasks"
(
    "task_id" INTEGER PRIMARY KEY NOT NULL,
    "text" TEXT NOT NULL,
    "done" INTEGER NOT NULL,
    "user_id" INTEGER NOT NULL,
    "date" TEXT NOT NULL,
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

INSERT INTO "tasks" (task_id, text, done, user_id, date)
VALUES (1, 'Make homework', 0, 4444, "23.02.2021");

INSERT INTO "tasks" (task_id, text, done, user_id, date)
VALUES (2, 'Find job', 1, 4444, "23.02.2021");
