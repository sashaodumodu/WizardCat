using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WizardCat wizardCat;
    [SerializeField] private EnemyAI enemyAI;

    private void Start()
    {
        if (wizardCat == null || enemyAI == null)
        {
            Debug.LogError("Assign WizardCat and EnemyAI in the Inspector.");
            return;
        }

        Debug.Log("Game started.");

        wizardCat.Appear();
        wizardCat.BackgroundMusic();
        wizardCat.Movement();
        wizardCat.Talk();
        wizardCat.Heal();
        wizardCat.HealSound();

        enemyAI.Appear();
        enemyAI.EnemyMusic();
        enemyAI.Movement();
        enemyAI.Attack();
        enemyAI.AttackSound();
        enemyAI.Emotions();
    }
}