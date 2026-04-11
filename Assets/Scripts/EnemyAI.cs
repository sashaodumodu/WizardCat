using UnityEngine;

public class EnemyAI : MonoBehaviour, IInteraction, IAudio
{
    // Interaction methods
    public void Dialogue()
    {
        Debug.Log("Enemy is having a conversation.");
    }

    public void Emotions()
    {
        Debug.Log("Enemy AI is experiencing an emotion.");
    }

    public void Fight()
    {
        Debug.Log("A fight is taking place.");
    }

    public void Talk()
    {
        Debug.Log("There is dialogue taking place.");
    }

    public void Disappear()
    {
        Debug.Log("Character has disappeared.");
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        Debug.Log("Character has appeared.");
    }

    public void Kill()
    {
        Debug.Log("Enemy killed character.");
    }

    public void Heal()
    {
        Debug.Log("Enemy used healing.");
    }

    public void InteractWithEnvironment()
    {
        Debug.Log("Character is engaged with environment.");
    }

    public void Movement()
    {
        Debug.Log("Character is moving about.");
    }

    public void Attack()
    {
        Debug.Log("Character is attacking enemy.");
    }

    public void Attitude()
    {
        Debug.Log("Character is not having it.");
    }

    // Extra enemy-only methods
    public void Steal()
    {
        Debug.Log("Enemy has stolen.");
    }

    public void DestroyEnvironment()
    {
        Debug.Log("Enemy has destroyed environment.");
    }

    public void Strength()
    {
        Debug.Log("Enemy has increased strength.");
    }

    public void Deception()
    {
        Debug.Log("Enemy has lied to character.");
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