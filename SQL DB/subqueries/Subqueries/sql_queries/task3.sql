SELECT co.id AS order_id, COUNT(od.id) AS items_count
FROM order_details od
JOIN customer_order co ON co.id = od.customer_order_id
WHERE co.operation_time BETWEEN '2021-01-01' AND '2021-12-31'
GROUP BY co.id
HAVING COUNT(od.id) > (
    SELECT AVG(items_per_order)
    FROM (
        SELECT COUNT(od2.id) AS items_per_order
        FROM order_details od2
        JOIN customer_order co2 ON co2.id = od2.customer_order_id
        GROUP BY co2.id
    ) sub
)
ORDER BY items_count ASC, co.id ASC;