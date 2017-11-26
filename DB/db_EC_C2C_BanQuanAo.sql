use master

go
IF EXISTS(select * from sys.databases where name='EC_C2C_BanQuanAo')
DROP DATABASE EC_C2C_BanQuanAo

go
create database EC_C2C_BanQuanAo

go
use EC_C2C_BanQuanAo

go
create table LoaiTK
(
	MaLoai int identity primary key not null,
	TenLoai nvarchar(50)
)

go
insert into LoaiTK (TenLoai)
values 
(N'Quản trị viên'),
(N'Quản lý'),
(N'Người bán'),
(N'Người mua')

go
create table TaiKhoan
(
	MaTK int identity primary key not null,
	LoaiTK int,
	TenNguoiDung varchar(30),
	MatKhau varchar(20),
	TenDayDu nvarchar(50),
	DiaChi nvarchar(50),
	Email nvarchar(50),
	NgaySinh date,
	SDT nvarchar(15),
	CMND nvarchar(20),
	NgayDangKy date,
	NgayDanhGia date,
	TongTinDaMua int default 0,
	TongTinKM int default 0,
	DiemDanhGia decimal(7,1),
	TinhTrang int
			
	foreign key (LoaiTK) references dbo.LoaiTK(MaLoai)
)

go
insert into TaiKhoan (LoaiTK,TenNguoiDung,MatKhau,TenDayDu,Email,NgaySinh,SDT,CMND,NgayDangKy,NgayDanhGia,TongTinDaMua)
values 
(1,'admin','123',null,null,null,null,null,null,null,null),
(1,'quanly','123',null,null,null,null,null,null,null,null),
(1,'nguoiban','123',null,null,null,null,null,null,null,null),
(1,'nguoimua','123',null,null,null,null,null,null,null,null)


go
create table DanhGia
(
	MaDG int identity primary key not null, 
	MaTK_Mua int not null,
	MaTK_Ban int not null,
	Diem int,
	NhanXet nvarchar(300)
			
	foreign key (MaTK_Mua) references dbo.TaiKhoan(MaTK),
	foreign key (MaTK_Ban) references dbo.TaiKhoan(MaTK)
)


go
create table GoiTin
(
	MaGoi int identity primary key not null,
	TenGoi nvarchar(50),
	SoLuongTin int,
	Gia int
)

go
insert into GoiTin (TenGoi,SoLuongTin,Gia)
values 
(N'Gói 1',10,100000),
(N'Gói 2',30,200000),
(N'Gói 3',60,300000)

go
create table Tin
(
	MaTin int identity primary key not null,
	MaTK int not null,
	MaSKU nvarchar(50),
	TenSP nvarchar(50),
	MoTa nvarchar(max),
	SoLuong int,
	Gia int,
	NgayDang date,
	NgayKetThuc date,
	TinhTrang int
	
	foreign key (MaTK) references dbo.TaiKhoan(MaTK)
)

go
create table HoaDon_BanTin
(
	MaHDT int identity primary key not null,
	MaTK int not null,
	MaGoi int not null,
	TinKhuyenMai int,
	Ngay date
			
	foreign key (MaTK) references dbo.TaiKhoan(MaTK),
	foreign key (MaGoi) references dbo.GoiTin(MaGoi)
)

go
create table HoaDon_BanHang
(
	MaHDH int identity primary key not null,
	MaTK int not null,
	DuongSo nvarchar(30),
	QuanHuyen nvarchar(30),
	TinhThanh nvarchar (30),
	SoLuong int,
	Ngay date,	
	TongTien int,
	TrangThai int
	
	foreign key (MaTK) references dbo.TaiKhoan(MaTK)	
)

go

create table CT_HoaDon_BanHang
(
	MaHDH int not null,
	MaTin int not null,
	Gia int,
	SoLuong int
	
	primary key (MaHDH, MaTin),
	foreign key (MaHDH) references dbo.HoaDon_BanHang(MaHDH),
	foreign key (MaTin) references dbo.Tin(MaTin)
)

go
create table GioHang
(
	MaTK_Mua int not null,
	MaTin int not null,
	MaTK_Ban int not null,
	Gia int,
	SoLuong int,
	
	primary key (MaTK_Mua, MaTin),
	foreign key (MaTK_Mua) references dbo.TaiKhoan(MaTK),
	foreign key (MaTK_Ban) references dbo.TaiKhoan(MaTK)
)