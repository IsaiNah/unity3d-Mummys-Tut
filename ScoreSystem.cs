using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
   
    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highscoreText;
    [SerializeField] private TMP_Text _multiplerText;
    [SerializeField] private FloatScoreText _floatingScoreText;
    [SerializeField] private Canvas _floatingScoreCanvas;
    private int _score;

    private int _highscore;
    private float _scoreMultiplierExpiration;
    private int _killMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        MummyController.OnMechDestroyed += MechDestroyed;
        _highscore = PlayerPrefs.GetInt("HighScore");
        _highscoreText.SetText("High Score: " + _highscore);
    }

    private void OnDestroy()
    {
        MummyController.OnMechDestroyed -= MechDestroyed; // Unregister event to prevent error
    }

    private void MechDestroyed(MummyController mech)
    {
       
        
        UpdateKillMultipler();

        _score += _killMultiplier;
        
        _score++;
        if (_score > _highscore)
        {
            _highscore = _score;
            _highscoreText.SetText("High Score: " + _highscore);
            PlayerPrefs.SetInt("HighScore", _highscore);
        }
        

        _scoreText.SetText(_score.ToString());

        var floatingText = Instantiate(_floatingScoreText, mech.transform.position, _floatingScoreCanvas.transform.rotation,
            _floatingScoreCanvas.transform);
        
        floatingText.SetScoreValue(_killMultiplier);
    }

    private void UpdateKillMultipler()
    {
        if (Time.time <= _scoreMultiplierExpiration)
        {
            _killMultiplier++;
            _score += _killMultiplier;
        }
        else
        {
            _killMultiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 1f;
        
       // _multiplerText.SetText("x " + _killMultiplier);
        
    
        
    }
}


