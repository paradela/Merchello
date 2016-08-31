﻿namespace Merchello.Web.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Web.Models.Ui.Rendering;

    /// <summary>
    /// Defines a ProductFilterGroupService.
    /// </summary>
    public interface IProductFilterGroupService : IEntityProxyService<IProductFilterGroup>
    {
        /// <summary>
        /// Gets a collection of <see cref="IProductFilterGroup"/> that has at least one filter that contains a product with key passed as parameter.
        /// </summary>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProductFilterGroup}"/>.
        /// </returns>
        IEnumerable<IProductFilterGroup> GetFilterGroupsContainingProduct(Guid productKey);

        /// <summary>
        /// Gets a collection of <see cref="IProductFilterGroup"/> in which NONE of the filters contains a product with key passed as parameter.
        /// </summary>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProductFilterGroup}"/>.
        /// </returns>
        IEnumerable<IProductFilterGroup> GetFilterGroupsNotContainingProduct(Guid productKey);
    }
}