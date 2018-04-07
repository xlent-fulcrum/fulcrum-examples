﻿using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public class CrdClientTranslator<TModel> : ReadClientTranslator<TModel>, ICrd<TModel, string>
    where TModel : IValidatable
    {
        private readonly ICrd<TModel, string> _storage;

        protected CrdClientTranslator(ICrd<TModel, string> storage, string idConceptName)
        :base(storage, idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<string> CreateAsync(TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            item = translator.DecorateItem(item);
            var decoratedId = await _storage.CreateAsync(item, token);
            // This is a new id, so there is no purpose in translating it.
            return decoratedId;
        }

        /// <inheritdoc />
        public async Task<TModel> CreateAndReturnAsync(TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            item = translator.DecorateItem(item);
            var decoratedResult = await _storage.CreateAndReturnAsync(item, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }

        /// <inheritdoc />
        public async Task CreateWithSpecifiedIdAsync(string id, TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            id = translator.DecorateId(IdConceptName, id);
            item = translator.DecorateItem(item);
            await _storage.CreateWithSpecifiedIdAsync(id, item, token);
        }

        /// <inheritdoc />
        public async Task<TModel> CreateWithSpecifiedIdAndReturnAsync(string id, TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            id = translator.DecorateId(IdConceptName, id);
            item = translator.DecorateItem(item);
            var decoratedResult = await _storage.CreateWithSpecifiedIdAndReturnAsync(id, item, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ClientName);
            id = translator.DecorateId(IdConceptName, id);
            await _storage.DeleteAsync(id, token);
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            await _storage.DeleteAllAsync(token);
        }
    }
}