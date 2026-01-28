using System.Collections.Concurrent;
using Filminurk.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Filminurk.Controllers
{
    public class ChatHub : Hub
    {
    }
}
