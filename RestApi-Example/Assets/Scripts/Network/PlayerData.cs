using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace Scripts.Network{
    public class PlayerData : MonoBehaviour
    {

        private void Start() {
            StartCoroutine(GetData());
        }

        private IEnumerator GetData(){
            UnityWebRequest data = UnityWebRequest.Get("http://localhost:5092/player/1");
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
    }
}
