CREATE TABLE [Leaderboard].[Leaderboard]
(
	EntryId			INT				IDENTITY(1, 1),
	UserId			VARCHAR(32)		NOT NULL,
	GameId			TINYINT			NOT NULL,
	ScoreTypeId		SMALLINT		NOT NULL,
	Score			BIGINT			NOT NULL,
	LastUpdateDate	DATETIMEOFFSET	NOT NULL,

	CONSTRAINT [PK_Leaderboard_Leaderboard] PRIMARY KEY CLUSTERED ([EntryId], [Score]), -- Non sono sicuro!
	CONSTRAINT [FK_Leaderboard_Leaderboard_GameId]		FOREIGN KEY ([GameId])		REFERENCES [Leaderboard].[Games] ([GameId]),
	CONSTRAINT [FK_Leaderboard_Leaderboard_ScoreTypeId]	FOREIGN KEY ([ScoreTypeId]) REFERENCES [Leaderboard].[ScoreTypes] ([ScoreTypeId]),
	CONSTRAINT [UK_Leaderboard_Leaderboard_UserId] UNIQUE ([UserId])
);

GO

-- Lo utilizzano la proc_GetScore e la proc_SetScore
CREATE NONCLUSTERED INDEX [IX_Leaderboard_Leaderboard] 
	ON [Leaderboard].[Leaderboard](UserId, GameId, ScoreTypeId)
	INCLUDE([Score]);

GO