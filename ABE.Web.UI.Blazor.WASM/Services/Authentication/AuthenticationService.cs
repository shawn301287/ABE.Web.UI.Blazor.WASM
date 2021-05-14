using ABE.Web.UI.Blazor.WASM.Models.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ABE.Web.UI.Blazor.WASM.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
		private readonly AuthenticationStateProvider _customAuthenticationProvider;
		private readonly ILocalStorageService _localStorageService;
		private readonly HttpClient _httpClient;

		public AuthenticationService(ILocalStorageService localStorageService,
			AuthenticationStateProvider customAuthenticationProvider,
			HttpClient httpClient)
		{
			_localStorageService = localStorageService;
			_customAuthenticationProvider = customAuthenticationProvider;
			_httpClient = httpClient;
		}

		public async Task<AuthResponse> RegisterAsync(RegisterModel model)
		{
			// https://localhost:44341/api/authenticate/register
			var response = await _httpClient.PostAsJsonAsync<RegisterModel>("/api/authenticate/register", model);

			var apiResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
			return apiResponse;

			//AuthResponse authData = await response.Content.ReadFromJsonAsync<AuthResponse>();
			//await _localStorageService.SetItemAsync("token", authData.Token);
			//(_customAuthenticationProvider as AppAuthenticationProvider).LoginNotify();

			
		}

		public async Task<AuthResponse> LoginAsync(LoginModel model)
		{
			var response = await _httpClient.PostAsJsonAsync<LoginModel>("/api/authenticate/login", model);

			if (!response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<AuthResponse>(); ;
			}

			AuthResponse authData = await response.Content.ReadFromJsonAsync<AuthResponse>();
			await _localStorageService.SetItemAsync("token", authData.Token);
			(_customAuthenticationProvider as AppAuthenticationProvider).LoginNotify();
			return authData;
		}

		public async Task<bool> LogoutAsync()
		{
			await _localStorageService.RemoveItemAsync("token");
			(_customAuthenticationProvider as AppAuthenticationProvider).LogoutNotify();
			return true;
		}
	}
}
