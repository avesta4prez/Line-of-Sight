using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject playerWin;
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private Image healthBar;
    //[SerializeField] private float health;
    private float currentHealth;
    private bool isColliding;
    private Vector3 initialPosition;
    private Vector3 initialRotation;


    void Start()
    {
        currentHealth = maxHealth;
        // Saving the Start Position of the Players
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
        healthBar.fillAmount = 1f;
        playerWin.SetActive(false);
    }



    public void TakeDamage(float damage)
    {

        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0f)
        {
            // Make the Player with no health disappear
            gameObject.SetActive(false);

            // Displays PlayerOne/Two wins!
            playerWin.SetActive(true);

            // Set the Player with no health back to initial position
            //transform.position = initialPosition;
            //transform.eulerAngles = initialRotation;
        }
    }
}
