CREATE PROCEDURE [Leaderboard].[proc_GetScore]
(
	@UserId			VARCHAR(32),
	@GameId			TINYINT,
	@ScoreTypeId	SMALLINT
)

AS

	BEGIN

		SELECT 
			Score

		FROM 
			Leaderboard.Leaderboard
			
		WHERE 
			UserId			= @UserId
			AND GameId		= @GameId
			AND ScoreTypeId = @ScoreTypeId
			
	END

GO

GRANT EXECUTE ON [Leaderboard].[proc_GetScore] TO [LeaderboardRole];

GO