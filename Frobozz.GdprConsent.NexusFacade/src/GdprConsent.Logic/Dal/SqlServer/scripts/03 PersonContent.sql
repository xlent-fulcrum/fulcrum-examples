/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) p.Name Person, c.Name Consent, pc.HasGivenConsent 
FROM [LeverExampleGdpr].[dbo].[PersonConsent] pc
LEFT JOIN Person p ON (p.Id = pc.PersonId)
LEFT JOIN Consent c ON (c.Id = pc.ConsentId)
ORDER BY p.Name, c.Name