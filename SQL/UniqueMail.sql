ALTER   trigger [dbo].[UniqueMail] on [dbo].[Utilisateur]
after insert
as
if exists(
	select *
	from inserted as i
	where exists(
		select *
		from Utilisateur as u
		where u.IdUtilisateur <> i.IdUtilisateur
		and u.mail = i.mail))
begin
raiserror('Deux comptes ne peuvent pas avoir le même identifiant', 16, 1);
rollback transaction;
return
end;
