using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horn : MonoBehaviour
{
    [SerializeField] private AudioClip Toeter = null;
    void Update()
    {
        if (GetComponent<Valve.VR.InteractionSystem.HoverButton>().engaged)
        {
            GetComponent<AudioSource>().PlayOneShot(Toeter);

        }       
    }
}
