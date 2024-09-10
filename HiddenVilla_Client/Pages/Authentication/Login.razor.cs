using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO userForAuthentication = new AuthenticationDTO();
        public bool IsPRocessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }
        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }


        private async Task LoginUser()
        {
            ShowAuthenticationErrors = false;
            IsPRocessing = true;
            var resualt = await authenticationService.Login(userForAuthentication);
            if (resualt.IsAuthenticationSuccessful)
            {
                IsPRocessing = false;
                var absoluteUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/"+ReturnUrl);
                }
            }
            else
            {
                IsPRocessing = false;
                Errors = resualt.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
