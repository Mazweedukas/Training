SELECT pc.name AS category, pt.title AS product
FROM product_category pc
INNER JOIN product_title pt ON pc.id = pt.product_category_id
JOIN product p ON pt.id = p.product_title_id
LEFT JOIN order_details od ON p.id = od.product_id
WHERE od.product_id IS NULL
ORDER BY p.id ASC;