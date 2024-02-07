using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AbilityType
{
    None,
    Freeze,
    Invincibility,
    Push
}

public class PlayerController : MonoBehaviour
{
    public Button freezeButton;
    public Button invincibilityButton;
    public Button pushButton;

    private AbilityType currentAbility = AbilityType.None;

    private List<AbilityType> availableAbilities = new List<AbilityType>();
    private float abilityCooldown = 5f; // Universal cooldown for all abilities
    private float cooldownTimer = 0f;

    public bool isInvincible = false;

    //private List<NPCMovement> npcMovements= new List<NPCMovement>();
    public List<NPCMovement> npcMovements = new List<NPCMovement>();

    public int maxHealth = 3;
    private int currentHealth;

    public NPCSpawner npcSpawner;
    public NPCMovement npcMovement;
    public Health healthBar;
    public GameManager gameManager;
    public TimerController timerController;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.health = currentHealth;
        healthBar.numOfHeart = maxHealth;

        // Initialize available abilities
        availableAbilities.Add(AbilityType.Freeze);
        availableAbilities.Add(AbilityType.Invincibility);
        availableAbilities.Add(AbilityType.Push);

        // Find and store references to NPCMovement scripts
        //NPCMovement[] npcs = FindObjectsOfType<NPCMovement>();
        //npcMovements.AddRange(npcs);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameState.Playing)
        {
            // Your normal update logic here

            //UpdateAbilityButtons();
            //HandleAbilityInput();
            //UpdateCooldown();
        }
    }
    private void HandleAbilityInput()
    {
        // Check for ability button press
        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            ActivateRandomAbility();
            cooldownTimer = abilityCooldown;
        }
    }

    private void UpdateCooldown()
    {
        // Update cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void ActivateRandomAbility()
    {
        if (availableAbilities.Count > 0)
        {
            int randomIndex = Random.Range(0, availableAbilities.Count);
            AbilityType selectedAbility = availableAbilities[randomIndex];
            // Remove the selected ability from the available list
            availableAbilities.RemoveAt(randomIndex);
            StartCoroutine(UseAbility());
        }
    }
    
    public void NPCsAvailable()
    {
        NPCMovement[] npcs = FindObjectsOfType<NPCMovement>();
        npcMovements.AddRange(npcs);
        Debug.Log("NPC's Found");
    }

    private IEnumerator UseAbility()
    {
        switch (currentAbility)
        {
            case AbilityType.Freeze:
                // Implement freeze ability logic
                Debug.Log("Freeze Used");
                foreach (GameObject npc in GameObject.FindGameObjectsWithTag("Opponent"))
                {
                    NPCController npcController = npc.GetComponent<NPCController>();
                    if (npcController != null)
                    {
                        npcController.Freeze();
                    }

                    NPCMovement npcMovement = npc.GetComponent<NPCMovement>();
                    if (npcMovement != null)
                    {
                        npcMovement.Freeze();
                    }
                }
                break;

            case AbilityType.Invincibility:
                // Implement invincibility ability logic
                Debug.Log("Invincibility Used");
                isInvincible = true;
                yield return new WaitForSeconds(2f);
                isInvincible = false;
                break;

            case AbilityType.Push:
                // Implement push ability logic
                Debug.Log("Push Used");
                PushNPCs();
                break;
        }

        yield return new WaitForSeconds(abilityCooldown);
        availableAbilities.Add(currentAbility);
        currentAbility = AbilityType.None;
    }

    private void UpdateAbilityButtons()
    {
        // Set all buttons inactive first
        freezeButton.gameObject.SetActive(false);
        invincibilityButton.gameObject.SetActive(false);
        pushButton.gameObject.SetActive(false);

        // Check the currentAbility and activate the corresponding button
        if (currentAbility == AbilityType.Freeze)
        {
            freezeButton.gameObject.SetActive(true);
        }
        else if (currentAbility == AbilityType.Invincibility)
        {
            invincibilityButton.gameObject.SetActive(true);
        }
        else if (currentAbility == AbilityType.Push)
        {
            pushButton.gameObject.SetActive(true);
        }

        // If an ability is in cooldown, deactivate all buttons
        if (cooldownTimer > 0f)
        {
            freezeButton.gameObject.SetActive(false);
            invincibilityButton.gameObject.SetActive(false);
            pushButton.gameObject.SetActive(false);
        }
    }
    //private void UpdateAbilityButtons()
    //{
    //    // Check if the currentAbility matches each ability and set its interactable property
    //    Debug.Log("button Update");

    //    freezeButton.gameObject.SetActive(currentAbility == AbilityType.None || currentAbility == AbilityType.Freeze);
    //    invincibilityButton.gameObject.SetActive(currentAbility == AbilityType.None || currentAbility == AbilityType.Invincibility);
    //    pushButton.gameObject.SetActive(currentAbility == AbilityType.None || currentAbility == AbilityType.Push);

    //    // If an ability is in cooldown, deactivate all buttons
    //    if (cooldownTimer > 0f)
    //    {
    //        freezeButton.gameObject.SetActive(false);
    //        invincibilityButton.gameObject.SetActive(false);
    //        pushButton.gameObject.SetActive(false);
    //    }
    //}

    public void PushNPCs()
    {
        Debug.Log("button pressed");
        foreach(NPCMovement npc in npcMovements)
        {
            Debug.Log("NPC's Pushed");
            Vector3 pushDirection = (npc.transform.position - transform.position).normalized;
            npc.PushAway(pushDirection);
        }
    }
    //---------------------------------------

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.health = currentHealth;
    }

    public void CheckWinLoseConditions()
    {
        float timeRemaining = timerController.timeRemaining;

        if (currentHealth <= 0)
        {
            //Debug.Log("player loses");
            gameManager.gameState = GameState.Lost; // Change game state to lose
            gameManager.ShowLosePanel();
        }
        else if (timeRemaining <= 0)
        {
            //Debug.Log("player win");
            gameManager.gameState = GameState.Won; // Change game state to won
            gameManager.ShowWinPanel();
            gameManager.UnlockNewLevel();
        }
    }
}
