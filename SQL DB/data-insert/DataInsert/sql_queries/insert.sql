INSERT INTO city (id, name) VALUES 
(1, 'Vilnius'),
(2, 'Kaunas'),
(3, 'Palanga');

INSERT INTO street (id, name, city_id) VALUES
(1, 'Gaga', 1),
(2, 'Lady', 1),
(3, 'Romance', 1),
(4, 'Paparazzi', 2),
(5, 'Poker Face', 3);

INSERT INTO supermarket (id, name, street_id, house_number) VALUES
(1, 'Just Dance', 1, '1'),
(2, 'Bad Romance', 2, '1'),
(3, 'Judas', 3, '1'),
(4, 'Abracadabra', 4, '2'),
(5, 'Shallow', 5, '3'),
(6, 'Telephone', 2, '1'),
(7, 'Die with a smile', 2, '1');

INSERT INTO person (id, name, surname, birth_date) VALUES
(1, 'John', 'Doe', '1990-01-01'),
(2, 'Jane', 'Doe', '1992-02-02'),
(3, 'Bob', 'Smith', '1985-03-03'),
(4, 'Alice', 'Brown', '1995-04-04'),
(5, 'Tom', 'White', '1988-05-05'),
(6, 'Emma', 'Black', '1993-06-06'),
(7, 'Liam', 'Green', '1991-07-07'),
(8, 'Olivia', 'Gray', '1994-08-08'),
(9, 'Noah', 'Blue', '1987-09-09'),
(10, 'Ava', 'Red', '1996-10-10');

INSERT INTO contact_type (id, name) VALUES
(1, 'phone'),
(2, 'email');

INSERT INTO person_contact (id, person_id, contact_type_id, contact_value) VALUES
(1,1,1,'123'),(2,1,2,'a@mail'),
(3,2,1,'234'),(4,2,2,'b@mail'),
(5,3,1,'345'),(6,3,2,'c@mail'),
(7,4,1,'456'),(8,4,2,'d@mail'),
(9,5,1,'567'),(10,5,2,'e@mail'),
(11,6,1,'678'),(12,6,2,'f@mail'),
(13,7,1,'789'),(14,7,2,'g@mail'),
(15,8,1,'890'),(16,8,2,'h@mail'),
(17,9,1,'901'),(18,9,2,'i@mail'),
(19,10,1,'012'),(20,10,2,'j@mail');

INSERT INTO customer (person_id, card_number, discount) VALUES
(1, 'C1', 5),(2, 'C2', 10),(3, 'C3', 0),
(4, 'C4', 15),(5, 'C5', 5),(6, 'C6', 0),
(7, 'C7', 20),(8, 'C8', 10),(9, 'C9', 5),(10, 'C10', 0);

INSERT INTO product_category (id, name) VALUES
(1, 'Food'),
(2, 'Drinks'),
(3, 'Electronics');

INSERT INTO product_title (id, title, product_category_id) VALUES
(1,'Bread',1),(2,'Milk',1),(3,'Cheese',1),
(4,'Water',2),(5,'Juice',2),(6,'Soda',2),
(7,'TV',3),(8,'Phone',3),(9,'Laptop',3),
(10,'Headphones',3);

INSERT INTO manufacturer (id, name) VALUES
(1,'Nestle'),
(2,'CocaCola'),
(3,'Samsung'),
(4,'Sony');

INSERT INTO product (id, product_title_id, manufacturer_id, price, comment) VALUES
(1,1,1,1.2,'a'),(2,2,1,1.5,'a'),(3,3,1,2.5,'a'),
(4,4,2,0.8,'a'),(5,5,2,1.0,'a'),(6,6,2,1.1,'a'),
(7,7,3,300,'a'),(8,8,3,500,'a'),(9,9,3,800,'a'),
(10,10,4,100,'a'),
(11,1,1,1.3,'b'),(12,2,1,1.6,'b'),(13,3,1,2.6,'b'),
(14,4,2,0.9,'b'),(15,5,2,1.1,'b'),(16,6,2,1.2,'b'),
(17,7,3,310,'b'),(18,8,3,510,'b'),(19,9,3,810,'b'),
(20,10,4,110,'b');

INSERT INTO customer_order (id, operation_time, supermarket_id, customer_id) VALUES
(1,'2024-01-01',1,1),(2,'2024-01-02',2,2),
(3,'2024-01-03',3,3),(4,'2024-01-04',4,4),
(5,'2024-01-05',5,5),(6,'2024-01-06',6,6),
(7,'2024-01-07',7,7),(8,'2024-01-08',1,8),
(9,'2024-01-09',2,9),(10,'2024-01-10',3,10),
(11,'2024-01-11',4,1),(12,'2024-01-12',5,2),
(13,'2024-01-13',6,3),(14,'2024-01-14',7,4),
(15,'2024-01-15',1,5),(16,'2024-01-16',2,6),
(17,'2024-01-17',3,7),(18,'2024-01-18',4,8),
(19,'2024-01-19',5,9),(20,'2024-01-20',6,10);

INSERT INTO order_details (id, customer_order_id, product_id, price, price_with_discount, product_amount) VALUES
(1,1,1,1.2,1.1,2),(2,2,2,1.5,1.3,1),
(3,3,3,2.5,2.3,1),(4,4,4,0.8,0.7,3),
(5,5,5,1.0,0.9,2),(6,6,6,1.1,1.0,1),
(7,7,7,300,280,1),(8,8,8,500,450,1),
(9,9,9,800,750,1),(10,10,10,100,90,1),
(11,11,11,1.3,1.2,2),(12,12,12,1.6,1.5,1),
(13,13,13,2.6,2.4,1),(14,14,14,0.9,0.8,3),
(15,15,15,1.1,1.0,2),(16,16,16,1.2,1.1,1),
(17,17,17,310,290,1),(18,18,18,510,480,1),
(19,19,19,810,770,1),(20,20,20,110,100,1);