using FlowsoftGamesLeaderboardApiSDK;
using Newtonsoft.Json;

namespace FlowsoftGamesLeaderboardApi.Requests
{
    public class MaskedRequest<TUnmaskedRequest>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }

        public TUnmaskedRequest GetCoreRequest()
        {
            var iv = Token.AsIV();
            var jsonVersion = Data.Decrypt(iv);
            return JsonConvert.DeserializeObject<TUnmaskedRequest>(jsonVersion);
        }
    }
}
