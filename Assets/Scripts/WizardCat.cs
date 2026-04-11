using UnityEngine;

public class WizardCat : MonoBehaviour, IInteraction, IAudio
{
    // Interaction methods
    public void Dialogue()
    {
        Debug.Log("Wizard Cat is having a conversation.");
    }

    public void Emotions()
    {
        Debug.Log("Wizard Cat is feeling emotional.");
    }

    public void Fight()
    {
        Debug.Log("Wizard Cat is fighting with the enemy.");
    }

    public void Talk()
    {
        Debug.Log("Wizard Cat is talking.");
    }

    public void Disappear()
    {
        Debug.Log("Wizard Cat disappears.");
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        Debug.Log("Wizard Cat appears.");
    }

    public void Kill()
    {
        Debug.Log("Wizard Cat defeated the enemy.");
    }

    public void Heal()
    {
        Debug.Log("Wizard Cat is healing a character.");
    }

    public void InteractWithEnvironment()
    {
        Debug.Log("Wizard Cat picked a fruit.");
    }

    public void Movement()
    {
        Debug.Log("Wizard Cat is leaving the house.");
    }

    public void Attack()
    {
        Debug.Log("Wizard Cat attacks the enemy.");
    }

    public void Attitude()
    {
        Debug.Log("Wizard Cat is not having it.");
    }

    // Audio methods
    public void BackgroundMusic()
    {
        Debug.Log("Happy music for the background.");
    }

    public void CharacterDeath()
    {
        Debug.Log("Sad, eerie music for background.");
    }

    public void EnemyMusic()
    {
        Debug.Log("Evil music for background.");
    }

    public void MixingPotions()
    {
        Debug.Log("Light, chaotic music for background.");
    }

    public void ObjectsMusic()
    {
        Debug.Log("Objects for music.");
    }

    public void EmotionSound()
    {
        Debug.Log("Sounds for every emotion.");
    }

    public void FightSound()
    {
        Debug.Log("Tense music for fights.");
    }

    public void TalkSound()
    {
        Debug.Log("Sounds to mimic voices.");
    }

    public void HealSound()
    {
        Debug.Log("Earthy sounds for healing.");
    }

    public void MovementSound()
    {
        Debug.Log("Sounds for movement like shuffling.");
    }

    public void AttackSound()
    {
        Debug.Log("Sounds for aggressive fight.");
    }
}