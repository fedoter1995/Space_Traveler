using UnityEngine;

public class StarshipSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindWithTag("Sounds").GetComponent<AudioSource>();
    }
    public void ShootSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
