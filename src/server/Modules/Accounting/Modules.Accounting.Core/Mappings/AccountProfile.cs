// --------------------------------------------------------------------------------------------------
// <copyright file="ProductProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Accounting.Account;
using FluentPOS.Shared.DTOs.Accounting.Payments;

namespace FluentPOS.Modules.Accounting.Core.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<GetAccountByCustomerIdResponse, Account>().ReverseMap();
            CreateMap<Payment, PaymentResponse>().ReverseMap();
            CreateMap<GetPaymentsResponse, Payment>().ReverseMap();
            CreateMap<PaginatedAccountingFilter, GetPaymentsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}