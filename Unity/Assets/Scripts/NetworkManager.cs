using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

[Serializable]
public class NetworkMessage
{
    public string type;
    public string playerId;
    public string message;
    public Vector3 position;
    public Vector3 rotation;
} 

[Serializable]
public class Vector3Data
{
    public float x; 
    public float y; 
    public float z;

    public Vector3Data(Vector3 v)
    {
        x = v.x; 
        y = v.y; 
        z = v.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

public class NetworkManager : MonoBehaviour
{
    private WebSocket webSocket;
    [SerializeField] private string serverUrl = "ws://localhost:3000";

    [SerializeField] private InputField messageInput;
    [SerializeField] private Button sendButton;
    [SerializeField] private Button connectButton;
    [SerializeField] private Text chatLog;
    [SerializeField] private Text stautusText;



    [SerializeField] private Transform localPlayer;
    [SerializeField] private GameObject remotePlayerPrefabs;
    [SerializeField] private float positionSendRate = 0.1f;

    private string myPlayerId;
    private Dictionary<string, GameObject> remotePlayers = new Dictionary<string, GameObject>();
    private float lastPositionSendTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if(webSocket != null)
        {
            webSocket.DispatchMessageQueue();
        }
#endif
        if(webSocket != null && webSocket.State == WebSocketState.Open && localPlayer != null)
        {
            if(Time.time - lastPositionSendTime >= positionSendRate)
            {
                //SendPositionUpdate();
                lastPositionSendTime = Time.time;
            }
        }
    }

    private void CreatgeRomotePlayer(string playerId, Vector3Data position, Vector3Data rotation)
    {
        if (remotePlayers.ContainsKey(playerId)) return;
        if(remotePlayerPrefabs == null)
        {
            return;
        }

        Vector3 pos = position != null ? position.ToVector3() : Vector3.zero;
        Vector3 rot = rotation != null ? rotation.ToVector3() : Vector3.zero;

        GameObject player = Instantiate(remotePlayerPrefabs, pos, Quaternion.Euler(rot));
        player.name = "RemotePlayer_" + playerId;
        remotePlayers.Add(playerId, player );


    }

    private void RemoveRemotePlayer(string playerId)
    {
        if(remotePlayers.ContainsKey(playerId))
        {
            Destroy(remotePlayers[playerId]);
            remotePlayers.Remove(playerId);
            
        }
    }

    private void UpdateRomotePlayer(string playerId, Vector3Data position, Vector3Data rotation)
    {
        if(!remotePlayers.ContainsKey(playerId))
        {
            CreatgeRomotePlayer(playerId, position, rotation);
            return;
        }

        GameObject player = remotePlayers[playerId];
        if (player == null) return;

        if(position != null)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, position.ToVector3(), Time.deltaTime * 10f);
        }

        if(rotation != null)
        {
            Quaternion targetRotation = Quaternion.Euler(rotation.ToVector3());
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void OnDestroy()
    {
        if(sendButton != null)
        {
            //sendButton.onClick.RemoveListener(SendChatMessage);
        }
    }

    private async void SendPositionUpdate()
    {
        if (localPlayer == null) return;
        
        NetworkMessage message = new NetworkMessage();
    }

    private void HandleMessage(string json)
    {
        try
        {
            NetworkMessage message = JsonConvert.DeserializeObject<NetworkMessage>(json);

            switch(message.type)
            {
                case "connection": 
                    myPlayerId = message.playerId;
                    break;
            }
        }
        catch (Exception e)
        {
                
        }
    }

}
