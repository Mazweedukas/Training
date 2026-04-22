SELECT t.title, p.price
FROM product p
JOIN product_title t ON p.product_title_id = t.id
JOIN order_details d ON p.id = d.product_id
JOIN customer_order o ON d.customer_order_id = o.id
JOIN customer c ON o.customer_id = c.person_id
JOIN person psn ON c.person_id = psn.id
WHERE psn.birth_date BETWEEN '2001-01-01' AND '2010-12-31'
GROUP BY t.title, p.price
ORDER BY  t.title ASC,p.price ASC;