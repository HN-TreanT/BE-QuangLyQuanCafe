USE [master]
GO
/****** Object:  Database [Cafe]    Script Date: 5/5/2023 6:53:00 PM ******/
CREATE DATABASE [Cafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cafe.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Cafe] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cafe] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cafe] SET RECOVERY FULL 
GO
ALTER DATABASE [Cafe] SET  MULTI_USER 
GO
ALTER DATABASE [Cafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cafe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Cafe', N'ON'
GO
ALTER DATABASE [Cafe] SET QUERY_STORE = ON
GO
ALTER DATABASE [Cafe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Cafe]
GO
/****** Object:  User [HoangNam]    Script Date: 5/5/2023 6:53:00 PM ******/
CREATE USER [HoangNam] FOR LOGIN [HoangNam] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [HoangNam]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[account]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[id] [char](10) NOT NULL,
	[displayName] [nvarchar](50) NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[active] [tinyint] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__account__3213E83F2188C266] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[idCategory] [char](10) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__category__79D361B6AF4F7F78] PRIMARY KEY CLUSTERED 
(
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[idCustomer] [char](10) NOT NULL,
	[fullname] [nvarchar](50) NULL,
	[gender] [nvarchar](16) NULL,
	[phone_number] [nvarchar](16) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__customer__D058768637199FBB] PRIMARY KEY CLUSTERED 
(
	[idCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detailImportGoods]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detailImportGoods](
	[idDetailImportGoods] [char](10) NOT NULL,
	[id_material] [char](10) NULL,
	[PhoneProvider] [nvarchar](20) NULL,
	[NameProvider] [nvarchar](max) NULL,
	[amount] [int] NULL,
	[price] [float] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__detailIm__8C8889A9EFF1495D] PRIMARY KEY CLUSTERED 
