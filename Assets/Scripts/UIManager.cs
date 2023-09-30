using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _resetLevelText;
    [SerializeField] private Image _playerLivesImage;
    [SerializeField] private Sprite[] _playerLivesSprite;

    private bool _gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _resetLevelText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            Debug.Log("Reset the game...");
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        _playerLivesImage.sprite = _playerLivesSprite[lives];

    }

    public void DisplayGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _resetLevelText.gameObject.SetActive(true);
        _gameOver = true;
        StartCoroutine(FlickerGameOver());
    }

    IEnumerator FlickerGameOver()
    {
       while (_gameOver)
        {
            yield return new WaitForSeconds(0.1f);
            bool flash;
            flash = _gameOverText.gameObject.activeSelf ? false : true;
            _gameOverText.gameObject.SetActive(flash);
        }
    }
}
