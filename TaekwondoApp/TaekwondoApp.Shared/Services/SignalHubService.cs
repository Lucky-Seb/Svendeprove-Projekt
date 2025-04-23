using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace TaekwondoApp.Shared.Services
{
    public class SignalRService
    {
        private readonly HubConnection _hubConnection;

        public SignalRService(string hubUrl, HttpMessageHandler? httpMessageHandler = null)
        {
            var hubConnectionBuilder = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    if (httpMessageHandler != null)
                    {
                        // Wrap the handler to prevent disposal
                        options.HttpMessageHandlerFactory = _ => new NonDisposableHttpMessageHandler(httpMessageHandler);
                    }
                })
                .WithAutomaticReconnect();

            _hubConnection = hubConnectionBuilder.Build();
        }

        public HubConnection HubConnection => _hubConnection;

        public async Task StartConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }

        public async Task StopConnectionAsync()
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.StopAsync();
            }
        }
        public class NonDisposableHttpMessageHandler : DelegatingHandler
        {
            public NonDisposableHttpMessageHandler(HttpMessageHandler innerHandler)
                : base(innerHandler)
            {
            }

            protected override void Dispose(bool disposing)
            {
                // Prevent disposal of the inner handler
            }
        }
    }
}
