    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DataBase')
  BEGIN
    CREATE DATABASE LsiDatabase


    END
    GO
       USE LsiDatabase
    GO
--You need to check if the table exists
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='dbo.Report' and xtype='U')
BEGIN
CREATE TABLE [dbo].[Report](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserExp] [varchar](200) NOT NULL,
	[Local] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END