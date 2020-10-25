using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBleSearchAssignment.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
