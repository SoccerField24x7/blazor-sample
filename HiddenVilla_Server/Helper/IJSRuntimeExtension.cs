using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenVilla_Server.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime JSRuntime, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static async ValueTask ShowSuccessModal(this IJSRuntime JSRuntime, string title, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowModal", "success", title, message);
        }

        public static async ValueTask ShowErrorModal(this IJSRuntime JSRuntime, string title, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowModal", "error", title, message);
        }
    }
}