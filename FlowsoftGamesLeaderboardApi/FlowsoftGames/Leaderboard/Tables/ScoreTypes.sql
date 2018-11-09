CREATE TABLE [Leaderboard].[ScoreTypes]
(
	[ScoreTypeId]		SMALLINT	NOT NULL,
	[ScoreTypeName]		VARCHAR(32) NOT NULL,

	CONSTRAINT [PK_Leaderboard_ScoreTypes] PRIMARY KEY CLUSTERED ([ScoreTypeId])
)