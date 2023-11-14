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

        PlayFabCloudScriptAPI.ExecuteFunction(request, ResultCallback, ErrorCallback);
    }

    private void ErrorCallback(PlayFabError obj)
    {
        throw new System.NotImplementedException();
    }

    private void ResultCallback(ExecuteFunctionResult obj)
    {
        Debug.Log(obj.FunctionResult);
    }
}
