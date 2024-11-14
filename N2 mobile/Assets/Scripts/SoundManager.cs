using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clipList;

    public enum SoundType
    {
        TypeVictory,
        TypeBTN,
        TypeCollect
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType clipType)
    {
        audioSource.PlayOneShot(clipList[(int)clipType]);
    }
}
