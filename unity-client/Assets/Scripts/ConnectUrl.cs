using System;
using Newtonsoft.Json;
using Openfort.Model;
using PlayFab;
using PlayFab.CloudScriptModels;
using TMPro;
using UnityEngine;

public class ConnectUrl : MonoBehaviour
{
    public TMP_InputField urlInput;
    public TextMeshProUGUI statusText;

    public void OnConnectBtnClickHandler()
    {
        CreateWeb3Connection(OpenfortController.Instance.GetPlayerId(), 80001, urlInput.text);    
    }
    
    private void CreateWeb3Connection(string playerId, int chainId, string uri)
    {
        statusText.text = "Creating Web3 Connection...";

        var request = new ExecuteFunctionRequest
        {
            FunctionName = "CreateWeb3Connection",
            FunctionParameter = new
            {
                playerId,
                chainId,
                uri
            }
        };

        PlayFabCloudScriptAPI.ExecuteFunction(request, OnCreateWeb3ConnectionSuccess, OnCreateWeb3ConnectionError);
    }
    
    private void GetWeb3Action(string connectionId)
    {
        statusText.text = "Getting Web3 Action...";

        var request = new ExecuteFunctionRequest
        {
            FunctionName = "GetWeb3Action",
            FunctionParameter = new
            {
                connectionId
            }
        };

        PlayFabCloudScriptAPI.ExecuteFunction(request, OnGetWeb3ActionSuccess, OnGetWeb3ActionError);
    }

    private void OnGetWeb3ActionError(PlayFabError error)
    {
        throw new System.NotImplementedException();
    }

    private void OnGetWeb3ActionSuccess(ExecuteFunctionResult result)
    {
        Debug.Log(result.FunctionResult.ToString());
    }

    private void OnCreateWeb3ConnectionError(PlayFabError error)
    {
        throw new System.NotImplementedException();
    }

    private void OnCreateWeb3ConnectionSuccess(ExecuteFunctionResult result)
    {
        Debug.Log(result.FunctionResult.ToString());
        var connectionId = result.FunctionResult.ToString();
        
        GetWeb3Action(connectionId);
    }
}
