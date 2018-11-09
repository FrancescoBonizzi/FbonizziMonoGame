CREATE PROCEDURE [Leaderboard].[proc_CreateScore]
(
	@UserId			VARCHAR(32),
	@GameId			TINYINT,
	@ScoreTypeId	SMALLINT,
	@Score			BIGINT
)

AS

	BEGIN

		INSERT INTO Leaderboard.Leaderboard
		(
			UserId,
			GameId,
			ScoreTypeId,
			Score,
			LastUpdateDate
		)
		VALUES
		(
			@UserId,
			@GameId,
			@ScoreTypeId,
			@Score,
			SYSDATETIMEOFFSET()
		)
			
	END

GO

GRANT EXECUTE ON [Leaderboard].[proc_CreateScore] TO [LeaderboardRole];

GO