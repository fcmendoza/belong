use belong
go

IF NOT EXISTS (select 1 from sys.tables where name = 'Host') 
BEGIN
    CREATE TABLE Host (
        [ID]        int IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Host] PRIMARY KEY,
        [Name]      varchar(100) not null,
        [CreatedOn] datetime not null CONSTRAINT [DF_Host_CreatedOn] DEFAULT (GETUTCDATE())
    )
END
ELSE BEGIN
    print 'Table Host created.'
END
GO

IF NOT EXISTS (select 1 from sys.tables where name = 'Guest')
BEGIN
    CREATE TABLE Guest (
        ID          int IDENTITY(1,1) not null CONSTRAINT [PK_Guest] PRIMARY KEY,
        [Name]      varchar(100) not null,
        [CreatedOn] datetime not null CONSTRAINT [DF_Guest_CreatedOn] DEFAULT (GETUTCDATE())
    )
END
ELSE BEGIN
    print 'Table Guest created.'
END
GO

declare @count int = 1
while (@count < 10) BEGIN
    insert into Host (Name) VALUES ('Jon' + cast(@count as varchar) + ' Host') 
    set @count = @count + 1
END

set @count = 1
while (@count <= 10) BEGIN
    insert into Guest (Name) VALUES ('Jon' + cast(@count as varchar) + ' Guest') 
    set @count = @count + 1
END


select top 10 * from Host (nolock)
select top 10 * from Guest (nolock)

-- truncate table Host
-- truncate table Guest
