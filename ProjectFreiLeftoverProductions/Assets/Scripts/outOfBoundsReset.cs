﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBoundsReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableItem>())
        {
            other.GetComponent<InteractableItem>().MoveToStartPos();
        }
    }
}
