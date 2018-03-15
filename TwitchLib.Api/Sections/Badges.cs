﻿using System.Threading.Tasks;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Badges
    {
        public V5 v5 { get; }

        public Badges(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }

            #region GetSubscriberBadgesForChannel
            public async Task<Models.v5.Badges.ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Badges.ChannelDisplayBadges>($"{Api.baseBadges}v1/badges/channels/{channelId}/display").ConfigureAwait(false);
            }
            #endregion

            #region GetGlobalBadges
            public async Task<Models.v5.Badges.GlobalBadgesResponse> GetGlobalBadgesAsync()
            {
                return await Api.GetGenericAsync<Models.v5.Badges.GlobalBadgesResponse>("https://badges.twitch.tv/v1/badges/global/display").ConfigureAwait(false);
            }
            #endregion
        }
    }
}