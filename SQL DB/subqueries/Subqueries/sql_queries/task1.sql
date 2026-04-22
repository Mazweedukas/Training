SELECT p.id, pt.title AS product, p.price
FROM product p
INNER JOIN product_title pt ON pt.id = p.product_title_id
WHERE price >= (
	SELECT MIN(price) 
	FROM product
	LIMIT 1) * 2
ORDER BY p.price ASC, pt.title ASC