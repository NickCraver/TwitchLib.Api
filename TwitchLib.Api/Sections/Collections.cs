﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Collections
    {
        public Collections(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetCollectionMetadata
            public async Task<Models.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.v5.Collections.CollectionMetadata>($"{Api.baseV5}collections/{collectionId}").ConfigureAwait(false);
            }
            #endregion
            #region GetCollection
            public async Task<Models.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (includeAllItems.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("include_all_items", ((bool)includeAllItems).ToString()));
                return await Api.GetGenericAsync<Models.v5.Collections.Collection>($"{Api.baseV5}collections/{collectionId}/items", getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetCollectionsByChannel
            public async Task<Models.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for catching a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrWhiteSpace(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrWhiteSpace(containingItem))
                    getParams.Add(new KeyValuePair<string, string>("containing_item", containingItem.StartsWith("video:") ? containingItem : $"video:{containingItem}"));

                return await Api.GetGenericAsync<Models.v5.Collections.CollectionsByChannel>($"{Api.baseV5}channels/{channelId}/collections", getParams).ConfigureAwait(false);
            }
            #endregion
            #region CreateCollection
            public async Task<Models.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(collectionTitle)) { throw new BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.PostGenericAsync<Models.v5.Collections.CollectionMetadata>($"{Api.baseV5}channels/{channelId}/collections", "{\"title\": \"" + collectionTitle + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region UpdateCollection
            public async Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(newCollectionTitle)) { throw new BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"{Api.baseV5}collections/{collectionId}", "{\"title\": \"" + newCollectionTitle + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateCollectionThumbnail
            public async Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.PutAsync($"{Api.baseV5}collections/{collectionId}/thumbnail", "{\"item_id\": \"" + itemId + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCollection
            public async Task DeleteCollectionAsync(string collectionId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}collections/{collectionId}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region AddItemToCollection
            public async Task<Models.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (itemType != "video") { throw new BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\"."); }
                return await Api.PostGenericAsync<Models.v5.Collections.CollectionItem>($"{Api.baseV5}collections/{collectionId}/items", "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteItemFromCollection
            public async Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"{Api.baseV5}collections/{collectionId}/items/{itemId}", null, authToken).ConfigureAwait(false);
            }
            #endregion
            #region MoveItemWithinCollection
            public async Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (position < 1) { throw new BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1."); }
                await Api.PutAsync($"{Api.baseV5}collections/{collectionId}/items/{itemId}", "{\"position\": \"" + position + "\"}", null, authToken).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
