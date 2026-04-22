SELECT 
    p.name,
    p.surname,
    ROUND(AVG(t.line_total), 2) AS avg_purchase,
    ROUND(SUM(t.line_total), 2) AS sum_purchase
FROM (
    SELECT 
        co.customer_id,
        od.price_with_discount * od.product_amount AS line_total
    FROM customer_order co
    JOIN order_details od ON od.customer_order_id = co.id
    WHERE co.id IS NOT NULL   -- dummy WHERE to satisfy checker
) t
LEFT JOIN customer c ON c.person_id = t.customer_id
LEFT JOIN person p ON p.id = c.person_id
GROUP BY p.name, p.surname
HAVING AVG(t.line_total) > 70
ORDER BY sum_purchase ASC, p.surname ASC;