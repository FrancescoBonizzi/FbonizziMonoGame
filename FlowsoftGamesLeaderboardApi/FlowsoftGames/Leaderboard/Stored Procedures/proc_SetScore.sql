CREATE PROCEDURE [Leaderboard].[proc_SetScore]
(
	@UserId			VARCHAR(32),
	@GameId			TINYINT,
	@ScoreTypeId	SMALLINT,
	@Score			BIGINT
)

AS

	BEGIN

		UPDATE Leaderboard.Leaderboard
			SET Score = @Score, LastUpdateDate = SYSDATETIMEOFFSET()

		WHERE 
			UserId			= @UserId
			AND GameId		= @GameId
			AND ScoreTypeId = @ScoreTypeId

	END

GO

GRANT EXECUTE ON [Leaderboard].[proc_SetScore] TO [LeaderboardRole];

GO