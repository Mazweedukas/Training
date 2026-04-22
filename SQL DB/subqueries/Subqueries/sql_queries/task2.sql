SELECT 
    p.surname,
    p.name,
    SUM(od.price_with_discount * od.product_amount) AS total_expenses
FROM person p
JOIN customer c ON c.person_id = p.id
JOIN customer_order co ON co.customer_id = c.person_id
JOIN order_details od ON od.customer_order_id = co.id
WHERE p.birth_date BETWEEN '2000-01-01' AND '2010-12-31'
GROUP BY p.surname, p.name
HAVING SUM(od.price_with_discount * od.product_amount) > (
    SELECT AVG(total)
    FROM (
        SELECT SUM(od2.price * od2.product_amount) AS total
        FROM customer_order co2
        JOIN order_details od2 ON co2.id = od2.customer_order_id
        GROUP BY co2.customer_id
    ) sub
)
ORDER BY total_expenses ASC, p.surname ASC;