using BethanysPieShopHRM.Shared;
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BethanysPieShopHRM.App.Services
{
	public interface ICountryDataService
	{
		Task<IEnumerable<Country>> GetAllCountries();

		Task<Country> GetCountryById(int countryId);
	}
}
