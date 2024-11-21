use fastfooddb;

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

-- insert product 
-- Category Id 2
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(1, 'Product', N'1 MIẾNG GÀ GIÒN VUI VẺ', 20000, 33000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_9.png', 2),
(2, 'Product', N'2 MIẾNG GÀ GIÒN VUI VẺ', 40000, 66000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_8_1.png', 2),
(3, 'Product', N'4 MIẾNG GÀ GIÒN VUI VẺ', 80000, 126000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_6_7.png', 2),
(4, 'Product', N'6 MIẾNG GÀ GIÒN VUI VẺ', 120000, 188000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_6_7_1.png', 2),
(5, 'Product', N'1 CƠM GÀ GIÒN VUI VẺ', 30000, 48000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_5.png', 2);

-- Category Id 3
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(6, 'Product', N'MÌ Ý JOLLY VỪA', 20000, 35000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_6_7-compressed.jpg', 3),
(7, 'Product', N'MÌ Ý JOLLY LỚN', 30000, 45000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_6_7-compressed_1.jpg', 3);

-- Category Id 4
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(8, 'Product', N'1 MIẾNG GÀ SỐT CAY', 25000, 35000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_7-compressed.jpg', 4),
(9, 'Product', N'2 MIẾNG GÀ SỐT CAY', 50000, 75000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_6-compressed.jpg', 4),
(10, 'Product', N'1 CƠM GÀ SỐT CAY', 30000, 50000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_5-compressed.jpg', 4);

-- Category Id 5
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(11, 'Product', N'JOLLY HOTDOG', 15000, 25000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_9.png', 5),
(12, 'Product', N'SANDWICH GÀ GIÒN', 20000, 30000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_8.png', 5),
(13, 'Product', N'BURGER TÔM', 28000, 40000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_7.png', 5),
(14, 'Product', N'CƠM GÀ MẮM TỎI', 25000, 35000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/5/4/54d1040569d8ce8697c9_1.jpg', 5);

-- Category Id 6
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(15, 'Product', N'CƠM TRẮNG', 5000, 10000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_6.png', 6),
(16, 'Product', N'SÚP BÍ ĐỎ', 10000, 15000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_5.png', 6),
(17, 'Product', N'KHOAI TÂY CHIÊN VỪA', 10000, 20000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_3_4-compressed.jpg', 6),
(18, 'Product', N'KHOAI TÂY CHIÊN LỚN', 15000, 25000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_3_4-compressed_1.jpg', 6),
(19, 'Product', N'KHOAI TÂY LẮC VỊ BBQ VỪA', 15000, 25000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_1_2-compressed_1.jpg', 6),
(20, 'Product', N'KHOAI TÂY LẮC VỊ BBQ LỚN', 20000, 35000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/p/h/ph_n_n_ph_-_1_2-compressed_1_1.jpg', 6);

-- Category Id 7
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(21, 'Product', N'KEM SỮA TƯƠI (CÚP)', 2000, 5000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_6.png', 7),
(22, 'Product', N'KEM SÔCÔLA (CÚP)', 4000, 7000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_5.png', 7),
(23, 'Product', N'KEM SUNDAE SOCOLA', 8000, 15000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_4.png', 7),
(24, 'Product', N'KEM SUNDAE DÂU', 8000, 15000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_3.png', 7),
(25, 'Product', N'TROPICAL SUNDAE', 10000, 20000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_2.png', 7),
(26, 'Product', N'BÁNH XOÀI ĐÀO', 5000, 10000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_tr_ng_mi_ng_-_1.png', 7);

-- Category Id 8
INSERT INTO Products (Id, Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
(27, 'Product', N'CACAO SỮA ĐÁ VỪA', 14000, 20000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_11_12.png', 8),
(28, 'Product', N'CACAO SỮA ĐÁ LỚN', 18000, 25000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_11_12_1.png', 8),
(29, 'Product', N'7UP VỪA', 8000, 15000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_9_10.png', 8),
(30, 'Product', N'7UP LỚN', 8000, 15000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_9_10_1.png', 8),
(31, 'Product', N'MIRINDA VỪA', 8000, 12000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_7_8.png', 8),
(32, 'Product', N'MIRINDA LỚN', 12000, 17000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_7_8_1.png', 8),
(33, 'Product', N'PEPSI VỪA', 8000, 12000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_5_6.png', 8),
(34, 'Product', N'PEPSI LỚN', 12000, 17000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_5_6_1.png', 8),
(35, 'Product', N'PEPSI KHÔNG CALO VỪA', 8000, 12000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_3_4.png', 8),
(36, 'Product', N'PEPSI KHÔNG CALO LỚN', 12000, 17000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_3_4_1.png', 8),
(37, 'Product', N'NƯỚC SUỐI', 4000, 8000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_1th_c_u_ng_-_2.png', 8),
(38, 'Product', N'NƯỚC ÉP XOÀI ĐÀO', 15000, 20000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/t/h/th_c_u_ng_-_1.png', 8);

-- insert combo 
-- Category 1
INSERT INTO Products (Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
( 'Combo', N'DEAL CỰC ĐÃ - ĂN THẢ GA 79K', 50000, 79000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/c/o/combo_79k-15.jpg', 1),
( 'Combo', N'COMBO MỘT MÌNH ĂN NGON', 50000, 78000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_1.png', 1),
( 'Combo', N'CẶP ĐÔI ĂN Ý', 100000, 145000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_2_2__1.png', 1),
( 'Combo', N'COMBO CẢ NHÀ NO NÊ', 120000, 185000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_3.png', 1),
( 'Combo', N'COMBO BẠN BÈ TỤ TẬP', 250000, 322000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_4_2.png', 1),
( 'Combo', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 1', 350000, 499000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_5.png', 1),
( 'Combo', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 2', 400000, 599000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_6.png', 1),
( 'Combo', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 3', 500000, 699000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_7.png', 1);

-- Category 2
INSERT INTO Products (Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
( 'Combo', N'2 GÀ GIÒN + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC', 60000, 91000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_1_1.png', 2),
( 'Combo', N'1 GÀ GIÒN + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC', 38000, 58000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_2.png', 2),
( 'Combo', N'1 CƠM GÀ GIÒN + 1 SÚP BÍ ĐỎ + 1 NƯỚC', 40000, 63000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_3.png', 2),
( 'Combo', N'1 CƠM GÀ GIÒN + 1 NƯỚC NGỌT', 40000, 58000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_gi_n_vui_v_-_4.png', 2);

-- Category 3
INSERT INTO Products (Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
( 'Combo', N'1 MÌ Ý JOLLY VỪA + 1 GÀ GIÒN + 1 KTC VỪA + 1 NƯỚC', 60000, 93000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_1-compressed.jpg', 3),
( 'Combo', N'1 MÌ Ý JOLLY VỪA + 2 GÀ KX + 1 KTC VỪA + 1 NƯỚC', 55000, 80000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_2-compressed.jpg', 3),
( 'Combo', N'1 MÌ Ý JOLLY VỪA + 2 GÀ KX + 1 NƯỚC', 50000, 70000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_3.png', 3),
( 'Combo', N'1 MÌ Ý JOLLY VỪA + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC', 45000, 60000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_4-compressed_1.jpg', 3),
( 'Combo', N'1 MÌ Ý JOLLY VỪA + 1 NƯỚC', 30000, 45000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_jolly_-_5-compressed_1.jpg', 3);

-- Category 4
INSERT INTO Products (Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
( 'Combo', N'2 GÀ SỐT CAY + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC NGỌT', 65000, 95000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_1-compressed.jpg', 4),
( 'Combo', N'1 GÀ SỐT CAY + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC NGỌT', 40000, 60000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_2-compressed.jpg', 4),
( 'Combo', N'1 CƠM GÀ SỐT CAY + 1 SÚP BÍ ĐỎ + 1 NƯỚC NGỌT', 45000, 65000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_3-compressed.jpg', 4),
( 'Combo', N'1 CƠM GÀ SỐT CAY + 1 NƯỚC NGỌT', 45000, 60000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/g/_/g_s_t_cay_-_4-compressed.jpg', 4);

-- Category 5
INSERT INTO Products (Type, Name, CostPrice, Price, Image, CategoryId)
VALUES
( 'Combo', N'1 CƠM GÀ MẮM TỎI + 1 PEPSI VỪA', 25000, 40000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/6/0/603ead39c9e46eba37f5.jpg', 5),
( 'Combo', N'1 BURGER TÔM + 1 KHOAI TÂY CHIÊN VỪA + 1 NƯỚC', 40000, 65000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_1.png', 5),
( 'Combo', N'1 BURGER TÔM + 1 NƯỚC NGỌT', 35000, 50000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_2.png', 5),
( 'Combo', N'1 JOLLY HOTDOG + 1 KTC VỪA + 1 NƯỚC', 35000, 50000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_3.png', 5),
( 'Combo', N'1 JOLLY HOTDOG + 1 NƯỚC NGỌT', 20000, 35000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_4.png', 5),
( 'Combo', N'1 SANDWICH GÀ GIÒN + 1 KTC VỪA + 1 NƯỚC', 40000, 55000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_5.png', 5),
( 'Combo', N'1 SANDWICH GÀ GIÒN + 1 NƯỚC NGỌT', 25000, 40000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/b/u/burger_-_6.png', 5);

-- ComboItem
INSERT INTO ComboItems (ComboId, ProductId, Quantity)
VALUES
()

