using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
//using Openfort.Model;
using PlayFab;
using PlayFab.CloudScriptModels;
using TMPro;
using UnityEngine;

public class ConnectUrl : MonoBehaviour
{
    [Serializable]
    private class Web3ActionResponseShort
    {
            
    }
    
    public TMP_InputField urlInput;
    public TextMeshProUGUI statusText;

    private string _connectionId;

    public void OnConnectBtnClickHandler()
    {
        CreateWeb3Connection(OpenfortController.Instance.GetPlayerId(), 80001, urlInput.text);    
    }

    #region AZURE_FUNCTION_CALLS
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
    
    private void SubmitWeb3Action(string connectionId, string actionId, bool approve)
    {
        statusText.text = "Submitting Web3 Action...";

        var request = new ExecuteFunctionRequest
        {
            FunctionName = "SubmitWeb3Action",
            FunctionParameter = new
            {
                connectionId,
                actionId,
                approve
            }
        };

        PlayFabCloudScriptAPI.ExecuteFunction(request, OnSubmitWeb3ActionSuccess, OnSubmitWeb3ActionError);
    }
    #endregion

    #region SUCCESS_CALLBACKS
    private void OnCreateWeb3ConnectionSuccess(ExecuteFunctionResult result)
    {
        Debug.Log(result.FunctionResult.ToString());
        _connectionId = result.FunctionResult.ToString();

        StartCoroutine(WaitAndExecuteAction());
    }
    
    private void OnGetWeb3ActionSuccess(ExecuteFunctionResult result)
    {
        var response = result.FunctionResult.ToString();
        Debug.Log(response);
        
        /*
        List<Web3ActionResponse> listResponses = JsonConvert.DeserializeObject<List<Web3ActionResponse>>(response);
        
        if (listResponses.Count == 0)
        {
            GetWeb3Action(_connectionId);
            return;
        }

        foreach (var web3Action in listResponses)
        {
            Debug.Log(web3Action.Id);
        }
        */
    }
    
    private void OnSubmitWeb3ActionSuccess(ExecuteFunctionResult obj)
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region ERROR_CALLBACKS
    private void OnCreateWeb3ConnectionError(PlayFabError error)
    {
        throw new System.NotImplementedException();
    }

    private void OnGetWeb3ActionError(PlayFabError error)
    {
        throw new System.NotImplementedException();
    }
    
    private void OnSubmitWeb3ActionError(PlayFabError obj)
    {
        throw new NotImplementedException();
    }
    #endregion

    IEnumerator WaitAndExecuteAction()
    {
        yield return new WaitForSeconds(1.5f);
        GetWeb3Action(_connectionId);
    }
}
