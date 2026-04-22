SELECT c.name AS category, COUNT(p.id) AS count
FROM product_category c
INNER JOIN product_title t ON c.id = t.product_category_id
INNER JOIN product p ON p.product_title_id = t.id
GROUP BY c.name
ORDER BY c.name ASC;