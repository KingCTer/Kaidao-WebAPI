
// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace Kaidao.Services.Api.Pages.Logout;

public class LoggedOutViewModel
{
    public string PostLogoutRedirectUri { get; set; }
    public string ClientName { get; set; }
    public string SignOutIframeUrl { get; set; }
    public bool AutomaticRedirectAfterSignOut { get; set; }
}