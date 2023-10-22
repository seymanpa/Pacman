using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class FinalSceneController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _highScoreText;
        [SerializeField]
        private TMP_Text _scoreText;

        public void Start()
        {

            ScoreDisplay();
        }

        private void ScoreDisplay()
        {
            _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            _scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}

