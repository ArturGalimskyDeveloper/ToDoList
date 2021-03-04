
IF OBJECT_ID('dbo.Tasks', 'U') IS NOT NULL
DROP TABLE dbo.Tasks
GO

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users
GO

CREATE TABLE dbo.Tasks
(
    task_id INT NOT NULL IDENTITY(1,1),
    task_text [VARCHAR](30) NOT NULL,
    user_id INT NOT NULL
);
GO

CREATE TABLE dbo.Users
(
    user_id INT NOT NULL,
    user_name TEXT NOT NULL
);
GO

ALTER TABLE dbo.Users
    ADD CONSTRAINT PK_Users
    PRIMARY KEY(user_id);   
ALTER TABLE dbo.Tasks
    ADD CONSTRAINT FK_Tasks_Users
    FOREIGN KEY(user_id)
    REFERENCES dbo.Users(user_id)
    ON DELETE CASCADE
    ON UPDATE CASCADE
GO


INSERT INTO dbo.Users
([user_id], [user_name])
VALUES
(4444, 'Hans')
GO

INSERT INTO dbo.Tasks
([task_text], [user_id])
VALUES
('create t-sql query', 4444),
('run t-sql query', 4444),
('read Itzik article', 4444)
GO