﻿using System;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Helpers;
using Xlent.Lever.Libraries2.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Basic
{
    /// <inheritdoc />
    public class PersonConsentMapper : IReadMapper<PersonConsent, PersonConsentTable>
    {
        /// <inheritdoc />
        public PersonConsent MapFromServer(PersonConsentTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                PersonId = MapperHelper.MapToType<string, Guid>(source.PersonId),
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}