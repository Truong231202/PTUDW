
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2023 09:19:48
-- Generated from EDMX file: C:\Users\Quang Truong\Semester 4\PTUDW\PTUDW\ModelFirst\ModelFirst\Models\QuangTruong.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_NhanVienPhongBan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhongBans] DROP CONSTRAINT [FK_NhanVienPhongBan];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[NhanViens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NhanViens];
GO
IF OBJECT_ID(N'[dbo].[PhongBans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhongBans];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'NhanViens'
CREATE TABLE [dbo].[NhanViens] (
    [maNhanVien] int IDENTITY(1,1) NOT NULL,
    [tenNhanVien] nvarchar(max)  NOT NULL,
    [ngaysinh] nvarchar(max)  NOT NULL,
    [luong] nvarchar(max)  NOT NULL,
    [hinhanh] nvarchar(max)  NOT NULL,
    [maPhong] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PhongBans'
CREATE TABLE [dbo].[PhongBans] (
    [maPhong] int IDENTITY(1,1) NOT NULL,
    [tenPhong] nvarchar(max)  NOT NULL,
    [NhanVien_maNhanVien] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [maNhanVien] in table 'NhanViens'
ALTER TABLE [dbo].[NhanViens]
ADD CONSTRAINT [PK_NhanViens]
    PRIMARY KEY CLUSTERED ([maNhanVien] ASC);
GO

-- Creating primary key on [maPhong] in table 'PhongBans'
ALTER TABLE [dbo].[PhongBans]
ADD CONSTRAINT [PK_PhongBans]
    PRIMARY KEY CLUSTERED ([maPhong] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NhanVien_maNhanVien] in table 'PhongBans'
ALTER TABLE [dbo].[PhongBans]
ADD CONSTRAINT [FK_NhanVienPhongBan]
    FOREIGN KEY ([NhanVien_maNhanVien])
    REFERENCES [dbo].[NhanViens]
        ([maNhanVien])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NhanVienPhongBan'
CREATE INDEX [IX_FK_NhanVienPhongBan]
ON [dbo].[PhongBans]
    ([NhanVien_maNhanVien]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------