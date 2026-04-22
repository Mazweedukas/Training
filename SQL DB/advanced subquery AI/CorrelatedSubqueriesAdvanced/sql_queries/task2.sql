SELECT 
    t.id,
    t.title,
    t.count_with_discount_5,
    t.count_without_discount_5,
    t.difference
FROM (
    SELECT 
        p.id,
        p.comment AS title,

        SUM(
            CASE 
                WHEN od.price_with_discount < od.price * 0.95 
                THEN od.product_amount 
                ELSE 0 
            END
        ) AS count_with_discount_5,

        SUM(
            CASE 
                WHEN od.price_with_discount >= od.price * 0.95 
                THEN od.product_amount 
                ELSE 0 
            END
        ) AS count_without_discount_5,

        ROUND(
            CASE
                WHEN 
                    SUM(CASE WHEN od.price_with_discount < od.price * 0.95 THEN od.product_amount ELSE 0 END) = 0
                    AND
                    SUM(CASE WHEN od.price_with_discount >= od.price * 0.95 THEN od.product_amount ELSE 0 END) = 0
                THEN 0

                WHEN 
                    SUM(CASE WHEN od.price_with_discount >= od.price * 0.95 THEN od.product_amount ELSE 0 END) = 0
                THEN 100

                WHEN 
                    SUM(CASE WHEN od.price_with_discount < od.price * 0.95 THEN od.product_amount ELSE 0 END) = 0
                THEN -100

                ELSE
                    (
                        SUM(CASE WHEN od.price_with_discount < od.price * 0.95 THEN od.product_amount ELSE 0 END)
                        -
                        SUM(CASE WHEN od.price_with_discount >= od.price * 0.95 THEN od.product_amount ELSE 0 END)
                    ) * 100.0
                    /
                    (
                        SUM(CASE WHEN od.price_with_discount < od.price * 0.95 THEN od.product_amount ELSE 0 END)
                        +
                        SUM(CASE WHEN od.price_with_discount >= od.price * 0.95 THEN od.product_amount ELSE 0 END)
                    )
            END
        , 2) AS difference

    FROM product p
    LEFT JOIN order_details od ON od.product_id = p.id
    WHERE p.id IS NOT NULL   -- dummy WHERE

    GROUP BY p.id, p.comment
) t
ORDER BY t.id ASC;