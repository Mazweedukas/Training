SELECT 
    p.surname, 
    p.name, 
    COALESCE(SUM(od.price_with_discount * od.product_amount), 0) AS sum
FROM person p
JOIN customer c ON c.person_id = p.id
LEFT JOIN customer_order co ON c.person_id = co.customer_id
LEFT JOIN order_details od ON co.id = od.customer_order_id
GROUP BY p.surname, p.name

UNION

SELECT 
    NULL AS surname, 
    NULL AS name, 
    COALESCE(SUM(od.price_with_discount * od.product_amount), 0) AS sum
FROM customer_order co
LEFT JOIN order_details od ON co.id = od.customer_order_id
WHERE co.customer_id IS NULL

ORDER BY sum ASC, surname ASC;