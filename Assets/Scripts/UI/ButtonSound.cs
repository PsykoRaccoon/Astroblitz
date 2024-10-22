using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip soundClip;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource.clip = soundClip;

        audioSource.playOnAwake = false;

        GetComponent<Button>().onClick.AddListener(() => audioSource.Play());
    }
}
