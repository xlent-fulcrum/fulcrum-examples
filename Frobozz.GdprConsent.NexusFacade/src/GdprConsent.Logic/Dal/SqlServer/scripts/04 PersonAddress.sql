/****** Script for SelectTopNRows command from SSMS  ******/
SELECT p.Name Person, a.Type AddressType, a.Street, a.City
FROM Person p 
LEFT JOIN Address a ON (a.PersonId = p.Id)
ORDER BY p.Name, a.Type