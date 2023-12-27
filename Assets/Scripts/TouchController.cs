using System;
using TMPro;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Samsung.Android;
using UnityEngine.Serialization;

public class TouchController : MonoBehaviour
{
    [SerializeField] private PlayerModel playerOne;
    [SerializeField] private PlayerModel playerTwo;
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private TextMeshProUGUI testText2;
    [SerializeField] private GrayscaleController grayscaleController;
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
        if (isGameStarted == false) {
            return;
        }
        
        var touches = Input.touches;

        CheckCachedTouch(touches);

        CheckWinner();
    }

    private void CheckWinner()
    {
        var milliseconds = playerOneDateTime.Subtract(playerTwoDateTime).TotalMilliseconds;
        if (milliseconds > 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerOne";
            
            playerOne.endAttackAnimationEvent += () =>
            {
                playerTwo.SetAnimState("Hit", true);
            };
        }
        else if(milliseconds < 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerTwo";
            playerTwo.endAttackAnimationEvent += () =>
            {
                playerOne.SetAnimState("Hit", true);
            };
        }
    }

    private void CheckCachedTouch(Touch[] touches)
    {
        if (isPlayerTap == true)
        {
            return;
        }
        
#if UNITY_EDITOR_WIN
        if (Input.GetMouseButtonDown(0))
        {
            var mousePositionX = Input.mousePosition.x;
            if (mousePositionX < 960)
            {
                isPlayerTap = true;
                testText.text = "POS < 960 PlayerOne: " + mousePositionX;
                playerOneDateTime = DateTime.Now;;
                playerOne.SetAnimState("Attack", true);
            }

            if (mousePositionX > 960)
            {
                isPlayerTap = true;
                testText.text = "POS > 960 PlayerTwo: " + mousePositionX;
                playerTwoDateTime = DateTime.Now;;
                playerTwo.SetAnimState("Attack", true);
            }
        }
#endif
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
                        playerOne.SetAnimState("Attack", true);
                        continue;
                    }

                    if (touch.position.x > 960 && touchIdTwo == -2)
                    {
                        isPlayerTap = true;
                        testText.text = "POS > 960 PlayerTwo: " + touch.position;
                        touchIdTwo = touch.fingerId;
                        playerTwoDateTime = DateTime.Now;;
                        playerTwo.SetAnimState("Attack", true);
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