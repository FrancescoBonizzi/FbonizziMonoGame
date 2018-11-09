using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using FlowsoftGamesLeaderboardApi.Domain;
using FlowsoftGamesLeaderboardApi.Exceptions;
using FlowsoftGamesLeaderboardApi.Requests;
using FlowsoftGamesLeaderboardApi.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FlowsoftGamesLeaderboardApi.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardRepository _leaderboardRepository;
        private readonly ICache _cache;
        private readonly ILogger _logger;
        private const string _applicationName = "LeaderboardApi";

        public LeaderboardController(
            ILeaderboardRepository leaderboardRepository,
            ICache cache,
            ILogger logger)
        {
            _leaderboardRepository = leaderboardRepository ?? throw new ArgumentNullException(nameof(leaderboardRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> GetAuthToken([FromBody] GetAuthTokenRequest request)
        {
            try
            {
                var controllerSecurity = new ControllerSecurity(_cache, Unauthorized(), HttpContext);

                try
                {
                    Validator.Validate(request);
                    await controllerSecurity.CheckIpBan();

                    return Ok(await controllerSecurity.BuildToken(request.UserId));
                }
                catch (InvalidRequestException invalidRequestEx)
                {
                    await _logger.LogErrorAsync(_applicationName, "Request error", invalidRequestEx);
                    return await controllerSecurity.UnauthorizeAndBlackList();
                }
                catch (InvalidLoginException invalidLoginEx)
                {
                    await _logger.LogErrorAsync(_applicationName, "Authentication error", invalidLoginEx);
                    return await controllerSecurity.UnauthorizeAndBlackList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(_applicationName, "Generic error", ex);
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetGlobalRank([FromBody] MaskedRequest<GetGlobalRankRequest> maskedRequest)
        {
            try
            {
                var controllerSecurity = new ControllerSecurity(_cache, Unauthorized(), HttpContext);

                try
                {
                    Validator.Validate(maskedRequest);
                    var request = maskedRequest.GetCoreRequest();
                    Validator.Validate(request);
                    await controllerSecurity.CheckIpBan();
                    await controllerSecurity.CheckToken(request.Token, request.UserId);

                    return Ok(await _leaderboardRepository.GetOrSetGlobalRank(
                        request.UserId,
                        request.GameName,
                        request.ScoreType,
                        request.CurrentLocalScore));
                }
                catch (InvalidRequestException invalidRequestEx)
                {
                    await _logger.LogErrorAsync(_applicationName, "Request error", invalidRequestEx);
                    return await controllerSecurity.UnauthorizeAndBlackList();
                }
                catch (InvalidLoginException invalidLoginEx)
                {
                    await _logger.LogErrorAsync(_applicationName, "Authentication error", invalidLoginEx);
                    return await controllerSecurity.UnauthorizeAndBlackList();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(_applicationName, "Generic error", ex);
                return Unauthorized();
            }
        }
    }
}