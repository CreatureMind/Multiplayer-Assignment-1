using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SessionItem
{
    public string sessionName;
}

[CreateAssetMenu(fileName = "LobbyRow", menuName = "Scriptable Objects/LobbyRow")]
public class SessionsListDataSO : ScriptableObject
{
    public List<SessionItem> sessionsList = new List<SessionItem>();
}
