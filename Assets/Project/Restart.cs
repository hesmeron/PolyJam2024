using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] 
    private PlayerInput _input;
    [SerializeField] private Image _image;

    private float _timePassed = 0f;
    private float _timeToPass = 3f;

    void Start()
    {
       ;
    }
    void Update()
    {
        if (_input.actions["BButton"].IsPressed())
        {
            _timePassed += Time.deltaTime;
            _image.fillAmount = _timePassed / _timeToPass;
            if (_timePassed / _timeToPass >= 1f)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
