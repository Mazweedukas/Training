SELECT c.id, c.name
FROM city c
WHERE NOT EXISTS (
    SELECT 1
    FROM street s
    WHERE s.city_id = c.id
)
ORDER BY c.name ASC;