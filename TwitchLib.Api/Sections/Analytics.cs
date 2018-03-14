using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Analytics
    {
        public Analytics(TwitchAPI api)
        {
            helix = new Helix(api);
        }
        
        public Helix helix { get; }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }

            #region GetGameAnalytics
            public async Task<Models.Helix.Analytics.GetGameAnalyticsResponse> GetGameAnalytics(string gameId = null, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Helix_Analytics_Read_Games, accessToken);
                List<KeyValuePair<string, string>> getParams = null;
                if (gameId != null)
                    getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("game_id", gameId) };

                return await Api.GetGenericAsync<Models.Helix.Analytics.GetGameAnalyticsResponse>("https://api.twitch.tv/helix/analytics/games", getParams, accessToken, ApiVersion.Helix).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
