DROP TABLE IF EXISTS user_tasks;

CREATE TEMPORARY TABLE "user_tasks"
(
  "task_id" INTEGER NOT NULL,
  "text" TEXT not NULL,
  "indx" INTEGER PRIMARY KEY AUTOINCREMENT
);

INSERT INTO user_tasks (task_id, text)
  SELECT task_id, TEXT
  FROM   tasks
  WHERE user_id = 782172028;

SELECT * FROM user_tasks;