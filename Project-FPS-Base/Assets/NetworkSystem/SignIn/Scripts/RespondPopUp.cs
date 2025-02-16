using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class RespondPopUp : MonoBehaviour
    {
        [Header("Respond PopUp")]
        [SerializeField]
        private TMP_Text _respondText;
        [SerializeField]
        private Image _backGround;

        public void SetMessage(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                _backGround.color = Color.green;
                _respondText.text = message;
                StartCoroutine(DestroyThis(1f));
            }
            else
            {
                _backGround.color = Color.red;
                _respondText.text = message;
                StartCoroutine(DestroyThis(3f));
            }
        }

        private IEnumerator DestroyThis(float value)
        {
            yield return new WaitForSecondsRealtime(value);
            Destroy(gameObject);
        }
    }

}

