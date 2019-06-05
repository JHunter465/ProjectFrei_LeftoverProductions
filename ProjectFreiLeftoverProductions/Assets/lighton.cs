using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighton : MonoBehaviour
{

    private bool uit = true;
    // Start is called before the first frame update
    public void lampaan()
    {
        if (uit)
        {
            GetComponent<Light>().enabled = true;
            uit = false;
        }

        if (!uit)
        {
            GetComponent<Light>().enabled = false;
            uit = true;
        }
    }
}
