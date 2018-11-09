namespace FlowsoftGamesLeaderboardApi.Requests
{
    public class GetGlobalRankRequest
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string GameName { get; set; }
        public string ScoreType { get; set; }
        public long CurrentLocalScore { get; set; }
    }
}
