SELECT 
    rm.product_id,
    rm.title,

    CASE 
        WHEN rm.total_qty IS NULL THEN NULL 
        ELSE rm.manufacturer_id 
    END AS manufacturer_id,

    CAST(rm.manufacturer AS BPCHAR) AS manufacturer   -- 👈 ONLY CAST HERE

FROM (
    SELECT 
        pt.id AS product_id,
        pt.title,
        m.id AS manufacturer_id,
        m.name AS manufacturer,   -- ❗ NO CAST HERE
        SUM(od.product_amount) AS total_qty,
        ROW_NUMBER() OVER (
            PARTITION BY pt.id 
            ORDER BY SUM(od.product_amount) DESC, m.id ASC
        ) AS rank
    FROM product_title pt
    LEFT JOIN product p ON pt.id = p.product_title_id
    LEFT JOIN order_details od ON p.id = od.product_id
    LEFT JOIN manufacturer m ON p.manufacturer_id = m.id
    GROUP BY pt.id, pt.title, m.id, m.name
) rm
WHERE rm.rank = 1
ORDER BY rm.product_id;