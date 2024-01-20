USE [master]
GO

/****** Object:  Database [ShoppingCart]    Script Date: 20/01/2024 13:39:34 ******/
DROP DATABASE [ShoppingCart]
GO

/****** Object:  Database [ShoppingCart]    Script Date: 20/01/2024 13:39:34 ******/
CREATE DATABASE [ShoppingCart]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoppingCart', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ShoppingCart.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShoppingCart_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ShoppingCart_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoppingCart].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ShoppingCart] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ShoppingCart] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ShoppingCart] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ShoppingCart] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ShoppingCart] SET ARITHABORT OFF 
GO

ALTER DATABASE [ShoppingCart] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ShoppingCart] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ShoppingCart] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ShoppingCart] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ShoppingCart] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ShoppingCart] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ShoppingCart] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ShoppingCart] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ShoppingCart] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ShoppingCart] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ShoppingCart] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ShoppingCart] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ShoppingCart] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ShoppingCart] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ShoppingCart] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ShoppingCart] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ShoppingCart] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ShoppingCart] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ShoppingCart] SET  MULTI_USER 
GO

ALTER DATABASE [ShoppingCart] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ShoppingCart] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ShoppingCart] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ShoppingCart] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ShoppingCart] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ShoppingCart] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [ShoppingCart] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ShoppingCart] SET  READ_WRITE 
GO

