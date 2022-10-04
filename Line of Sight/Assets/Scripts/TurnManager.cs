using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (waitingForNextTurn)
        {
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns)
            {
                turnDelay = 0;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }
        else
        {
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeForNextTurn)
            {
                turnDelay = 0;
                
                TriggerChangeTurn();
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
        turnDelay = 0;
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
}