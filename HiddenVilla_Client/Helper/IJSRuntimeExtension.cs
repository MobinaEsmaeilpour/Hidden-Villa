using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToasterSuccess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToasterError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
