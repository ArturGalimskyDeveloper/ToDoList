
IF OBJECT_ID('dbo.Tasks', 'U') IS NOT NULL
DROP TABLE dbo.Tasks;

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users;

CREATE TABLE dbo.Tasks
(
    task_id INT NOT NULL IDENTITY(1,1),
    task_text [VARCHAR](30) NOT NULL,
    user_id INT NOT NULL
);

CREATE TABLE dbo.Users
(
    user_id INT NOT NULL,
    user_name [VARCHAR](30) NOT NULL
);

ALTER TABLE dbo.Users
    ADD CONSTRAINT PK_Users
    PRIMARY KEY(user_id);   
ALTER TABLE dbo.Tasks
    ADD CONSTRAINT FK_Tasks_Users
    FOREIGN KEY(user_id)
    REFERENCES dbo.Users(user_id)
    ON DELETE CASCADE
    ON UPDATE CASCADE;