﻿// <copyright file="GetCategoryByIdResponse.cs" company="FluentPOS">
// --------------------------------------------------------------------------------------------------
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Categories
{
    public record GetCategoryByIdResponse(Guid Id, string Name, string Detail);
}