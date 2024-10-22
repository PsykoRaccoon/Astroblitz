using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip soundClip;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = soundClip;

        audioSource.playOnAwake = false;

        GetComponent<Button>().onClick.AddListener(() => audioSource.Play());
    }
}
