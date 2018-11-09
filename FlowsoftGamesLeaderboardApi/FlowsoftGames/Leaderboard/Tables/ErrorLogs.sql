CREATE TABLE [Leaderboard].[ErrorLogs]
(
	GameName		VARCHAR(32)		NOT NULL,
	MethodName		VARCHAR(32)		NOT NULL,
	[Message]		VARCHAR(MAX)	NOT NULL,
	InsertDate		DATETIMEOFFSET	NOT NULL
);

GO

CREATE CLUSTERED INDEX [IX_Ledaerboard_ErrorLogs_InsertDate] ON [Leaderboard].[ErrorLogs](InsertDate ASC);

GO
