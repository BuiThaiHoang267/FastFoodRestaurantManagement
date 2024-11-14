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

INSERT INTO Products (Id, Type, Name, Price, Image, CategoryId)
VALUES
(1, 'Product', N'DEAL CỰC ĐÃ - ĂN THẢ GA 79K', 79000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/c/o/combo_79k-15.jpg', 1),
(2, 'Product', N'COMBO MỘT MÌNH ĂN NGON', 78000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_1.png', 1),
(3, 'Product', N'CẶP ĐÔI ĂN Ý', 145000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_2_2__1.png', 1),
(4, 'Product', N'COMBO CẢ NHÀ NO NÊ', 185000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_3.png', 1),
(5, 'Product', N'COMBO BẠN BÈ TỤ TẬP', 322000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_4_2.png', 1),
(6, 'Product', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 1', 499000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_5.png', 1),
(7, 'Product', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 2', 599000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_6.png', 1),
(8, 'Product', N'TIỆC KIỂU MỚI, QUÀ CHUẨN GU 3', 699000, 'https://jollibee.com.vn/media/catalog/product/cache/9011257231b13517d19d9bae81fd87cc/m/_/m_n_ngon_ph_i_th_-_7.png', 1);