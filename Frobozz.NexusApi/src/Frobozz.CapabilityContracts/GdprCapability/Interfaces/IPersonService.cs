﻿using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <summary>
    /// Methods for accessing a person
    /// </summary>
    public interface IPersonService : ICrudBasic<PersonCreate, Person, string>
    {
        /// <summary>
        /// Find the first person with an exact match for the name
        /// </summary>
        /// <param name="name">The name to have an exact match for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken));
    }
}