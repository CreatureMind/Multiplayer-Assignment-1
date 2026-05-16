using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    private UIDocument _uiDocument;
    
    [Header("Menus")]
    [SerializeField] private VisualTreeAsset sessionsListView;
    [SerializeField] private VisualTreeAsset roomsListView;
    [SerializeField] private VisualTreeAsset roomView;
    
    [Header("Templates")]
    [SerializeField] private VisualTreeAsset sessionRowTemplate;
    [SerializeField] private VisualTreeAsset roomRowTemplate;
    
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        //NetworkManager.Instance.OnSessionDataRefreshed += UpdatePlayerCountInLobby;
        ReadyManager.OnReadyCounterReachedMax += MaxPlayersReady;
    }

    private void OnDisable()
    {
        if (NetworkManager.Instance)
        {
            //NetworkManager.Instance.OnSessionUpdated -= UpdateLobbyUIList;
        }
        ReadyManager.OnReadyCounterReachedMax -= MaxPlayersReady;
    }
    
    private void Start()
    {
        ShowSessionsListView();
    }
    
    private void MaxPlayersReady()
    {
        throw new System.NotImplementedException();
    }

    private void ShowSessionsListView()
    {
        _uiDocument.visualTreeAsset = sessionsListView;
        var root = _uiDocument.rootVisualElement;
        
        var scrollView = root.Q<ScrollView>("session-scroll-view");
        if (scrollView == null)
        {
            Debug.LogError("Could not find ListView named 'session-scroll-view' in sessionsListView.");
            return;
        }

        UpdateLobbyUIList(scrollView);
    }
    
    private void UpdateLobbyUIList(ScrollView scrollView)
    {
        scrollView.Clear();
        
        if (!NetworkManager.Instance.SessionsListData) return;
        var availableSessions = NetworkManager.Instance.SessionsListData.sessionsList;

        foreach (var session in availableSessions)
        {
            var sessionRow = sessionRowTemplate.CloneTree();
            
            var sessionName = sessionRow.Q<Label>("lobby-name");
            if (sessionName != null) sessionName.text = session.sessionName;

            var enterBtn = sessionRow.Q<Button>("enter-button");
            if (enterBtn != null)
            {
                enterBtn.clicked += () =>
                {
                    enterBtn.SetEnabled(false);
                    NetworkManager.Instance.ConnectToCustomLobby(session.sessionName);
                    ShowRoomsListView();
                };
            }
            
            scrollView.Add(sessionRow);
        }
    }
    
    private void UpdatePlayerCountInLobby(List<SessionInfo> arg1, int arg2)
    {
        throw new System.NotImplementedException();
    }

    private void ShowRoomsListView()
    {
        _uiDocument.visualTreeAsset = roomsListView;
        var root = _uiDocument.rootVisualElement;
    }
}
