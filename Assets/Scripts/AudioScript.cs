using UnityEngine;

public class AudioScript : MonoBehaviour
{

    [SerializeField] private AudioClip pageTurn;
    [SerializeField] private AudioClip write;
    [SerializeField] private AudioClip walk;
    
    [SerializeField] private AudioSource source;

    public void PlayTurn()
    {
        source.clip = pageTurn;
        source.Play();
    }

    public void PlayWrite()
    {
        source.clip = write;
        source.Play();
    }

    public void PlayWalk()
    {
        source.clip = walk;
        source.Play();
    }

}
