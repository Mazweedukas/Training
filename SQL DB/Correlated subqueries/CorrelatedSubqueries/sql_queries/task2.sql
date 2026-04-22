SELECT pc.id, pc.name
FROM product_category pc
WHERE EXISTS (
    SELECT 1
    FROM product_title pt
    JOIN product p ON p.product_title_id = pt.id
    WHERE pt.product_category_id = pc.id
)
AND NOT EXISTS (
    SELECT 1
    FROM product_title pt
    JOIN product p ON p.product_title_id = pt.id
    WHERE pt.product_category_id = pc.id
      AND NOT EXISTS (
          SELECT 1
          FROM order_details od
          WHERE od.product_id = p.id
      )
)
ORDER BY pc.id ASC;