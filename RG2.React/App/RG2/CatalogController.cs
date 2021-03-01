﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signum.Engine;
using Signum.Engine.Translation;
using Signum.Entities;
using Signum.Entities.Reflection;
using Signum.React.ApiControllers;
using Signum.React.Filters;
using RG2.Entities;
using RG2.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RG2.React.ApiControllers
{
    public class CatalogController : ControllerBase
    {
        static PropertyRoute prCategoryName = PropertyRoute.Construct((CategoryEntity a) => a.CategoryName);
        static PropertyRoute prDescription = PropertyRoute.Construct((CategoryEntity a) => a.Description);

        [HttpGet("api/catalog"), SignumAllowAnonymous]
        public List<CategoryWithProducts> Catalog()
        {
            return ProductLogic.ActiveProducts.Value.Select(a => new CategoryWithProducts
            {
                Category = a.Key.ToLite(),
                Picture = a.Key.Picture?.BinaryFile,
                LocCategoryName = TranslatedInstanceLogic.TranslatedField(a.Key.ToLite(), prCategoryName, null, a.Key.CategoryName),
                LocDescription = TranslatedInstanceLogic.TranslatedField(a.Key.ToLite(), prDescription, null, a.Key.Description),
                Products = a.Value
            }).ToList();
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        public class CategoryWithProducts
        {
            public Lite<CategoryEntity> Category;
            public byte[]? Picture;
            public string LocCategoryName;
            public string LocDescription;
            public List<ProductEntity> Products;
        }
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
    }
}
