CREATE PROCEDURE [Leaderboard].[proc_GetGlobalRank]
(
	@UserId			VARCHAR(32),
	@GameId			TINYINT,
	@ScoreTypeId	SMALLINT
)

AS

	BEGIN

		;WITH CTE_Ranks_Of_This_GameId_And_ScoreTypeId as
		(
			SELECT 
				UserId, 
				[Rank] = RANK() OVER (ORDER BY Score DESC)
			
			FROM 
				[Leaderboard].[Leaderboard]

			WHERE 
				GameId			= @GameId
				AND ScoreTypeId = @ScoreTypeId
		)
		SELECT 
			[Rank] 
			
		FROM 
			CTE_Ranks_Of_This_GameId_And_ScoreTypeId 
			
		WHERE UserId = @UserId
		

	END

GO

GRANT EXECUTE ON [Leaderboard].[proc_GetGlobalRank] TO [LeaderboardRole];

GO