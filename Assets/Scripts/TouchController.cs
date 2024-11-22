using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private PlayerModel playerOne;
    [SerializeField] private PlayerModel playerTwo;
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private TextMeshProUGUI testText2;
    [SerializeField] private GrayscaleController grayscaleController;
    [SerializeField] private EndGameUIController endGameUiController;
    private bool isGameStarted = false;
    private bool isPlayerTap = false;
    private bool isLeftPlayerTap = false;
    private bool isRightPlayerTap = false;
    private bool isWinnerShowed = false;
    private int touchIdOne = -1;
    private int touchIdTwo = -2;
    public DateTime playerOneDateTime;
    public DateTime playerTwoDateTime;
    public DateTime startDateTime;

    private void Start()
    {
        playerTwo.endAttackAnimationEvent += () =>
        {
            playerOne.SetAnimState("Hit", true);
        };
        
        playerOne.endAttackAnimationEvent += () =>
        {
            playerTwo.SetAnimState("Hit", true);
        };
    }

    public void StartGame()
    {
        isGameStarted = true;
        startDateTime = DateTime.Now;
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
    }

    private void CheckWinner()
    {
        if (isWinnerShowed)
        {
            return;
        }
        
        var milliseconds = playerOneDateTime.Subtract(playerTwoDateTime).TotalMilliseconds;
        if (milliseconds > 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerOne";
            
            endGameUiController.ShowWinner(true);
            isWinnerShowed = true;
        }
        else if(milliseconds < 0)
        {
            testText2.text = "Время: " + milliseconds + "  \\ Выйграл PlayerTwo";
            
            endGameUiController.ShowWinner(false);
            isWinnerShowed = true;
        }
    }

    private void CheckCachedTouch(Touch[] touches)
    {
#if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Tab))
        {
                //testText.text = "POS < 960 PlayerOne: " + mousePositionX;
                playerOneDateTime = DateTime.Now;;
                playerOne.SetAnimState("Attack", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
                //testText.text = "POS > 960 PlayerTwo: " + mousePositionX;
                playerTwoDateTime = DateTime.Now;;
                playerTwo.SetAnimState("Attack", true);
        }
#endif
        if (touches.Length > 0)
        {
            foreach (var touch in touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < 960 && touchIdOne == -1 && isLeftPlayerTap == false)
                    {
                        isLeftPlayerTap = true;

                        //testText.text = "POS < 960 PlayerOne: " + touch.position;
                        touchIdOne = touch.fingerId;
                        playerOneDateTime = DateTime.Now;
                        if (isRightPlayerTap == false)
                        {
                            playerOne.SetAnimState("Attack", true);
                        }
                        continue;
                    }

                    if (touch.position.x > 960 && touchIdTwo == -2 && isRightPlayerTap == false)
                    {
                        isRightPlayerTap = true;
                        
                        //testText2.text = "POS > 960 PlayerTwo: " + touch.position;
                        touchIdTwo = touch.fingerId;
                        playerTwoDateTime = DateTime.Now;
                        if (isLeftPlayerTap == false)
                        {
                            playerTwo.SetAnimState("Attack", true);
                        }
                        continue;
                    }
                }
            }
        }
    }
}