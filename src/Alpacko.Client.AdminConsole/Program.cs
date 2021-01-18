using Alpacko.Client.AdminConsole.Controllers;
using Alpacko.Client.AdminConsole.Services;
using Alpacko.Client.AdminConsole.Views;
using System;
using System.Net.Http;

namespace Alpacko.Client.AdminConsole
{
    internal class Program
    {
        private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJKRThaaWxBZ21haWwyY29tIiwianRpIjoiY2Q2NWUxMzUtN2M0MS00Njg1LWFmNDgtMGIzYWZhNmQ0MzNiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2MTA5OTUzNDcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.ST3FLLr13uBLcev6Y63_ZIPjvLei7-5-7__LQsFqqaw";

        private static void Main(string[] args)
        {
            ViewManager.AddService(new HttpClient() { BaseAddress = new Uri("https://localhost:44396/") });
            ViewManager.AddService<AuthService>().MarkUserAsAuthenticated(token);
            ViewManager.AddService<PostOfficeController>();

            ViewManager.RenderView<MainMenuView>();

            ViewManager.Dispose();
        }
    }
}