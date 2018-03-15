﻿using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Ingests
    {
        public Ingests(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetIngests
            public async Task<Models.v3.Ingests.IngestsResponse> GetIngestsAsync()
            {
                return await Api.GetGenericAsync<Models.v3.Ingests.IngestsResponse>($"{Api.baseV3}ingests", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetIngestServerList
            public async Task<Models.v5.Ingests.Ingests> GetIngestServerListAsync()
            {
                return await Api.GetGenericAsync<Models.v5.Ingests.Ingests>($"{Api.baseV5}ingests").ConfigureAwait(false);
            }
            #endregion
        }
    }
}
