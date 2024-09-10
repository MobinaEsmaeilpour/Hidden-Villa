using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {
        private UserRequestDTO userForRegisteration = new UserRequestDTO();
        public bool IsPRocessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }


        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsPRocessing = true;
            var resualt = await authenticationService.RegisterUser(userForRegisteration);
            if (resualt.IsRegisterationSuccessful)
            {
                IsPRocessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsPRocessing = false;
                Errors = resualt.Errors;
                ShowRegistrationErrors = true;
            }
        }
    }
}
