CREATE PROCEDURE [Leaderboard].[proc_Manutenzione]
	
AS
	
	BEGIN

		-- Butta via tutti gli utenti con score a 0 da più di una settimana
		DELETE FROM 
			Leaderboard.Leaderboard 

		WHERE 
			Score = 0 
			AND LastUpdateDate > DATEADD(WEEK, -1, SYSDATETIMEOFFSET())


		-- Butta via tutti i log d'errore più vecchi di una settimana
		DELETE FROM
			Leaderboard.ErrorLogs

		WHERE
			InsertDate > DATEADD(WEEK, -1, SYSDATETIMEOFFSET())

		-- Tiene solo i primi 1000 record per GameId e ScoreTypeId
		-- TODO

	END