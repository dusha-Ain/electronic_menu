INSERT INTO Roles (roleName) VALUES
('Admin'),
('Unauthorized'),
('UserAgent');



INSERT INTO CategoriesDishes (categoryName) VALUES
('Супы'),
('Салаты'),
('Горячие блюда'),
('Десерты'),
('Напитки'),
('Хлеб');



INSERT INTO Users (PasswordHash, FirstName, SecondName, LastName, RegistrationDate, FKRole) VALUES
('hash1', 'Иван', 'Иванович', 'Иванов', '2024-01-10 10:00:00', 1),
('hash2', 'Мария', 'Петровна', 'Сидорова', '2024-05-15 12:30:00', 2),
('hash3', 'Алексей', 'Викторович', 'Кузнецов', '2025-02-20 09:45:00', 3),
('hash4', 'Елена', 'Владимировна', 'Смирнова', '2023-11-20 14:15:00', 2),
('hash5', 'Дмитрий', 'Александрович', 'Козлов', '2025-01-05 08:30:00', 3),
('hash6', 'Анна', 'Сергеевна', 'Новикова', '2024-06-17 16:45:00', 2),
('hash7', 'Сергей', 'Петрович', 'Лебедев', '2024-09-09 13:00:00', 3),
('hash8', 'Ольга', 'Николаевна', 'Зайцева', '2025-02-25 11:10:00', 2);




INSERT INTO Dishes (DishName, Description, FKCategory) VALUES
('Борщ', 'Традиционный русский суп', 1),
('Цезарь', 'Салат с курицей и крутонами', 2),
('Котлета по-киевски', 'Обжаренная куриная котлета с маслом', 3),
('Тирамису', 'Итальянский десерт с маскарпоне', 4),
('Вода', '0.5 литров', 5);



INSERT INTO Statuses (StatusName) VALUES
('Создан'),
('Готовится'),
('Выполнен'),
('Отменен');




INSERT INTO Orders (OrderDate, FKStatus, TotalAmount, FKUser) VALUES
('2025-09-10 18:00:00', 1, 1500, 2),
('2025-09-11 13:45:00', 2, 2500, 3);




INSERT INTO OrderItem (Quantity, UnitPrice, FKDish, FKOrder) VALUES
(2, 300, 1, 1),
(1, 1200, 2, 1),
(3, 500, 3, 2);

