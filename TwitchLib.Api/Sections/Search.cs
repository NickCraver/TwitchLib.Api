﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Search
    {
        public Search(TwitchAPI api)
        {
            v5 = new V5(api);
        }
        
        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region SearchChannels
            public async Task<Models.v5.Search.SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("query", encodedSearchQuery) };
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

                return await Api.TwitchGetGenericAsync<Models.v5.Search.SearchChannels>("/search/channels", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region SearchGames
            public async Task<Models.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                getParams.Add(new KeyValuePair<string, string>("query", encodedSearchQuery));
                if(live.HasValue)
                {
                    getParams.Add(live.Value
                        ? new KeyValuePair<string, string>("live", "true")
                        : new KeyValuePair<string, string>("live", "false"));
                }
                return await Api.TwitchGetGenericAsync<Models.v5.Search.SearchGames>("/search/games", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region SearchStreams
            public async Task<Models.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("query", encodedSearchQuery) };
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (hls.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("hls", hls.Value.ToString()));

                return await Api.TwitchGetGenericAsync<Models.v5.Search.SearchStreams>("/search/streams", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
