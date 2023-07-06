﻿using BethanysPieShopHRM.Shared;
using System.Net.Http;
using System.Text;
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;



namespace BethanysPieShopHRM.App.Services
{
	public class CountryDataService : ICountryDataService
	{

		private readonly HttpClient _httpClient;

		public CountryDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<Country>> GetAllCountries()
		{
			return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>
				(await _httpClient.GetStreamAsync($"api/country"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}

		public async Task<Country> GetCountryById(int countryId)
		{
			return await JsonSerializer.DeserializeAsync<Country>
				(await _httpClient.GetStreamAsync($"api/country{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
	}
}