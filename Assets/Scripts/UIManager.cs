using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game UI Manager.
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Game Scene Canvas
/// 
/// This class manages the user interaction with the game features (not game play) such 
/// as exiting, restarting, etc.
/// </remarks>
/// 

public class UIManager : MonoBehaviour
{

    /// <summary>
    /// The following variables are populated in the Inspector.
    /// </summary>
    [SerializeField] private Image      _playerLivesImage;
    [SerializeField] private Sprite[]   _playerLivesSprite;
    [SerializeField] private Text       _scoreText;
    [SerializeField] private Text       _ammoText;
    [SerializeField] private Text       _gameOverText;
    [SerializeField] private Text       _resetLevelText;

    /// <summary>
    /// Private Variables
    /// </summary>
    private bool _gameOver  = false;
    private bool _outOfAmmo = false;

    /// <summary>
    /// Initialize all game objects here, rather than above so they are reset
    /// on game re-start.
    /// </summary>
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _resetLevelText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _gameOver  = false;
        _outOfAmmo = false;
    }

    /// <summary>
    /// Sit and wait for the 'R' (restart) 'Escape' (exit) keys.
    /// </summary>
    /// 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            Debug.Log("Reset the game...");
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quiting Game");
        }
    }

    /// <summary>
    /// Update score UI element by passing in new score.
    /// 
    /// <example>
    ///    <code>
    ///    private UIManager _uiManager;
    ///    _uiManager = ("Canvas").GetComponent&lt;UIManager&gt;();
    ///    _uiManager.UpdateScore(10);
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <param name="score">Current Score</param>
    /// 
    /// <remarks>
    /// This method does not manage or track the score.  It simple displays what is passed to it.
    /// </remarks>
    /// 

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Update player health (lives) graphic
    /// 
    /// <example>
    ///    <code>
    ///    private UIManager _uiManager;
    ///    _uiManager = ("Canvas").GetComponent&lt;UIManager&gt;();
    ///    _uiManager.UpdateLives(_lives);
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <param name="lives">Number of remaining lives</param>
    /// 
    /// <remarks>
    /// This method only updates the health graphic.  It does not maintain
    /// the actual count of remaining lives.
    /// </remarks>
    /// 

    public void UpdateLives(int lives)
    {
        if (lives < 0) { lives = 0; };
        // lives = (lives < 0) ? 0 : lives; 
        _playerLivesImage.sprite = _playerLivesSprite[lives];

    }

    /// <summary>
    /// Manage ammo count display
    /// </summary>
    /// 
    /// <param name="ammo">Current ammo count</param>
    /// 
    /// <remarks>
    /// </remarks>
    /// 

    public void updateAmmo(int ammo)
    {
        _ammoText.text = "Ammo: " + ammo;
        _outOfAmmo = (ammo > 0) ? false : true;
        if (_outOfAmmo) { this.DisplayOutOfAmmo();  }
    }

    /// <summary>
    /// Handle received Player Dead message from Player
    /// </summary>
    /// 
    /// <remarks><
    /// Perform all end of game steps when the Player informs the UI Manager it has died.
    /// </remarks>
    /// 

    public void PlayerDead()
    {
        this.DisplayGameOver();
    }

    /// <summary>
    /// Display end of game graphics
    /// 
    /// <example>
    ///    <code>
    ///    private UIManager _uiManager;
    ///    _uiManager = ("Canvas").GetComponent&lt;UIManager&gt;();
    ///    _uiManager.DisplayGameOver();
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Enable the various End of Game graphics and instructions.
    /// </remarks>
    /// 

    public void DisplayGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _resetLevelText.gameObject.SetActive(true);
        _gameOver = true;
        StartCoroutine(FlickerGameOver());
    }

    /// <summary>
    /// Display out of ammo graphics
    /// 
    /// <example>
    ///    <code>
    ///    private UIManager _uiManager;
    ///    _uiManager = ("Canvas").GetComponent&lt;UIManager&gt;();
    ///    _uiManager.DisplayOutOfAmmo();
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Enable the various End of Game graphics and instructions.
    /// </remarks>
    /// 

    public void DisplayOutOfAmmo()
    {
        _outOfAmmo = true;
        StartCoroutine(FlickerOutOfAmmo());
    }

    /// <summary>
    /// IEnumerator: Make the Game Over text flicker
    /// 
    /// <example>
    ///    <code>
    ///    this.StartCoroutine(FlickerGameOver());
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <returns>WaitForSeconds(0.1f)</returns>
    /// 
    /// <remarks>
    /// Cause 'Game Over' text to flicker switching object between active and inactive.
    /// </remarks>
    /// 

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

    /// <summary>
    /// IEnumerator: Make the ammo count text flicker ammo count equals zero
    /// 
    /// <example>
    ///    <code>
    ///    this.StartCoroutine(FlickerOutOfAmmo());
    ///    </code>
    /// </example>
    /// 
    /// </summary>
    /// 
    /// <returns>WaitForSeconds(0.1f)</returns>
    /// 
    /// <remarks>
    /// Cause 'Ammo' text to flicker switching object between active and inactive.
    /// </remarks>
    /// 

    IEnumerator FlickerOutOfAmmo()
    {
        while (_outOfAmmo)
        {
            yield return new WaitForSeconds(0.1f);
            _ammoText.color = Color.red;
            bool flash;
            flash = _ammoText.gameObject.activeSelf ? false : true;
            _ammoText.gameObject.SetActive(flash);
        }
        _ammoText.color = Color.white;
    }
}
