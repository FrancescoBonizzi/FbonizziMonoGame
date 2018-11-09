using FlowsoftGamesLeaderboardApi.Exceptions;

namespace FlowsoftGamesLeaderboardApi.Requests
{
    public static class Validator
    {
        public static void Validate(GetGlobalRankRequest request)
        {
            if (request == null)
                throw new InvalidRequestException($"request is null");

            if (string.IsNullOrWhiteSpace(request.Token))
                throw new InvalidRequestException($"Token in the request is null");

            if (string.IsNullOrWhiteSpace(request.UserId))
                throw new InvalidRequestException($"UserId in the request is null");

            if (string.IsNullOrWhiteSpace(request.GameName))
                throw new InvalidRequestException($"GameName in the request is null");

            if (string.IsNullOrWhiteSpace(request.ScoreType))
                throw new InvalidRequestException($"ScoreType in the request is null");
        }

        public static void Validate(GetAuthTokenRequest request)
        {
            if (request == null)
                throw new InvalidRequestException($"request is null");

            if (string.IsNullOrWhiteSpace(request.UserId))
                throw new InvalidRequestException($"UserId in the request is null");
        }

        public static void Validate<T>(MaskedRequest<T> request)
        {
            if (request == null)
                throw new InvalidRequestException($"request is null");

            if (string.IsNullOrWhiteSpace(request.Data))
                throw new InvalidRequestException($"Data in the request is null");
        }
    }
}
