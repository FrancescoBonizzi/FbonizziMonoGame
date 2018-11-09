CREATE TABLE [Leaderboard].[Games]
(
	[GameId]	TINYINT		NOT NULL,
	[GameName]	VARCHAR(32) NOT NULL,

	CONSTRAINT [PK_Leaderboard_Games] PRIMARY KEY CLUSTERED ([GameId]),
	CONSTRAINT [UK_Leaderboard_Games_GameName] UNIQUE ([GameName])
);
