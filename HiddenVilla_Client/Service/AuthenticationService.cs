﻿using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
        {
            var content = JsonConvert.SerializeObject(userFromAuthentication);
            var bodycontent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/sigin", bodycontent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resualt = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp); 

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, resualt.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, resualt.userDTO);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resualt.Token);
                return new AuthenticationResponseDTO { IsAuthenticationSuccessful = true }; 
            }
            else
            {
                return resualt;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisterationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration)
        {
            var content = JsonConvert.SerializeObject(userForRegisteration);
            var bodycontent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodycontent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resualt = JsonConvert.DeserializeObject<RegisterationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegisterationResponseDTO { IsRegisterationSuccessful = true };
            }
            else
            {
                return resualt;
            }
        }
    }
}
