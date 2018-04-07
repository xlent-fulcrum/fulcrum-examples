﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public class ManyToOneClientTranslator<TModel> : ClientTranslatorBase, IManyToOneRelation<TModel, string>
    where TModel : IValidatable
    {
        private readonly IManyToOneRelation<TModel, string> _storage;

        protected ManyToOneClientTranslator(IManyToOneRelation<TModel, string> storage, string idConceptName)
        :base(idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<TModel>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            parentId = translator.DecorateId(IdConceptName, parentId);
            var result = await _storage.ReadChildrenWithPagingAsync(parentId, offset, limit, token);
            await translator.Add(result).ExecuteAsync();
            return translator.Translate(result);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TModel>> ReadChildrenAsync(string parentId, int limit = int.MaxValue, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            parentId = translator.DecorateId(IdConceptName, parentId);
            var result = await _storage.ReadChildrenAsync(parentId, limit, token);
            var array = result as TModel[] ?? result.ToArray();
            await translator.Add(array).ExecuteAsync();
            return translator.Translate(array);
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(string parentId, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            parentId = translator.DecorateId(IdConceptName, parentId);
            await _storage.DeleteChildrenAsync(parentId, token);
        }
    }
}