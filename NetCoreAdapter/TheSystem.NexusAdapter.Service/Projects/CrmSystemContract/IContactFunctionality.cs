﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Projects.CrmSystemContract
{
    /// <summary>
    /// The contract for the customer service
    /// </summary>
    public interface IContactFunctionality
    {
        /// <summary>
        /// Get a list of all customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> ReadAllAsync();
    }
}