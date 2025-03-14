using UnityEngine.SceneManagement;

namespace Assets.LevelSystem.Scripts
{
    // TODO refactor and implement save system
    public class LevelManager : Singleton<LevelManager>
    {
        public void LoadStartMenu()
        {
            SceneManager.LoadScene("StartMenu");
        }

        public void Load(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void Next()
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }

        public void Restart()
        {
            string activeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(activeScene);
        }
    }
}
