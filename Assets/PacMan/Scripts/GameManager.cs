using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PacMan
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PacmanController _pacman;
        [SerializeField]
        private GhostController[] _ghosts;
        [SerializeField]
        private Transform _pelletsTransform;
        [SerializeField]
        private Transform _spawner;
        [SerializeField]
        private CollectibleItems _items;

        public int ghostMultiplier { get; private set; } = 1;
        public int score { get; private set; }
        public int lives { get; private set; }
        public int highScore { get; private set; }

        [SerializeField]
        private TMP_Text _gameOverText;
        [SerializeField]
        private TMP_Text _scoreText;
        [SerializeField]
        private TMP_Text _highScoreText;
        [SerializeField]
        private Image _live1;
        [SerializeField]
        private Image _live2;
        [SerializeField]
        private Image _live3;

        [SerializeField]
        private AudioSource pelletEating;
        [SerializeField]
        private AudioSource ghostEating;
        [SerializeField]
        private AudioSource pacmanDeath;

        public GameObject SettingsMenu;

        private bool isPaused = false;

        private void Start()
        {
            pelletEating = GetComponent<AudioSource>();
            NewGame();
        }

        private void Update()
        {
 
            if (Input.anyKeyDown && this.lives <= 0)
            {
                NewGame();
            }
            _scoreText.text = score.ToString();
        }

        private void NewGame()
        {
            GetHighScore();
            SetHighScore(score);
            SetScore(0);
            SetLives(3);
            NewRound();
        }
        private void NewRound()
        {
            foreach (Transform pellet in _pelletsTransform)
            {
                pellet.gameObject.SetActive(true);
            }

            ResetState();
        }

        private void ResetState()
        {
            for (int i = 0; i < _ghosts.Length; i++)
            {
                _ghosts[i].ResetState();
            }
            _pacman.ResetState();
            Invoke(nameof(SpawnItem), 5.0f);
            _gameOverText.text = "";
        }

        private void GameOver()
        {
            _gameOverText.text = "GAME  OVER";
            for (int i = 0; i < _ghosts.Length; i++)
            {
                _ghosts[i].gameObject.SetActive(false);
            }
            _pacman.gameObject.SetActive(false);

            SetHighScore(score);
            PlayerPrefs.SetInt("Score", score);

            Invoke(nameof(FinalScene), 3.0f);
        }

        private void SetScore(int score)
        {
            this.score = score;
        }

        private void SetLives(int lives)
        {
            this.lives = lives;
        }

        public void GhostEaten(GhostController ghost)
        {
            SetScore(this.score + (ghost.point * this.ghostMultiplier));
            this.ghostMultiplier++;
            ghostEating.Play();
        }

        public void PacmanEaten()
        {
            _pacman.DeathSequence();
            pacmanDeath.Play();

            SetLives(this.lives - 1);
            switch (lives)
            {
                case 0:
                    _live1.enabled = false;
                    _live2.enabled = false;
                    _live3.enabled = false; break;
                case 1:
                    _live1.enabled = true;
                    _live2.enabled = false;
                    _live3.enabled = false; break;
                case 2:
                    _live1.enabled = true;
                    _live2.enabled = true;
                    _live3.enabled = false; break;
                case 3:
                    _live1.enabled = true;
                    _live2.enabled = true;
                    _live3.enabled = true; break;
            }
                
                    
            if (this.lives > 0)
            {
    
                Invoke(nameof(ResetState), 3.0f);  
            }    
            else
                Invoke(nameof(GameOver), 3.0f);
        }

        public void PelletEaten(PelletContoller pellet)
        {
            if(!pelletEating.isPlaying)
            {
                pelletEating.Play();
            }
            pellet.gameObject.SetActive(false);
            SetScore(this.score + pellet.point);
            if(!HasRemainingPellets())
            {
                this._pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);
            }
        }

        public void PowerPelletEaten(PowerPelletController powerPellet)
        {
            for (int i = 0; i < _ghosts.Length; i++)
            {
                _ghosts[i].scared.Enable(powerPellet.duration);
            }

            PelletEaten(powerPellet);
            CancelInvoke(nameof(ResetGhostMultiplier));
            Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
        }

        public void ItemEaten(CollectibleItems item)
        {
            item.gameObject.SetActive(false);
            SetScore(this.score + item.point);
        }

        private bool HasRemainingPellets()
        {
            foreach (Transform pellet in _pelletsTransform)
            {
                if(pellet.gameObject.activeSelf)
                    return true;
            }
            return false;
        }

        private void ResetGhostMultiplier()
        {
            this.ghostMultiplier = 1;
        }

        private void SpawnItem()
        {
            Instantiate(_items, _spawner.position, _spawner.rotation);

        }

        private void SetHighScore(int score)
        {
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
                highScore = score;
                _highScoreText.text = score.ToString();
            }
        }

        private void GetHighScore()
        {
            this.highScore = PlayerPrefs.GetInt("HighScore");
            _highScoreText.text = highScore.ToString();
        }


        public void Settings()
        {
            bool isActive = SettingsMenu.activeSelf;
            SettingsMenu.SetActive(!isActive);

            if (!isPaused)
            {
                Time.timeScale = 0f; // Zaman ölçeðini ayarla
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f; // Zaman ölçeðini normale döndür
                isPaused = false;
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void FinalScene()
        {
            SceneManager.LoadScene(2);
        }
    }
}
