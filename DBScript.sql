USE [master]
GO
/****** Object:  Database [ProjectA]    Script Date: 5/28/2024 1:41:23 PM ******/
CREATE DATABASE [ProjectA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProjectA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProjectA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProjectA] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectA] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectA] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectA] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjectA', N'ON'
GO
ALTER DATABASE [ProjectA] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProjectA] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProjectA]
GO
/****** Object:  Table [dbo].[Advisor]    Script Date: 5/28/2024 1:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advisor](
	[Id] [int] NOT NULL,
	[Designation] [int] NOT NULL,
	[Salary] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluation]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[TotalWeightage] [int] NOT NULL,
 CONSTRAINT [PK_Evaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created_On] [date] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupEvaluation]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupEvaluation](
	[GroupId] [int] NOT NULL,
	[EvaluationId] [int] NOT NULL,
	[ObtainedMarks] [int] NOT NULL,
	[EvaluationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupEvaluation] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[EvaluationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupProject]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupProject](
	[ProjectId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupProject] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupStudent]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupStudent](
	[GroupId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupStudent] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NULL,
	[Contact] [varchar](20) NULL,
	[Email] [varchar](30) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectAdvisor]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAdvisor](
	[AdvisorId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AdvisorRole] [int] NOT NULL,
	[AssignmentDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProjectAdvisor] PRIMARY KEY CLUSTERED 
(
	[AdvisorId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/28/2024 1:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] NOT NULL,
	[RegistrationNo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Advisor] ([Id], [Designation], [Salary]) VALUES (99, 9, CAST(57000 AS Decimal(18, 0)))
INSERT [dbo].[Advisor] ([Id], [Designation], [Salary]) VALUES (100, 9, CAST(50000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Evaluation] ON 

INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (1, N'Mid Term', 30, 30)
INSERT [dbo].[Evaluation] ([Id], [Name], [TotalMarks], [TotalWeightage]) VALUES (2, N'Final Term', 40, 40)
SET IDENTITY_INSERT [dbo].[Evaluation] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [Created_On]) VALUES (1, CAST(N'2024-04-29' AS Date))
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
INSERT [dbo].[GroupEvaluation] ([GroupId], [EvaluationId], [ObtainedMarks], [EvaluationDate]) VALUES (1, 1, 24, CAST(N'2024-04-29T15:22:23.687' AS DateTime))
INSERT [dbo].[GroupEvaluation] ([GroupId], [EvaluationId], [ObtainedMarks], [EvaluationDate]) VALUES (1, 2, 35, CAST(N'2024-04-29T15:22:31.670' AS DateTime))
GO
INSERT [dbo].[GroupProject] ([ProjectId], [GroupId], [AssignmentDate]) VALUES (1, 1, CAST(N'2024-04-29T15:21:20.917' AS DateTime))
GO
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (1, 12, 3, CAST(N'2024-04-29T15:21:37.353' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (1, 16, 3, CAST(N'2024-04-29T15:21:28.793' AS DateTime))
INSERT [dbo].[GroupStudent] ([GroupId], [StudentId], [Status], [AssignmentDate]) VALUES (1, 20, 3, CAST(N'2024-04-29T15:21:33.023' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Lookup] ON 

INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (1, N'Male', N'GENDER')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (2, N'Female', N'GENDER')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (3, N'Active', N'STATUS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (4, N'InActive', N'STATUS')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (6, N'Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (7, N'Associate Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (8, N'Assisstant Professor', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (9, N'Lecturer', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (10, N'Industry Professional', N'DESIGNATION')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (11, N'Main Advisor', N'ADVISOR_ROLE')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (12, N'Co-Advisror', N'ADVISOR_ROLE')
INSERT [dbo].[Lookup] ([Id], [Value], [Category]) VALUES (14, N'Industry Advisor', N'ADVISOR_ROLE')
SET IDENTITY_INSERT [dbo].[Lookup] OFF
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (1, N'Hafix M.Abdullah', N'Mughal', N'03274351956', N'muhammadabdullah740@gmail.com', CAST(N'2003-01-04T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (3, N'Noor', N'Fatima ', N'03100556522', N'noorfatima262004@gmail.com', CAST(N'2004-07-26T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (4, N'Syed Muhammad ', N'Danish ', N'03090425216 ', N'Syeddanish425@gmail.com ', CAST(N'2004-08-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (5, N'Muhammad ', N'Nouman', N'03228429291', N'2022cs49@student.uet.edu.pk', CAST(N'2004-08-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (6, N'Ehsan ', N'Ullah ', N'03431695809', N'ehsanullahcs04@gmail.com', CAST(N'2004-01-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (7, N'Muhammad', N'Junaid zia', N'0331 4129503', N'junaidzia697@gmail.com', CAST(N'2003-10-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (8, N'Aleena ', N'Sheikh', N'03353323290', N'aleenasheikh499@gmail.com', CAST(N'2005-03-04T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (9, N'Muhammad Ali', N'Tariq', N'03334353825', N'm.alit9876@gmail.com', CAST(N'2004-03-22T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (11, N'Shahzaib', N'Saleem', N'03240585600', N'maliksha1199@gmail.com', CAST(N'2003-08-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (12, N'Muhammad ', N'Hussnain ', N'03314212155', N'2022cs24@student.uet.edu.pk', CAST(N'2005-12-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (13, N'Shehroz', N'Ahmad', N'03124676733', N'sheryidk123@gmail.com', CAST(N'2004-02-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (14, N'Muhammad ', N'Tayyab', N'03074369732', N'tu.haider138@gmail.com', CAST(N'2003-10-23T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (15, N'Danish', N'Akram', N'03187254030', N'danishalizx4@gmail.com', CAST(N'2002-06-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (16, N'Hassan ', N'Subhan ', N'03154194442', N'hs3837358@gmail.com', CAST(N'2005-07-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (17, N'MUHAMMAD ', N'AHMAD ', N'03305009026 ', N'Mahmadmughal0987@gmail.com ', CAST(N'2004-09-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (18, N'Minahil', N'Afzal', N'03258188410', N'Minahilafazal27@gmail.com', CAST(N'2005-09-27T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (19, N'Mian Muhammad', N'Ashhad', N'03260877889', N'mianashhad7@gmail.com', CAST(N'2005-06-24T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (20, N'Saba ', N'Shahdin ', N'+92 326 0202201', N'sabashahdin20@gmail.com', CAST(N'2004-06-22T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (21, N'ABUBAKAR', N'SAJID ', N'03084306708', N'abubakarsajid12345@gmail.com', CAST(N'2004-12-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (22, N'Umaima ', N'Noor', N'0305 4233306 ', N'umaimanoor42@gmail.com', CAST(N'2004-06-28T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (23, N'Fatima ', N'Shahid', N'03020482709', N'fatimashahid1040@gmail.com', CAST(N'2004-10-04T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (24, N'Muhammad Saad ', N'Akmal ', N'03304133887', N'saadakmal460@gmail.com', CAST(N'2004-10-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (25, N'Bilal', N'Ahmad', N'03249891105', N'ba298466@gmail.com', CAST(N'2003-12-22T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (26, N'Khadija', N'Imran', N'03104026320', N'thankyouparrot23239@gmail.com', CAST(N'2003-01-18T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (27, N'GHULAM ', N'MUSTAFA ', N'03096597530 ', N'Captaingmsandhu@gmail.com ', CAST(N'2003-02-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (28, N'Muhammad Asim Ali', N'Murtaza', N'03173524567', N'mo.asim@gmail.com', CAST(N'2000-12-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (29, N'Muhammad', N'Abubakar', N'03097892590', N'Abubakarilyas334@gmail.com ', CAST(N'2002-07-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (30, N'Arham', N'Imran', N'03245521508', N'imranarham798@gmail.com', CAST(N'2003-03-26T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (31, N'Tayyba', N'Haider', N'03082325274', N'haideruet73.055@gmail.com', CAST(N'2004-12-19T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (32, N'Faizan ', N'Ahmed ', N'03079767577', N'faizanahmed.pak1@gmail.com', CAST(N'2003-08-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (33, N'Rida', N'Mushtaq', N'03104877300', N'mushtaqrida06@gmail.com', CAST(N'2004-11-28T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (34, N'Mohammad Zaid', N'Sibghatullah', N'03164259818', N'sheikhzaid768@gmail.com', CAST(N'2002-08-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (35, N'Burhan', N'Ahmad', N'03244558318', N'burhanjutt141@gmail.com', CAST(N'2006-09-17T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (36, N'Noman', N'Ali', N'03001234987', N'nomanali@gmail.com', CAST(N'2006-02-03T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (37, N'Hamna', N'Kamran', N'03628262830', N'ham82nwu2@gmail.com', CAST(N'2023-02-17T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (38, N'Muhammad Taha', N'Saleem', N'03004567198', N'tahasaleeml@gmail.com', CAST(N'2005-07-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (39, N'Ayesha', N'Khalid', N'03444571722', N'aishakhalid988@gmail.com ', CAST(N'2003-02-27T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (40, N'Sami', N'Ullah', N'03477586056', N'samiullahglotar420@gmail.com', CAST(N'2004-04-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (41, N'Noor', N'Asghar', N'03044608290', N'noorasghar2004@gmail.com', CAST(N'2004-01-09T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (42, N'Kanza-tul-Islam', N'NA', N'03347550649', N'kanzagulzar582@gmail.com', CAST(N'2003-10-05T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (43, N'Muhammed ', N'Tayyab ', N'03008453640', N'tayyabashraf629@gmail.com', CAST(N'2003-07-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (44, N'Rafay', N'Butt', N'03200675478', N'Rafaybutt21@gmail.com ', CAST(N'2005-09-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (45, N'Mohammad', N'Abdullah', N'03099708894', N'ma645431@gmail.com', CAST(N'2004-06-18T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (46, N'Muhammad ', N'Awais ', N'03052808851', N'awaisjuttgjuttg0@gmail.com', CAST(N'2005-01-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (47, N'Rushba', N'Arshad', N'03227114536', N'rushbaarshad@gmail.com', CAST(N'2003-11-19T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (48, N'Shahbaz', N'Ali', N'03481512693', N'shahbazghafil@gmail.com', CAST(N'2005-07-24T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (49, N'Abdullah ', N'Azher Chaudhary ', N'03274534352', N'abdullahazher11@gmail.com', CAST(N'2004-06-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (50, N'Muhammad', N'Salman', N'03484569123', N'salmannawaz008@gmail.com', CAST(N'2004-08-19T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (51, N'Iqra', N'Tariq', N'03054897030', N'iqrat269@gmail.com', CAST(N'2003-12-10T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (52, N'Ariba ', N'Shafaqat ', N'03034369624', N'aribashafaqat4@gmail.com ', CAST(N'2004-05-30T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (53, N'Rizwan', N'Yaqoob', N'03054511688', N'2022cs176@student.uet.edu.pk', CAST(N'2004-06-08T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (54, N'Ifra ', N'Fazal', N'03019709107', N'ifrafazal113@gmail.com', CAST(N'2004-04-17T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (55, N'Muhammad ', N'Kamran', N'03437495636', N'muhammadkamran@gmail.com', CAST(N'2003-01-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (56, N'Ahmar', N'Shahid', N'+92 320 1046131', N'ahmarjan456@gmail.com', CAST(N'2003-05-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (57, N'Muhammad Toseef', N'Haider', N'03487162132', N'muhammadtoseef@gmail.com', CAST(N'2004-06-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (58, N'Bisma', N'Shakeel ', N'03019464826', N'sbisma751@gmail.com ', CAST(N'2004-11-09T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (59, N'Hussain ', N'Ali', N'03053513883', N'zeshanshl44@gmail.com', CAST(N'2003-01-21T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (60, N'M Soban', N'Akram', N'03079856818', N'chsobanakram@gmail.com', CAST(N'2004-02-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (61, N'Ghania', N'Khan Niazi', N'03092002299', N'gkniazi2005@gmail.com', CAST(N'2005-09-03T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (62, N'Muhammad Mudassir ', N'Naveed', N'03099603075', N'mudassirnaveed303@gmail.com', CAST(N'2003-08-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (63, N'Muhammad ', N'Zubair', N'03268550359', N'zubairjaved1014@gmail.com', CAST(N'2004-08-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (64, N'Muhammad Rauf', N'Muhammad Akbar', N'03176826604', N'mrauf6826604@gmail.com', CAST(N'2003-12-06T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (65, N'Jawad Ali', N'Amjad', N'03028090100', N'jawadali90@gmail.com', CAST(N'2003-05-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (66, N'Irtaza', N'Manzoor ', N'03252020436', N'irtaza.manzoor1203@gmail.com', CAST(N'2005-10-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (67, N'Maria Imran ', N'Muhammad Imran ', N'03344439940', N'imranmaria634@gmail.com', CAST(N'2004-07-28T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (68, N'Tazeem', N'Hussain', N'0305-4816180', N'tazeemh580@gmail.com', CAST(N'2006-02-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (69, N'Ameer Hamza ', N'Mehar', N'03430601827', N'hamzanaseer496@gmail.com', CAST(N'2004-01-08T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (70, N'Muhammad Furqan', N'Ishaq', N'0322 8553562', N'mfurqan@gmail.com', CAST(N'2002-12-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (71, N'Abdur Rehman ', N'Kazim ', N'03044858565', N'abdurrehman68@gmail.com', CAST(N'2003-04-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (72, N'Muskan ', N'Awais ', N'03036804492', N'muskanawais02@gmail.com', CAST(N'2005-02-04T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (73, N'Waleed', N'Ahmed', N'+92 3194674268', N'waah805@gmail.com', CAST(N'2005-01-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (74, N'Eeman ', N'Waheed', N'03234656585', N'eemanwaheed2003@gmail.com', CAST(N'2003-12-27T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (75, N'Areej', N'Fatima ', N'03045678933', N'Areej345@gmail.com', CAST(N'2004-02-19T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (76, N'Harmain', N'Iftikhar', N'03256784454', N'harmanrick81@gmail.com', CAST(N'2005-12-27T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (77, N'Muhammad Faizan Asim ', N'Asim Majeed ', N'03098273508', N'mr.faizan.asim@gmail.com', CAST(N'2004-08-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (78, N'Asad', N'Ullah', N'03074068411', N'asadullah7370@gmail.com', CAST(N'2004-03-31T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (79, N'Affan', N'Ahmed', N'03121234567', N'123@gmail.com', CAST(N'2005-12-31T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (80, N'HABIB ', N'UL REHMAN ', N'03154676984', N'habibulrehman71@gmail.com', CAST(N'2002-02-19T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (81, N'Fiza', N'Amjad', N'03234894774', N'fizaamjad377@gmail.com', CAST(N'2004-04-25T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (82, N'Zohaib', N'Arshad', N'03105600644', N'zohaibarshad2022@gmail.com', CAST(N'2003-12-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (83, N'Usama ', N'Mehboob ', N'03224684313', N'mehboobusama47@gmail.com', CAST(N'2004-06-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (84, N'Robass ', N'Atif', N'03266150854', N'robassatif@gmail.com', CAST(N'2004-01-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (85, N'Maryam', N'Mueen', N'03204328611', N'maryammueen07@gmail.com', CAST(N'2004-02-05T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (86, N'Abdullah', N'Fayyaz', N'03216546278', N'abdullahkhan98@gmail.com', CAST(N'2004-03-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (96, N'Maryam', N'Mueen', N'03204328611', N'maryammueen07@gmail.com', CAST(N'2004-02-05T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (98, N'Muhammad Adnan', N'Amin', N'03265145770', N'adnanamin.available@gmail.com', CAST(N'2004-01-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (99, N'Laeeq', N'Khan', N'03054567898', N'laeeq@gmail.com', CAST(N'1995-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Contact], [Email], [DateOfBirth], [Gender]) VALUES (100, N'Miada', N'Shahid', N'03243456787', N'maida@gmail.com', CAST(N'1999-07-07T00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([Id], [Description], [Title]) VALUES (1, N'Database Final Project', N'Banking Management System')
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (99, 1, 11, CAST(N'2024-04-29T15:21:06.653' AS DateTime))
INSERT [dbo].[ProjectAdvisor] ([AdvisorId], [ProjectId], [AdvisorRole], [AssignmentDate]) VALUES (100, 1, 12, CAST(N'2024-04-29T15:21:13.357' AS DateTime))
GO
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (1, N'2022-CS-35')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (3, N'2022-CS-8')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (4, N'2022-CS-46 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (5, N'2022-CS-49')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (6, N'2022-CS-36')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (7, N'2022-CS-2')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (8, N'2022-CS-130 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (9, N'2022-CS-22')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (11, N'2022-CS-124')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (12, N'2022-CS-24')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (13, N'2022-CS-157')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (14, N'2022-CS-133')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (15, N'2022-CS-25')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (16, N'2022-CS-117')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (17, N'2022-CS-144')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (18, N'2022-CS-160')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (19, N'2022-CS-114')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (20, N'2022-CS-112')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (21, N'2022-CS-125')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (22, N'2022-CS-147 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (23, N'2022_CS_142')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (24, N'2022-CS-148')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (25, N'2022-CS-170')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (26, N'2022-CS-171')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (27, N'2022-r-2021-CD-CS-2 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (28, N'2022-CS-175')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (29, N'2022-CS-188 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (30, N'2022-CS-209')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (31, N'2022-CS-198')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (32, N'2022-CS-120')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (33, N'2022-CS-168')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (34, N'2022R/2021-CS-214')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (35, N'2022-CS-191')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (36, N'2022-CS-166')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (37, N'2022-CS-210')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (38, N'2022-CS-139')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (39, N'2022-CS-153')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (40, N'2022CS143')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (41, N'2022-CS-140')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (42, N'2022-CS-42')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (43, N'2022-CS-135')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (44, N'2022-CS-290')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (45, N'2022-CS-155')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (46, N'2022-CS-30')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (47, N'2022-CS-141')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (48, N'2022-CS-27')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (49, N'2022-CS-204 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (50, N'2022-CS-138')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (51, N'2022-CS-29')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (52, N'2022-CS-113 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (53, N'2022-CS-176')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (54, N'2022-CS-194')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (55, N'2022-CS-53')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (56, N'2022-CS-206')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (57, N'2022-CS-16')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (58, N'2022-CS-50 ')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (59, N'2022-CS-129')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (60, N'2022-CS-173')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (61, N'2022-CS-156')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (62, N'2022-CS-32')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (63, N'2022-CS-20')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (64, N'2022-CS-37')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (65, N'2022-CS-183')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (66, N'2022-CS-189')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (67, N'2022-CS-14')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (68, N'2022-CS-128')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (69, N'2022-CS-17')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (70, N'2022R/2021-CS-199')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (71, N'2022-CS-115')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (72, N'2022-CS-38')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (73, N'2022-CS-41')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (74, N'2022-CS-52')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (75, N'2022-CS-177')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (76, N'2022-CS-07')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (77, N'2022CS111')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (78, N'2022-CS-11')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (79, N'2022-CS-178')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (80, N'2022-CS-201')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (81, N'2022-CS-172')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (82, N'2022-CS-51')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (83, N'2021-CS-10')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (84, N'2022-cs-150')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (85, N'2022-CS-05')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (86, N'2022-CS-21')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (96, N'2022-CS-05')
INSERT [dbo].[Student] ([Id], [RegistrationNo]) VALUES (98, N'2022-CS-13')
GO
ALTER TABLE [dbo].[Advisor]  WITH CHECK ADD  CONSTRAINT [FK_Advisor_Lookup] FOREIGN KEY([Designation])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Advisor] CHECK CONSTRAINT [FK_Advisor_Lookup]
GO
ALTER TABLE [dbo].[GroupEvaluation]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvaluation_Evaluation] FOREIGN KEY([EvaluationId])
REFERENCES [dbo].[Evaluation] ([Id])
GO
ALTER TABLE [dbo].[GroupEvaluation] CHECK CONSTRAINT [FK_GroupEvaluation_Evaluation]
GO
ALTER TABLE [dbo].[GroupEvaluation]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvaluation_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupEvaluation] CHECK CONSTRAINT [FK_GroupEvaluation_Group]
GO
ALTER TABLE [dbo].[GroupProject]  WITH CHECK ADD  CONSTRAINT [FK_GroupProject_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupProject] CHECK CONSTRAINT [FK_GroupProject_Group]
GO
ALTER TABLE [dbo].[GroupProject]  WITH CHECK ADD  CONSTRAINT [FK_GroupProject_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[GroupProject] CHECK CONSTRAINT [FK_GroupProject_Project]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_GroupStudents_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_GroupStudents_Lookup]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStudents_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_ProjectStudents_Group]
GO
ALTER TABLE [dbo].[GroupStudent]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStudents_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[GroupStudent] CHECK CONSTRAINT [FK_ProjectStudents_Student]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Lookup] FOREIGN KEY([Gender])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Lookup]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectAdvisor_Lookup] FOREIGN KEY([AdvisorRole])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectAdvisor_Lookup]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectAdvisor_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectAdvisor_Project]
GO
ALTER TABLE [dbo].[ProjectAdvisor]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTeachers_Teacher] FOREIGN KEY([AdvisorId])
REFERENCES [dbo].[Advisor] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdvisor] CHECK CONSTRAINT [FK_ProjectTeachers_Teacher]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Person] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Person]
GO
USE [master]
GO
ALTER DATABASE [ProjectA] SET  READ_WRITE 
GO
