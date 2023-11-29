using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Canvas _joystickCanvas;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private SkinShop _skinShop;
    [SerializeField] private ProgressController _progressController;
    [SerializeField] private GameObject _timeOutPanel;
    [SerializeField] private GameObject _completeLevelPanel;
    [SerializeField] private TextMeshProUGUI _coinsTimeOutEarnedText;
    [SerializeField] private TextMeshProUGUI _coinsCompleteLevelEarnedText;
    [SerializeField] private AnyButtonWaiter _anyButtonWaiter;
    [SerializeField] private TutorialController _tutorialController;

    private DataHandler _dataHandler;
    private int _coinsEarned;
    private int _eatableObjectsCount;
    private bool _gameWasPaused = false;
    private bool _gameStarted = false;
    private bool _gameFinished = false;

    public void ToMenu() => SceneManager.LoadScene(1);

    public void ShowFullscreenAd() => YaAdv.ShowFullscreenAdv();
    public void StartGame()
    {
        if (_dataHandler.PlayerData.TutorialPassed == false)
        {
            _tutorialController.TutorialEnd += _tutorialController_TutorialEnd;

            _menuCanvas.gameObject.SetActive(false);
            _joystickCanvas.gameObject.SetActive(true);
            //_timer.StartTimer(_dataHandler.PlayerData.TimeSeconds);
            

            _tutorialController.StartTutorial();
            //_timer.SetPause(true);
        }
        else
        {
            _coinsEarned = 0;
            _timer.StartTimer(_dataHandler.PlayerData.TimeSeconds);
            _menuCanvas.gameObject.SetActive(false);
            _joystickCanvas.gameObject.SetActive(true);
            _progressController.ProgressChanged += GameProgressChanged;
            _playerMovement.enabled = true;
            _gameStarted = true;
        }
    }

    private void _tutorialController_TutorialEnd()
    {
        _dataHandler.TutorialHasPassed();
        _timer.SetPause(false);
        _anyButtonWaiter.gameObject.SetActive(true);

        //StartGame();
    }

    public void GameCompleted()
    {
        _progressController.ProgressChanged -= GameProgressChanged;
        _dataHandler.AddCoins(_coinsEarned);
        _dataHandler.SavePlayerData();
        _completeLevelPanel.SetActive(true);
        _coinsCompleteLevelEarnedText.text = _coinsEarned.ToString();
        _playerMovement.enabled = false;
        _dataHandler.IsReplay = false;
        _gameFinished = true;
        _timer.SetPause(true);
    }

    public void ReplayGame()
    {
        _dataHandler.IsReplay = true;
        YaAdv.FullscreenAdClosed += ReplayFullscreenAdClosed;
        YaAdv.ShowFullscreenAdv();
    }

    private void ReplayFullscreenAdClosed(bool obj)
    {
        YaAdv.FullscreenAdClosed -= ReplayFullscreenAdClosed;
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {        
        _dataHandler = FindObjectOfType<DataHandler>();
        if(_dataHandler.IsReplay)
        {
            _menuCanvas.gameObject.SetActive(false);
            _anyButtonWaiter.gameObject.SetActive(true);            
            _playerMovement.enabled = false;
        }
        InitPlayer();
    }

    private void _anyButtonWaiter_Clicked()
    {
        if (_gameFinished == true) return;
        _playerMovement.enabled = true;
        _anyButtonWaiter.gameObject.SetActive(false);
        if (_gameWasPaused)
        {
            _timer.SetPause(false);
            _gameWasPaused = false;
        }
        else
            StartGame();
    }

    private void OnEnable()
    {
        _timer.TimerComplited += GameTimerComplited;
        _timer.TimerTick += _timer_TimerTick;
        _anyButtonWaiter.Clicked += _anyButtonWaiter_Clicked;

    }

    private void _timer_TimerTick(int obj)
    {
        if (FindAnyObjectByType<Eaten>() == null)
            GameCompleted();
    }

    private void OnDisable()
    {
        _timer.TimerComplited -= GameTimerComplited;
        _anyButtonWaiter.Clicked -= _anyButtonWaiter_Clicked;
        _timer.TimerTick -= _timer_TimerTick;

    }

    private void GameTimerComplited()
    {
        _progressController.ProgressChanged -= GameProgressChanged;
        _dataHandler.AddCoins(_coinsEarned);
        _dataHandler.SavePlayerData();
        _timeOutPanel.SetActive(true);
        _coinsTimeOutEarnedText.text = _coinsEarned.ToString();
        _playerMovement.enabled = false;
        _dataHandler.IsReplay = false;
        _gameFinished = true;
    }

    private void InitPlayer()
    {
        _playerMovement.Speed = _dataHandler.PlayerData.Speed;
        _skinShop.SetSkin(_dataHandler.PlayerData.ActiveSkinID);//use it for set skin from load data
    }
    
    private void GameProgressChanged(float obj)
    {
        ++_coinsEarned;
    }


    private void OnApplicationFocus(bool focus)
    {
        //This is necessary because the timer does not stop when switching tabs
        if (_gameWasPaused == false && _gameFinished == false)
        {
            _timer.SetPause(!focus);
            if (_timer.IsPause() && _gameStarted)
            {
                _anyButtonWaiter.gameObject.SetActive(true);
                _gameWasPaused = true;
            }
        }
    }

}
