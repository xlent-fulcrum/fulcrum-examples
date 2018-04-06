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
    public class ManyToOneMapper<TClientModel, TClientId, TServerLogic, TServerModel, TServerId> : MapperBase<TClientModel, TClientId, TServerLogic, TServerModel, TServerId>, IManyToOneRelation<TClientModel, TClientId>
    {
        private readonly IManyToOneRelation<TServerModel, TServerId> _service;
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManyToOneMapper(TServerLogic storage, IManyToOneRelation<TServerModel, TServerId> service, IMapper<TClientModel, TServerLogic, TServerModel> mapper)
        : base(storage, mapper)
        {
            _service = service;
        }

        /// h<inheritdoc />
        public virtual async Task<PageEnvelope<TClientModel>> ReadChildrenWithPagingAsync(TClientId parentId, int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            var serverPage = await _service.ReadChildrenWithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(serverPage);
            return new PageEnvelope<TClientModel>(serverPage.PageInfo, await CreateAndMapFromServerAsync(serverPage.Data, token));
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public virtual async Task<IEnumerable<TClientModel>> ReadChildrenAsync(TClientId parentId, int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            var serverItems = await _service.ReadChildrenAsync(serverId, limit, token);
            return await CreateAndMapFromServerAsync(serverItems, token);
        }

        /// <inheritdoc />
        public virtual async Task DeleteChildrenAsync(TClientId parentId, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(parentId);
            await _service.DeleteChildrenAsync(serverId, token);
        }
    }
}
