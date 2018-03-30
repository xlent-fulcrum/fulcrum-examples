﻿using System;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model
{
    public class PersonConsentTable : StorableItem, ITimeStamped
    {
        public bool HasGivenConsent { get; set; }
        /// <summary>
        ///  The person that this address is for
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        ///  The person that this address is for
        /// </summary>
        public Guid ConsentId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordCreatedAt { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordUpdatedAt { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotDefaultValue(PersonId, nameof(PersonId), errorLocation);
            FulcrumValidate.IsNotDefaultValue(ConsentId, nameof(ConsentId), errorLocation);
        }
    }
}