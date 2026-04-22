 SELECT p.id, t.title, p.price
 FROM product p
 INNER JOIN product_title t ON p.product_title_id = t.id
 ORDER BY t.title ASC;