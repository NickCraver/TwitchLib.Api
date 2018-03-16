﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Chat
    {
        public Chat(TwitchAPI api)
        {
            v5 = new V5(api);
        }
        
        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetChatBadgesByChannel
            public async Task<Models.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchGetGenericAsync<Models.v5.Chat.ChannelBadges>($"/chat/{channelId}/badges", ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetChatEmoticonsBySet
            public async Task<Models.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
            {
                List<KeyValuePair<string, string>> getParams = null;
                if(emotesets != null && emotesets.Count > 0)
                    getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets)) };
                return await Api.TwitchGetGenericAsync<Models.v5.Chat.EmoteSet>("/chat/emoticon_images", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetAllChatEmoticons
            public async Task<Models.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
            {
                return await Api.TwitchGetGenericAsync<Models.v5.Chat.AllChatEmotes>("/chat/emoticons", ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetChatRoomsByChannel 
            public async Task<Models.v5.Chat.ChatRoomsByChannelResponse> GetChatRoomsByChannelAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Any, authToken);
                return await Api.TwitchGetGenericAsync<Models.v5.Chat.ChatRoomsByChannelResponse>($"/chat/{channelId}/rooms", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
        }
    }
}