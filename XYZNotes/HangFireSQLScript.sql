USE [MyProjectApiHangFire]
GO
/****** Object:  Schema [HangFire]    Script Date: 7/26/2021 10:00:06 PM ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 7/26/2021 10:00:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded', 27, NULL)
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2021-07-26', 6, CAST(N'2021-08-26T13:58:07.357' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2021-07-26-13', 6, CAST(N'2021-07-27T13:58:07.357' AS DateTime))
GO
INSERT [HangFire].[Counter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded', 1, NULL)
INSERT [HangFire].[Counter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2021-07-26', 1, CAST(N'2021-08-26T13:59:07.447' AS DateTime))
INSERT [HangFire].[Counter] ([Key], [Value], [ExpireAt]) VALUES (N'stats:succeeded:2021-07-26-13', 1, CAST(N'2021-07-27T13:59:07.447' AS DateTime))
GO
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'CreatedAt', N'2021-05-09T14:20:22.6905514Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'Cron', N'* * * * *', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'Job', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":"[\"\\\"Database updated\\\"\"]"}', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'LastExecution', N'2021-07-26T13:59:07.4052186Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'LastJobId', N'28', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'NextExecution', N'2021-07-26T14:00:00.0000000Z', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'Queue', N'default', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'TimeZoneId', N'UTC', NULL)
INSERT [HangFire].[Hash] ([Key], [Field], [Value], [ExpireAt]) VALUES (N'recurring-job:Console.WriteLine', N'V', N'2', NULL)
GO
SET IDENTITY_INSERT [HangFire].[Job] ON 

INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (22, 71, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:53:51.940' AS DateTime), CAST(N'2021-07-27T13:53:52.080' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (23, 74, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:54:07.050' AS DateTime), CAST(N'2021-07-27T13:54:07.067' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (24, 77, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:55:07.117' AS DateTime), CAST(N'2021-07-27T13:55:07.153' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (25, 80, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:56:07.187' AS DateTime), CAST(N'2021-07-27T13:56:07.223' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (26, 83, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:57:07.263' AS DateTime), CAST(N'2021-07-27T13:57:07.297' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (27, 86, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:58:07.327' AS DateTime), CAST(N'2021-07-27T13:58:07.357' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (28, 89, N'Succeeded', N'{"Type":"System.Console, System.Console, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a","Method":"WriteLine","ParameterTypes":"[\"System.String, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\"]","Arguments":null}', N'["\"Database updated\""]', CAST(N'2021-07-26T13:59:07.407' AS DateTime), CAST(N'2021-07-27T13:59:07.450' AS DateTime))
SET IDENTITY_INSERT [HangFire].[Job] OFF
GO
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (22, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (22, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (22, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (22, N'Time', N'1627307631')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (23, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (23, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (23, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (23, N'Time', N'1627307647')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (24, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (24, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (24, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (24, N'Time', N'1627307707')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (25, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (25, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (25, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (25, N'Time', N'1627307767')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (26, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (26, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (26, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (26, N'Time', N'1627307827')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (27, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (27, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (27, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (27, N'Time', N'1627307887')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (28, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (28, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (28, N'RecurringJobId', N'"Console.WriteLine"')
INSERT [HangFire].[JobParameter] ([JobId], [Name], [Value]) VALUES (28, N'Time', N'1627307947')
GO
INSERT [HangFire].[Schema] ([Version]) VALUES (7)
GO
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2021-07-26T13:53:51.2931685Z"}', CAST(N'2021-07-26T13:59:51.920' AS DateTime))
GO
INSERT [HangFire].[Set] ([Key], [Score], [Value], [ExpireAt]) VALUES (N'recurring-jobs', 1627308000, N'Console.WriteLine', NULL)
GO
SET IDENTITY_INSERT [HangFire].[State] ON 

INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (69, 22, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:53:51.963' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:53:51.9558191Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (70, 22, N'Processing', NULL, CAST(N'2021-07-26T13:53:52.050' AS DateTime), N'{"StartedAt":"2021-07-26T13:53:52.0370454Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"88258f1b-570c-4e1e-b60a-b25bfdc83731"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (71, 22, N'Succeeded', NULL, CAST(N'2021-07-26T13:53:52.080' AS DateTime), N'{"SucceededAt":"2021-07-26T13:53:52.0731645Z","PerformanceDuration":"17","Latency":"115"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (72, 23, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:54:07.050' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:54:07.0494376Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (73, 23, N'Processing', NULL, CAST(N'2021-07-26T13:54:07.057' AS DateTime), N'{"StartedAt":"2021-07-26T13:54:07.0533520Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"88258f1b-570c-4e1e-b60a-b25bfdc83731"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (74, 23, N'Succeeded', NULL, CAST(N'2021-07-26T13:54:07.067' AS DateTime), N'{"SucceededAt":"2021-07-26T13:54:07.0636843Z","PerformanceDuration":"4","Latency":"8"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (75, 24, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:55:07.120' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:55:07.1211617Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (76, 24, N'Processing', NULL, CAST(N'2021-07-26T13:55:07.140' AS DateTime), N'{"StartedAt":"2021-07-26T13:55:07.1382913Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"648873a6-02f2-45ab-8d43-73051de33880"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (77, 24, N'Succeeded', NULL, CAST(N'2021-07-26T13:55:07.153' AS DateTime), N'{"SucceededAt":"2021-07-26T13:55:07.1495270Z","PerformanceDuration":"4","Latency":"27"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (78, 25, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:56:07.197' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:56:07.1961016Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (79, 25, N'Processing', NULL, CAST(N'2021-07-26T13:56:07.210' AS DateTime), N'{"StartedAt":"2021-07-26T13:56:07.2088041Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"86d477b4-d63a-4436-8f34-4ed49e01f814"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (80, 25, N'Succeeded', NULL, CAST(N'2021-07-26T13:56:07.223' AS DateTime), N'{"SucceededAt":"2021-07-26T13:56:07.2196703Z","PerformanceDuration":"4","Latency":"27"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (81, 26, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:57:07.267' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:57:07.2674696Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (82, 26, N'Processing', NULL, CAST(N'2021-07-26T13:57:07.283' AS DateTime), N'{"StartedAt":"2021-07-26T13:57:07.2798443Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"86d477b4-d63a-4436-8f34-4ed49e01f814"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (83, 26, N'Succeeded', NULL, CAST(N'2021-07-26T13:57:07.297' AS DateTime), N'{"SucceededAt":"2021-07-26T13:57:07.2906486Z","PerformanceDuration":"4","Latency":"22"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (84, 27, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:58:07.330' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:58:07.3296283Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (85, 27, N'Processing', NULL, CAST(N'2021-07-26T13:58:07.347' AS DateTime), N'{"StartedAt":"2021-07-26T13:58:07.3432809Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"86d477b4-d63a-4436-8f34-4ed49e01f814"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (86, 27, N'Succeeded', NULL, CAST(N'2021-07-26T13:58:07.357' AS DateTime), N'{"SucceededAt":"2021-07-26T13:58:07.3533379Z","PerformanceDuration":"4","Latency":"22"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (87, 28, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2021-07-26T13:59:07.413' AS DateTime), N'{"EnqueuedAt":"2021-07-26T13:59:07.4118471Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (88, 28, N'Processing', NULL, CAST(N'2021-07-26T13:59:07.430' AS DateTime), N'{"StartedAt":"2021-07-26T13:59:07.4282200Z","ServerId":"maximus-xi:6960:aece7ac9-2344-409e-a268-5f1a99993a03","WorkerId":"bb52f2d2-743d-4d56-b7b0-21bdb4e5d10f"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (89, 28, N'Succeeded', NULL, CAST(N'2021-07-26T13:59:07.450' AS DateTime), N'{"SucceededAt":"2021-07-26T13:59:07.4445866Z","PerformanceDuration":"5","Latency":"32"}')
SET IDENTITY_INSERT [HangFire].[State] OFF
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
