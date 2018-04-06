﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    /// <summary>
    /// Mapping for IManyToOneRelation.
    /// </summary>
    public class ManyToOneBiased1Mapper<TClientModel, TClientId, TServerLogic, TServerModel, TServerId> : MapperBase<TClientModel, TClientId, TServerLogic, TServerModel, TServerId>, IManyToOneRelation<TClientModel, TClientId>
    {
        private readonly IManyToManyBiased1<TServerModel, TServerId> _service;
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManyToOneBiased1Mapper(TServerLogic storage, IManyToManyBiased1<TServerModel, TServerId> service, IMapper<TClientModel, TServerLogic, TServerModel> mapper)
        : base(storage, mapper)
        {
            _service = service;
        }

        /// h<inheritdoc />
        public virtual async Task<PageEnvelope<TClientModel>> ReadChildrenWithPagingAsync(TClientId parentId, int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            var serverPage = await _service.ReadReferencedItemsByReference1WithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(serverPage);
            return new PageEnvelope<TClientModel>(serverPage.PageInfo, await CreateAndMapFromServerAsync(serverPage.Data, token));
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public virtual async Task<IEnumerable<TClientModel>> ReadChildrenAsync(TClientId parentId, int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            var serverItems = await _service.ReadReferencedItemsByReference1Async(serverId, limit, token);
            return await CreateAndMapFromServerAsync(serverItems, token);
        }

        /// <inheritdoc />
        public virtual async Task DeleteChildrenAsync(TClientId parentId, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            await _service.DeleteReferencesByReference1(serverId, token);
        }
    }
}
