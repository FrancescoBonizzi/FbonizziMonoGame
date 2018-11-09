using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApiSDK
{
    public class HttpLeaderboard : ILeaderboard
    {
        private readonly string _endpoint;
        private readonly string _gameName;
        private static readonly HttpClient _httpClient = new HttpClient();

        public HttpLeaderboard(string endpoint, string gameName)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));
        
            if (string.IsNullOrWhiteSpace(gameName))
                throw new ArgumentNullException(nameof(gameName));
        
            _endpoint = endpoint;
            _gameName = gameName;
            _httpClient.Timeout = TimeSpan.FromSeconds(3);
        }

        private async Task<string> GetAuthToken(string userId)
        {
            using (var getAuthTokenBody = new StringContent($@"
                {{
                    userId: ""{userId}""
                }}"))
            {
                getAuthTokenBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var tokenReponse = await _httpClient.PostAsync($"{_endpoint}/Leaderboard/GetAuthToken", getAuthTokenBody).ConfigureAwait(false))
                {
                    tokenReponse.EnsureSuccessStatusCode();
                    return await tokenReponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }

        public async Task<int> GetGlobalRank(string userId, string scoreType, long currentLocalScore)
        {
            var token = await GetAuthToken(userId).ConfigureAwait(false);

            var getGlobalRankUnmasked = $@"
                {{
                    token: ""{token}"",
                    userId: ""{userId}"",
                    gameName: ""{_gameName}"",
                    scoreType: ""{scoreType}"",
                    currentLocalScore: ""{currentLocalScore}""
                }}";

            var getGlobalRankMasked = getGlobalRankUnmasked.Crypt(token.AsIV());
            using (var getGlobalRankBody = new StringContent($@"
                {{
                    token: ""{token}"",
                    userId: ""{userId}"",
                    data: ""{getGlobalRankMasked}""
                }}"))
            {
                getGlobalRankBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var getGlobalRankResponse = await _httpClient.PostAsync($"{_endpoint}/LeaderBoard/GetGlobalRank", getGlobalRankBody).ConfigureAwait(false))
                {
                    getGlobalRankResponse.EnsureSuccessStatusCode();
                    var getGlobalRankResponseString = await getGlobalRankResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (string.IsNullOrEmpty(getGlobalRankResponseString))
                        throw new ApplicationException("GetGlobalRank didn't created the new user!");

                    return Convert.ToInt32(getGlobalRankResponseString);
                }
            }
        }
    }
}
