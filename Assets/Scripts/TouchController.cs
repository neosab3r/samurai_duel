using System;
using TMPro;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Samsung.Android;

public class TouchController : MonoBehaviour
{
    [SerializeField] private PlayerModel playerOne;
    [SerializeField] private PlayerModel playerTwo;
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private TextMeshProUGUI testText2;
    [SerializeField] private GameController gameController;
    private bool isGameStarted = false;
    private bool isPlayerTap = false;
    private int touchIdOne = -1;
    private int touchIdTwo = -2;
    private DateTime playerOneDateTime;
    private DateTime playerTwoDateTime;
    private DateTime startDateTime;

    private void Start()
    {
        startDateTime = DateTime.Now;
        playerOneDateTime = startDateTime;
        playerTwoDateTime = startDateTime;
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    private void Update()
    {
        if (isGameStarted == false)
        {
            return;
        }
        
        var touches = Input.touches;

        CheckCachedTouch(touches);

        CheckWinner();
        //MovePlayerModel(touches);
    }

    private void CheckWinner()
    {
        var milliseconds = playerOneDateTime.Subtract(playerTwoDateTime).TotalMilliseconds;
        if (milliseconds > 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerOne";
            gameController.ShowWinner(true);
        }
        else if(milliseconds < 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerTwo";
            gameController.ShowWinner(false);
        }
    }

    private void MovePlayerModel(Touch[] touches)
    {
        foreach (var touch in touches)
        {
            if (touch.fingerId == touchIdOne)
            {
                //playerOne.UpdatePosition(touch.position);
            }

            if (touch.fingerId == touchIdTwo)
            {
                //playerTwo.UpdatePosition(touch.position);
            }
        }
#if UNITY_EDITOR_WIN
        //playerOne.UpdatePosition(Input.mousePosition);
        //playerTwo.UpdatePosition(Input.mousePosition);
#endif
    }

    private void CheckCachedTouch(Touch[] touches)
    {
        if (isPlayerTap == true)
        {
            return;
        }
        if (touches.Length > 0)
        {
            foreach (var touch in touches)
            {
                Debug.Log("POS: " + touch.position);
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < 960 && touchIdOne == -1)
                    {
                        isPlayerTap = true;
                        testText.text = "POS < 960 PlayerOne: " + touch.position;
                        touchIdOne = touch.fingerId;
                        playerOneDateTime = DateTime.Now;;
                        continue;
                    }

                    if (touch.position.x > 960 && touchIdTwo == -2)
                    {
                        isPlayerTap = true;
                        testText.text = "POS > 960 PlayerTwo: " + touch.position;
                        touchIdTwo = touch.fingerId;
                        playerTwoDateTime = DateTime.Now;;
                        continue;
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (touch.fingerId == touchIdOne)
                    {
                        touchIdOne = -1;
                        continue;
                    }

                    if (touch.fingerId == touchIdTwo)
                    {
                        touchIdTwo = -2;
                        continue;
                    }
                }
            }
        }
    }
}