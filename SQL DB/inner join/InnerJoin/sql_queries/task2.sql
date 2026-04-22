SELECT p.id, t.title, c.name AS category, p.price
FROM product p
INNER JOIN product_title t ON p.product_title_id = t.id
INNER JOIN product_category c ON t.product_category_id = c.id
ORDER BY category, t.title ASC;