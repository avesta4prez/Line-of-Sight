using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private PlayerTurn playerOne;
    [SerializeField] private PlayerTurn playerTwo;
    [SerializeField] private float timeBetweenTurns;

    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;

    [SerializeField] private int currentPlayerIndex;
    private bool waitingForNextTurn;
    public float timeForNextTurn;
    private float turnDelay;

    public List<Text> timeTexts;
    
    private void Awake()
    {
        cam1.SetActive(true);
        if (instance == null)
        {
            instance = this;
            currentPlayerIndex = 1;
            playerOne.SetPlayerTurn(1);
            playerTwo.SetPlayerTurn(2);
            cam1.SetActive(true);
            cam2.SetActive(false);
        }

    }

    private void Start()
    {
        turnDelay = timeForNextTurn;
    }

    private void Update()
    {
        if (waitingForNextTurn)
        {
            turnDelay -= Time.deltaTime;
            if (turnDelay <= 0)
            {
                turnDelay = timeForNextTurn;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }
        else
        {
            turnDelay -= Time.deltaTime;
            if (turnDelay <= 0)
            {

                TriggerChangeTurn();
            }
            else 
            {
                DisplayTime(turnDelay);
            }
        }

        
    }

    public bool IsItPlayerTurn(int index)
    {
        if (waitingForNextTurn)
        {
            return false;
        }

        return index == currentPlayerIndex;
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public void TriggerChangeTurn()
    {
        waitingForNextTurn = true;
        turnDelay = timeBetweenTurns;
    }

    private void ChangeTurn()
    {
        if (currentPlayerIndex == 1)
        {
            currentPlayerIndex = 2;
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
        else if (currentPlayerIndex == 2)
        {
            currentPlayerIndex = 1;
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeTexts[currentPlayerIndex - 1].text = string.Format("{0:0}", seconds);
    }
}