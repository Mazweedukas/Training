SELECT p.surname, p.name, p.birth_date
FROM person p
INNER JOIN customer c ON c.person_id = p.id
LEFT JOIN customer_order co ON  c.person_id = co.customer_id
WHERE co.customer_id IS NULL
ORDER BY p.surname ASC, p.birth_date ASC;