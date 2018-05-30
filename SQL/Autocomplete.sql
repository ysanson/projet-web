ALTER   trigger [dbo].[CompleteEspRace] on [dbo].[Espece]
for insert, update
as
BEGIN
	update dbo.Espece
	set espece.esprace = CONCAT(inserted.espece, ', ', inserted.race)
	from inserted
	where espece.idEspece = inserted.idEspece
END