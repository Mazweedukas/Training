SELECT c.name AS city, SUM(od.price_with_discount * od.product_amount) AS income
FROM order_details od
INNER JOIN customer_order co ON od.customer_order_id = co.id
INNER JOIN supermarket s ON co.supermarket_id = s.id
INNER JOIN street st ON s.street_id = st.id
INNER JOIN city c ON st.city_id = c.id
WHERE co.operation_time BETWEEN '2020-11-03' AND '2020-11-30'
GROUP BY c.name
ORDER BY city ASC, income ASC;