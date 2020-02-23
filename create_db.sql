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

if not exists (select top 1 * from Host (nolock)) begin
	declare @count int = 1
	while (@count < 10) begin
	    insert into Host (Name) VALUES ('Jon' + cast(@count as varchar) + ' Host')
	    set @count = @count + 1
	end
end
GO

if not exists (select top 1 * from Guest (nolock)) begin
	declare @count int = 1
	set @count = 1
	while (@count <= 10) begin
	    insert into Guest (Name) VALUES ('Jon' + cast(@count as varchar) + ' Guest')
	    set @count = @count + 1
	end
end
GO

-- Houses hosts have/own/manage.
if not exists (select 1 from sys.tables where name = 'House') begin
	CREATE TABLE House (
		[ID] int identity(1,1) not null constraint [PK_House] primary key,
		[HostId] int not null constraint [FK_House_Host] foreign key (HostId) references Host(ID),
		[Address1] varchar(100) not null,
		[Address2] varchar(100) null,
		[City]     varchar(100) not null,
		[State]    varchar(50)  not null,
		[ZipCode]  char(5)      not null,
		[CreatedOn] datetime not null CONSTRAINT [DF_House_CreatedOn] DEFAULT (GETUTCDATE())
	)
end
GO

if not exists (select top 1 * from House (nolock)) begin
	declare @count int = 1
	while (@count < 10) begin
		INSERT INTO House (HostId, Address1, City, State, ZipCode)
		VALUES (@count, '311' + cast(@count as varchar) + ' SACRAMENTO ST', 'SAN FRANCISCO', 'CA', '94115')
	    set @count = @count + 1
	end
end

if exists (select 1 from sys.procedures where name = 'GetHousesByHost') begin
	DROP PROCEDURE GetHousesByHost
end
GO

CREATE PROCEDURE GetHousesByHost (
	@HostID int
)
AS
BEGIN
	SELECT h.ID, h.HostId, hs.Name, (h.Address1 + ', ' + h.City + ', ' + h.State + ' ' + h.ZipCode) as Address
	FROM House h (nolock)
	INNER JOIN Host hs (nolock) ON hs.ID = h.hostId
	WHERE h.HostId = @HostID
END
GO

