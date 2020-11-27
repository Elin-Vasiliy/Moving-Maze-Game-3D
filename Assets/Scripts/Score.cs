using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] private Text _scoreTxtGame;
    [SerializeField] private Text _scoreTxtDie;
    [SerializeField] private PlayerController _player;
    [SerializeField] private MoveCam _cam;

    private float t = 0;
    private int score;
    private int Gold;

    private void OnEnable()
    {
        _player.DiePlayer += PlustGold;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            if (t < 0.5f)
            {
                t += Time.deltaTime;
            }
            else
            {
                score++;
                _scoreTxtGame.text = $"{score}";
                _scoreTxtDie.text = $"Score: {score}";
                t = 0;
                if (score%20 ==0 && _player.Speed < 15)
                {
                    _player.Speed += 0.5f;
                    _cam.Speed += 0.005f;
                }
            }
        }
    }

    private void PlustGold()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        Gold += score;
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.Save();
    }
}
