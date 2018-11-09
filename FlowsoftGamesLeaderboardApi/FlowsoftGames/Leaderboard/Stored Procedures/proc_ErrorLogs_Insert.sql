CREATE PROCEDURE [Leaderboard].[proc_ErrorLogs_Insert]
(
	@GameName		VARCHAR(32),
	@MethodName		VARCHAR(32),
	@Message		VARCHAR(MAX)
)

AS

	BEGIN

		INSERT INTO Leaderboard.ErrorLogs
		(
			GameName,
			MethodName,
			[Message],
			InsertDate
		)
		VALUES
		(
			@GameName,
			@MethodName,
			@Message,
			SYSDATETIMEOFFSET()
		)

	END

GO

GRANT EXECUTE ON [Leaderboard].[proc_ErrorLogs_Insert] TO [LeaderboardRole];

GO