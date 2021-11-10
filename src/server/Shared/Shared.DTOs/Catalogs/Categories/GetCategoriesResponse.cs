// --------------------------------------------------------------------------------------------------
// <copyright file="GetCategoriesResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Categories
{
    public class GetCategoriesResponse {
        public Guid Id  {get; set;}
        public string Name  {get; set;}

        public string Detail {get; set;}

        public GetCategoriesResponse(Guid id, string name, string detail) {
            Id = id;
            Name = name;
            Detail = detail;
        }
    }
}