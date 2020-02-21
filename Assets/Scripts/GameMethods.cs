using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMethods : MonoBehaviour
{
    // Returns child of given gameObject with a specific given tag
    public GameObject FindChildTagged(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform) // search through all children for the target tag
        {
            if (child.tag.ToLower() == tag.ToLower())
            {
                return child.gameObject;
            }
        }

        return null; // if no child with tag found return null
    }

    // Returns child transfrom with tag containing a given string
    public GameObject FindChildPartTagged(GameObject parent, string partialTag)
    {
        foreach (Transform child in parent.transform) // search through all children for the target tag
        {
           
            if (child.tag.ToLower().Contains(partialTag.ToLower()))
            {
                return child.gameObject;
            }
        }

        return null; // if no child with tag found return null
    }

    // Returns children transforms with tags containing a given string
    public List<GameObject> FindChildrenPartTagged(GameObject parent, string partialTag)
    {
        List<GameObject> foundChildren = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            if (child.tag.ToLower().Contains(partialTag.ToLower()))
            {
                foundChildren.Add(child.gameObject);
            }
        }
        return foundChildren;
    }

    
}
