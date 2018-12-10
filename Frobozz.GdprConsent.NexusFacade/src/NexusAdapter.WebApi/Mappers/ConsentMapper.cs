﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <summary>
    /// Maps between storage and service models
    /// </summary>
    public class ConsentMapper : 
        CrudMapper<ConsentCreate, Consent, string, ConsentTable, Guid>,
        IConsentService
    {
        /// <inheritdoc />
        public ConsentMapper(IStorage service, IMappable<Consent, ConsentTable> mapper) 
            : base(service.Consent, mapper)
        {
        }
    }
}