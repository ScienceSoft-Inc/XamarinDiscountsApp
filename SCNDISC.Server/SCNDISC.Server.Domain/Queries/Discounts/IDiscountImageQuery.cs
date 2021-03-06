﻿using System.Threading.Tasks;

namespace SCNDISC.Server.Domain.Queries.Discounts
{
	public interface IDiscountImageQuery
	{
		Task<string> GetImageBase64Async(string id);
        Task<string> GetLogoBase64Async(string id);
    }
}