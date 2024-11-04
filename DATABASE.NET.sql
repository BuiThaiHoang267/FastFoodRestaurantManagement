USE FastFoodManagementDb


--INSERT POSITIONS
GO
SET IDENTITY_INSERT Positions ON;
GO
INSERT INTO Positions(Id, Name, Description)
VALUES
(1, N'Admin', N'Admin hệ thống'),
(2, N'Quản lý', N'Quản lý của một chi nhánh'),
(3, N'Nhân viên thu ngân', N'Xử lý bán hàng tại quầy'),
(4, N'Nhân viên bếp', N'Xử lý các món ăn trong đơn hàng'),
(5, N'Nhân viên kho', N'Quản lý nhập kho các nguyên liệu trong kho');
--SELECT * FROM Positions
GO
SET IDENTITY_INSERT Positions OFF


--INSERT ROLES
GO
SET IDENTITY_INSERT Roles ON
GO
INSERT INTO Roles (Id, Code, Name, Description)
VALUES
(1, N'ADMIN', N'Admin', N'Toàn quyền trong hệ thống'),
(2, N'MANAGER', N'Quản lý', N'Quản lý toàn quyền trong một chi nhánh'),
(3, N'NV', N'Nhân viên', N'Quản lý và xử lí bán hàng và các đơn hàng'),
(4, N'NVK', N'Nhân viên kho', N'Quản lý kho hàng và nguyên liệu');
--SELECT * FROM Roles
GO
SET IDENTITY_INSERT Roles OFF


--INSERT BRANCHES
GO
SET IDENTITY_INSERT Branches ON
GO
INSERT INTO Branches (Id, Name, Location, Phone, Email, IsActive)
VALUES
(1, N'Chi nhánh 1', N'Hồ Chí Minh', '1234567890', 'chinhanh1@gmail.com', 1),
(2, N'Chi nhánh 2', N'Hà Nội', '1234567891', 'chinhanh2@gmail.com', 1);
--SELECT * FROM Branches
GO
SET IDENTITY_INSERT Branches OFF

-- INSERT CATEGORIES
GO
SET IDENTITY_INSERT Categories ON;
GO
INSERT INTO Categories(Id, Name, Image)
VALUES
(1, N'Món ngon phải thử', 'https://jollibee.com.vn/media/catalog/category/web-12_1_1.png'),
(2, N'Gà giòn vui vẻ', 'https://jollibee.com.vn/media/catalog/category/web-05_1.png'),
(3, N'Mì Ý Jolly', 'https://jollibee.com.vn/media/catalog/category/web-06.png'),
(4, N'Gà sốt cay', 'https://jollibee.com.vn/media/catalog/category/web-07.png'),
(5, N'Burger/Cơm', 'https://jollibee.com.vn/media/catalog/category/cat_burger_1.png'),
(6, N'Phần ăn phụ', 'https://jollibee.com.vn/media/catalog/category/phananphu.png'),
(7, N'Món tráng miệng', 'https://jollibee.com.vn/media/catalog/category/trangmieng.png'),
(8, N'Thức uống', 'https://jollibee.com.vn/media/catalog/category/thucuong.png');
--SELECT * FROM Categories
GO
SET IDENTITY_INSERT Categories OFF;


--INSERT PRODUCT
GO
SET IDENTITY_INSERT Products ON
GO
INSERT INTO Products (Id, Name, Price, Image, CategoryId)
VALUES
(1, N'DEAL CỰC ĐÃ - ĂN THẢ GA 79K', 79000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/c/o/combo_79k-15.jpg', 1),
(2, N'COMBO MỘT MÌNH ĂN NGON', 78000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_1.png', 1),
(3, N'CẶP ĐÔI ĂN Ý', 145000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_2_2__1.png', 1),
(4, N'COMBO CẢ NHÀ NO NÊ', 185000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_3.png', 1),
(5, N'COMBO BẠN BÈ TỤ TẬP', 322000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_4_2.png', 1),
(6, N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 1', 499000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_5.png', 1),
(7, N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 2', 599000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_6.png', 1),
(8, N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 3', 699000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_7.png', 1);
--SELECT * FROM Products
GO
SET IDENTITY_INSERT Products OFF
