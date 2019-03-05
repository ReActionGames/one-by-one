using UnityEngine;
using UnityEngine.SceneManagement;

namespace Continuous
{
    public class UISceneLoader : MonoBehaviour
    {
        [SerializeField] private string uiSceneName;

        private void Start()
        {
            SceneManager.LoadScene(uiSceneName, LoadSceneMode.Additive);
        }
    }
}