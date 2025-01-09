using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{

    public IEnumerator GetData(string dataType)
    {
        UnityWebRequest data = UnityWebRequest.Get("http://localhost:5092/player" + dataType);
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
        form.AddField("http://localhost:5092/player", "test data");

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
