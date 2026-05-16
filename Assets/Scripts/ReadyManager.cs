using System;
using Fusion;
using UnityEngine;

public class ReadyManager : NetworkBehaviour
{
    public static event Action OnReadyCounterReachedMax;
    public int readyCounter = 0;

    [Rpc]
    public void SetReadyRpc(RpcInfo info = default)
    {
        Debug.Log($"Player {info.Source.PlayerId} is ready! {readyCounter}");
        readyCounter++;
        if (readyCounter >= 2)
        {
            OnReadyCounterReachedMax?.Invoke();
        }
    }

    public override void Spawned()
    {
        base.Spawned();
        NetworkManager.Instance.readyManagerInstance = this;
    }
}
