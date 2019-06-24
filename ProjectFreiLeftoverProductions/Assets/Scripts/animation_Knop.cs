using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_Knop : MonoBehaviour
{
    [SerializeField] private GameObject Animatable = null;
    [SerializeField] private GameObject HideContainer = null;
    private Container m_container = null;
    private bool pressed = false;
    private Animation m_animation;
    private Valve.VR.InteractionSystem.HoverButton hoverButton;

    private void Start()
    {
        try
        {
            m_container = HideContainer.GetComponent<Container>();
        }
        catch
        {
            Debug.Log("No Container Found");
        }
        m_animation = Animatable.GetComponent<Animation>();
        hoverButton = GetComponent<Valve.VR.InteractionSystem.HoverButton>();
    }

    private void Update()
    {
        if (hoverButton.engaged)
        {
            buttonPressed();
        }
    }
    public void buttonPressed()
    {
        StartCoroutine(startAnimation());
    }
    IEnumerator startAnimation()
    {
        if (!pressed)
        {
            m_animation.Play("1st");
            //m_container.Open = true;
            yield return new WaitForSeconds(2f);
            pressed = true;
        }
        else
        {
            m_animation.Play("2nd");
            yield return new WaitForSeconds(2f);
            pressed = false;
        }
        yield return null;
    }
}