(
	[idDetailImportGoods] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[IdMaterial] [char](10) NOT NULL,
	[NameMaterial] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[amount] [float] NULL,
	[unit] [nvarchar](10) NULL,
	[expiry] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[IdMaterial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[idOrderDetail] [char](10) NOT NULL,
	[id_order] [char](10) NULL,
	[id_product] [char](10) NULL,
	[price] [float] NULL,
	[amout] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__order_de__D04A4263C154FAE8] PRIMARY KEY CLUSTERED 
(
	[idOrderDetail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[idOrder] [char](10) NOT NULL,
	[id_customer] [char](10) NULL,
	[price] [nchar](10) NULL,
	[id_table] [char](10) NULL,
	[TimePay] [datetime] NULL,
	[status] [tinyint] NULL,
	[amount] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__orders__C8AAF6FF62CB1FB7] PRIMARY KEY CLUSTERED 
(
	[idOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[idProduct] [char](10) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[thumbnail] [nvarchar](500) NULL,
	[description] [text] NULL,
	[price] [float] NULL,
	[status] [tinyint] NULL,
	[unit] [nvarchar](20) NULL,
	[id_category] [char](10) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__product__5EEC79D1CE06E0A2] PRIMARY KEY CLUSTERED 
(
	[idProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectedWorkShift]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectedWorkShift](
	[idStaff] [char](10) NULL,
	[IdWorkShift] [int] NULL,
	[IdSeletedWorkShift] [char](10) NOT NULL,
 CONSTRAINT [PK_SelectedWorkShift] PRIMARY KEY CLUSTERED 
(
	[IdSeletedWorkShift] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[staff](
	[idStaff] [char](10) NOT NULL,
	[fullname] [nvarchar](50) NULL,
	[birthday] [date] NULL,
	[address] [nvarchar](200) NULL,
	[email] [nvarchar](150) NULL,
	[gender] [nvarchar](16) NULL,
	[phone_number] [nvarchar](16) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[salary] [float] NULL,
	[pathImage] [nvarchar](max) NULL,
 CONSTRAINT [PK__staff__98C886A9CB905920] PRIMARY KEY CLUSTERED 
(
	[idStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tableFood]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tableFood](
	[idTable] [char](10) NOT NULL,
	[name] [int] NULL,
	[status] [tinyint] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__tableFoo__716BDE24A16D201C] PRIMARY KEY CLUSTERED 
(
	[idTable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenInfo]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NOT NULL,
	[RefreshTokenExpiry] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TokenInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UseMaterial]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UseMaterial](
	[IdUseMaterial] [char](10) NOT NULL,
	[id_product] [char](10) NULL,
	[id_material] [char](10) NULL,
	[amount] [float] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_UseMaterial] PRIMARY KEY CLUSTERED 
(
	[IdUseMaterial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkShift]    Script Date: 5/5/2023 6:53:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkShift](
	[IdWorkShift] [int] NOT NULL,
	[ArrivalTime] [time](7) NULL,
	[TimeOn] [time](7) NULL,
 CONSTRAINT [PK_WorkShift] PRIMARY KEY CLUSTERED 
(
	[IdWorkShift] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF__account__display__08B54D69]  DEFAULT ('admin') FOR [displayName]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF__account__isAdmin__09A971A2]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF__account__created__0A9D95DB]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF__account__updated__0B91BA14]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [DF__category__create__3C69FB99]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [DF__category__update__3D5E1FD2]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF__customer__create__5165187F]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF__customer__update__52593CB8]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[detailImportGoods] ADD  CONSTRAINT [DF__detailImp__creat__68487DD7]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[detailImportGoods] ADD  CONSTRAINT [DF__detailImp__updat__693CA210]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[Material] ADD  CONSTRAINT [DF_Material_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[Material] ADD  CONSTRAINT [DF_Material_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Material] ADD  CONSTRAINT [DF_Material_updated_at]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__creat__628FA481]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[order_detail] ADD  CONSTRAINT [DF__order_det__updat__6383C8BA]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF__orders__created___5CD6CB2B]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF__orders__updated___5DCAEF64]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__created__49C3F6B7]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF__product__updated__4AB81AF0]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[staff] ADD  CONSTRAINT [DF__staff__created_a__4D94879B]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[staff] ADD  CONSTRAINT [DF__staff__updated_a__4E88ABD4]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[tableFood] ADD  CONSTRAINT [DF__tableFood__statu__5535A963]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[tableFood] ADD  CONSTRAINT [DF__tableFood__creat__5629CD9C]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tableFood] ADD  CONSTRAINT [DF__tableFood__updat__571DF1D5]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[UseMaterial] ADD  CONSTRAINT [DF_UseMaterial_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[UseMaterial] ADD  CONSTRAINT [DF_UseMaterial_updated_at]  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[detailImportGoods]  WITH CHECK ADD  CONSTRAINT [FK_detailImportGoods_Material] FOREIGN KEY([id_material])
REFERENCES [dbo].[Material] ([IdMaterial])
GO
ALTER TABLE [dbo].[detailImportGoods] CHECK CONSTRAINT [FK_detailImportGoods_Material]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK__order_det__id_or__60A75C0F] FOREIGN KEY([id_order])
REFERENCES [dbo].[orders] ([idOrder])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK__order_det__id_or__60A75C0F]
GO
ALTER TABLE [dbo].[order_detail]  WITH CHECK ADD  CONSTRAINT [FK__order_det__id_pr__619B8048] FOREIGN KEY([id_product])
REFERENCES [dbo].[product] ([idProduct])
GO
ALTER TABLE [dbo].[order_detail] CHECK CONSTRAINT [FK__order_det__id_pr__619B8048]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK__orders__id_custo__5AEE82B9] FOREIGN KEY([id_customer])
REFERENCES [dbo].[customer] ([idCustomer])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK__orders__id_custo__5AEE82B9]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK__orders__id_table__5BE2A6F2] FOREIGN KEY([id_table])
REFERENCES [dbo].[tableFood] ([idTable])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK__orders__id_table__5BE2A6F2]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK__product__id_cate__48CFD27E] FOREIGN KEY([id_category])
REFERENCES [dbo].[category] ([idCategory])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK__product__id_cate__48CFD27E]
GO
ALTER TABLE [dbo].[SelectedWorkShift]  WITH CHECK ADD  CONSTRAINT [FK_SelectedWorkShift_staff] FOREIGN KEY([idStaff])
REFERENCES [dbo].[staff] ([idStaff])
GO
ALTER TABLE [dbo].[SelectedWorkShift] CHECK CONSTRAINT [FK_SelectedWorkShift_staff]
GO
ALTER TABLE [dbo].[SelectedWorkShift]  WITH CHECK ADD  CONSTRAINT [FK_SelectedWorkShift_WorkShift] FOREIGN KEY([IdWorkShift])
REFERENCES [dbo].[WorkShift] ([IdWorkShift])
GO
ALTER TABLE [dbo].[SelectedWorkShift] CHECK CONSTRAINT [FK_SelectedWorkShift_WorkShift]
GO
ALTER TABLE [dbo].[UseMaterial]  WITH CHECK ADD  CONSTRAINT [FK_UseMaterial_Material1] FOREIGN KEY([id_material])
REFERENCES [dbo].[Material] ([IdMaterial])
GO
ALTER TABLE [dbo].[UseMaterial] CHECK CONSTRAINT [FK_UseMaterial_Material1]
GO
ALTER TABLE [dbo].[UseMaterial]  WITH CHECK ADD  CONSTRAINT [FK_UseMaterial_product] FOREIGN KEY([id_product])
REFERENCES [dbo].[product] ([idProduct])
GO
ALTER TABLE [dbo].[UseMaterial] CHECK CONSTRAINT [FK_UseMaterial_product]
GO
USE [master]
GO
ALTER DATABASE [Cafe] SET  READ_WRITE 
GO
