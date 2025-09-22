using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class GameAPI : MonoBehaviour
{
    private string baseUrl = "http://localhos:4000/api";

    public IEnumerator RegitserPlayer(string playerName, string password)
    {
        var requesData = new {name  = playerName, password = password};
        string jsonData = JsonConvert.SerializeObject(requesData);
        Debug.Log($"Registerng player : {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success )
            {
                Debug.LogError($"Error registering player : { request.result}");
            }
            else
            {
                Debug.Log("Player register successfully");
            }
        }
    }

    //플레이어 로그인 메서드
    public IEnumerator LoginPlayer(string playerName, string password, Action<PlayerModel> onSuccess)
    {
        var requestData = new {name = playerName,password = password};
        string jsonData = JsonConvert.SerializeObject (requestData);

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/login", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error login player : {request.result}");
            }
            else
            {
                string responseBody = request.downloadHandler.text;

                try
                {
                    var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

                    PlayerModel playerMode = new PlayerModel(responseData["playerName"].ToString())
                    {
                        metal = Convert.ToInt32(responseData["matal"]),
                        crystal = Convert.ToInt32(responseData["crystal"]),
                        deuteriurm = Convert.ToInt32(responseData["deuteriurm"]),
                        Planes = new List<PlaneModel>()
                    };
                    onSuccess?.Invoke(playerMode);
                    Debug.Log("Login successful");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error processing login responve : {ex.Message}");
                }
            }

        }
           
    }
}
