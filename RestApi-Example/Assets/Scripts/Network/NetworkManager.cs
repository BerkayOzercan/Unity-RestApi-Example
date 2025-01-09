using System.Collections;
using System.Collections.Generic;
using Scripts.Network;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetData());
        StartCoroutine(PostData());
    }

    public IEnumerator GetData()
    {
        UnityWebRequest data = UnityWebRequest.Get("http://localhost:5092/player");
        yield return data.SendWebRequest();

        if (data.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(data.error);
        }
        else
        {
            // Show results as text
            Debug.Log(data.downloadHandler.text);
        }
    }

    public IEnumerator PostData()
    {
        string uri = "http://localhost:5092/player";
        WWWForm form = new WWWForm();
        form.AddField("title", "test data");

        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Success");
            }
        }
    }
}
