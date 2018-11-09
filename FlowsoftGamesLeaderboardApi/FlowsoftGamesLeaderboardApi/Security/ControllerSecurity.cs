using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using FlowsoftGamesLeaderboardApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Security
{
    public class ControllerSecurity
    {
        private readonly ICache _cache;
        private readonly UnauthorizedResult _unauthorized;
        private readonly HttpContext _httpContext;
        private readonly TimeSpan _tokenExpiration = TimeSpan.FromSeconds(4);
        private readonly TimeSpan _tokenRequestExpiration = TimeSpan.FromSeconds(1);
        private readonly TimeSpan _ipAddressBanTime = TimeSpan.FromMinutes(10);

        public ControllerSecurity(
            ICache cache, 
            UnauthorizedResult unauthorized,
            HttpContext httpContext)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _unauthorized = unauthorized ?? throw new ArgumentNullException(nameof(unauthorized));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public async Task CheckIpBan()
        {
            var remoteIpAddress = _httpContext.Connection.RemoteIpAddress.ToString();
            var isIpAddressPresentInBlackListCache = (await _cache.Get(GetBannedIpCacheKey(remoteIpAddress))) != null;
            if (isIpAddressPresentInBlackListCache)
                throw new InvalidLoginException($"Ip adress is banned: {remoteIpAddress}");
        }

        public async Task CheckToken(string token, string userId)
        {
            var tokenCacheKey = GetTokenCacheKey(userId);
            var cacheToken = await _cache.Get(tokenCacheKey);

            // Se il token è scaduto, esci
            if (cacheToken == null)
                throw new InvalidLoginException($"Token expired or not requested");

            // Consumo il token, non deve MAI più essere riutilizzabile
            await _cache.Remove(tokenCacheKey);

            // Impedisco che nei prossimi _tokenRequestExpiration secondi ne venga richiesto un altro dallo stesso utente
            await _cache.Set(GetUserLastTokenRequestCacheKey(userId), string.Empty, _tokenRequestExpiration);

            // Se il token è diverso da quello generato in auth, esci
            if (string.Compare(cacheToken, token, false) != 0)
                throw new InvalidLoginException($"Wrong token for this user");
        }

        public async Task<UnauthorizedResult> UnauthorizeAndBlackList()
        {
            await BlackListThisRequestIp();
            return _unauthorized;
        }

        private string GetTokenCacheKey(string userId)
            => $"{userId}-Token";

        private string GetUserLastTokenRequestCacheKey(string userId)
            => $"{userId}-RequestedToken";

        private string GetBannedIpCacheKey(string ip)
            => $"BannedId-{ip}";

        public async Task<string> BuildToken(string userId)
        {
            var userLastTokenRequestCacheKey = GetUserLastTokenRequestCacheKey(userId);

            // Se negli ultimi _tokenRequestExpiration è già stato richiesto il token, esco
            var userLastTokenRequestEntry = await _cache.Get(userLastTokenRequestCacheKey);
            if (userLastTokenRequestEntry != null)
                throw new InvalidRequestException($"Too many token requests");

            // Genero il token
            var token = Guid.NewGuid().ToString("N");

            // Metto il token in cache per considerarlo valido
            await _cache.Set(GetTokenCacheKey(userId), token, _tokenExpiration);

            // Impedisco che nei prossimi _tokenRequestExpiration secondi ne venga richiesto un altro dallo stesso utente
            await _cache.Set(userLastTokenRequestCacheKey, string.Empty, _tokenRequestExpiration);

            return token;
        }

        private Task BlackListThisRequestIp()
        {
            // Refresho la cache di ban per questo ip, dato che ci ha riprovato
            var ipToBan = _httpContext.Connection.RemoteIpAddress.ToString();
            return _cache.Set(GetBannedIpCacheKey(ipToBan), string.Empty, _ipAddressBanTime);
        }
    }
}
