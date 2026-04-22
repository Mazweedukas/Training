SELECT p.surname, p.name, p.birth_date, SUM(od.price_with_discount * od.product_amount) AS sum
FROM order_details od
INNER JOIN customer_order co ON od.customer_order_id = co.id
INNER JOIN customer c ON c.person_id = co.customer_id
INNER JOIN person p ON c.person_id = p.id
WHERE c.discount = NULL OR c.discount = 0 AND co.operation_time BETWEEN '2021-01-01' AND '2021-12-31'
GROUP BY p.surname
ORDER BY p.surname ASC, sum ASC;