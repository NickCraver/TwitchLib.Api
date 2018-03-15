﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Blocks
    {
        public Blocks(TwitchAPI api)
        {
            v3 = new V3(api);
        }

        public V3 v3 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            { }

            #region GetBlocks
            public async Task<Models.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Read, accessToken);
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                return await Api.GetGenericAsync<Models.v3.Blocks.GetBlocksResponse>($"{Api.baseV3}users/{channel}/blocks", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region CreateBlock
            public async Task<Models.v3.Blocks.Block> CreateBlockAsync(string channel, string target, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, accessToken);
                return await Api.PutGenericAsync<Models.v3.Blocks.Block>($"{Api.baseV3}users/{channel}/blocks/{target}", null, null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region RemoveBlock
            public async Task RemoveBlockAsync(string channel, string target, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, accessToken);
                await Api.DeleteAsync($"{Api.baseV3}users/{channel}/blocks/{target}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
