DROP TABLE IF EXISTS "Tasks";

CREATE TABLE "Tasks"
(
    "task_id" INTEGER PRIMARY KEY NOT NULL,
    "text" TEXT NOT NULL,
    "date" TEXT NOT NULL
);

INSERT INTO "tasks" (task_id, text, date)
VALUES (1, 'Make homework', '22.02.11');

INSERT INTO "tasks" (task_id, text, date)
VALUES (2, 'Find job', '22.04.21');
