USE [SystemProgramming_01]
GO
/****** Object:  Table [dbo].[DataScienceTable]    Script Date: 22.02.2018 16:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataScienceTable](
	[DataScienceId] [int] IDENTITY(1,1) NOT NULL,
	[MemorySize] [bigint] NOT NULL,
 CONSTRAINT [PK_DataScienceTable] PRIMARY KEY CLUSTERED 
(
	[DataScienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Processes]    Script Date: 22.02.2018 16:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Processes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProcessId] [int] NOT NULL,
	[ProcessName] [varchar](64) NOT NULL,
	[ProcessMemorySize] [bigint] NOT NULL,
 CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[DataScienceTable] ON 

INSERT [dbo].[DataScienceTable] ([DataScienceId], [MemorySize]) VALUES (1, 115)
SET IDENTITY_INSERT [dbo].[DataScienceTable] OFF
SET IDENTITY_INSERT [dbo].[Processes] ON 

INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (3, 6528, N'chrome', 78)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (4, 848, N'chrome', 78)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (5, 848, N'chrome', 78)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (6, 848, N'chrome', 78)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (7, 848, N'chrome', 78)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (8, 8084, N'chrome', 107)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (9, 608, N'chrome', 85)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (114, 808, N'chrome', 147)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (115, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (116, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (117, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (118, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (119, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (120, 808, N'chrome', 148)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (121, 2308, N'chrome', 87)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (122, 9448, N'chrome', 176)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (123, 9328, N'chrome', 91)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (124, 1440, N'chrome', 86)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (125, 2952, N'chrome', 87)
INSERT [dbo].[Processes] ([Id], [ProcessId], [ProcessName], [ProcessMemorySize]) VALUES (126, 12912, N'chrome', 87)
SET IDENTITY_INSERT [dbo].[Processes] OFF
/****** Object:  Trigger [dbo].[GetTensRow]    Script Date: 22.02.2018 16:30:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   CREATE trigger [dbo].[GetTensRow]
   on [dbo].[Processes] after INSERT
   as
   DECLARE @AVERAGEmem int
   BEGIN
	if((select i.Id from inserted i)%10=0)
		BEGIN
		print 'Десятый элемент'
			Select @AVERAGEmem=AVG(p.ProcessMemorySize) from Processes p
			insert into DataScienceTable
			values(@AVERAGEmem)
		END
	else
		BEGIN	
			print 'Это не десятый элемент таблицы'
		END
	
   END
   
GO
ALTER TABLE [dbo].[Processes] ENABLE TRIGGER [GetTensRow]
GO
