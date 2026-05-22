#if !EXCLUDE_STEAMWORKS

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using FlaxEngine.Networking;
using Steamworks;
using Steamworks.Data;

namespace FacepunchSteamworks;

public class SteamNetworkConnectionManager : ConnectionManager
{
    public FacepunchNetworkDriver Driver;

    public event Action<NetworkEventType, ulong, byte[]> NetworkEvent;

    public override void OnConnecting(ConnectionInfo info)
    {
        FlaxEngine.Debug.Log($"[Client] OnConnecting called - State: {info.State}");
        FlaxEngine.Debug.Log($"[Client] OnConnecting called - Identity: {info.Identity}");
        FlaxEngine.Debug.Log($"[Client] OnConnecting called - Address: {info.Address}");
        base.OnConnecting(info);

        //NetworkEvent?.Invoke(NetworkEventType., Driver.TargetSteamId, []);
    }

    public override void OnConnected(ConnectionInfo info)
    {
        FlaxEngine.Debug.Log($"[Client] OnConnected called - State: {info.State}");
        FlaxEngine.Debug.Log($"[Client] OnConnected called - Identity: {info.Identity}");
        FlaxEngine.Debug.Log($"[Client] OnConnected called - Address: {info.Address}");
        base.OnConnected(info);

        NetworkEvent?.Invoke(NetworkEventType.Connected, Driver.TargetSteamId, []);
    }

    public override void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
    {
        byte[] bytes = new byte[size];

        unsafe
        {
            fixed (void* p = bytes)
            {
                Buffer.MemoryCopy((void*)data, p, size, size);
            }
        }

        NetworkEvent?.Invoke(NetworkEventType.Message, Driver.TargetSteamId, bytes);
    }
}

#endif