using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIconCreator : MonoBehaviour
{
    public GameObject soldierIconObject; // Reference to the GameObject to hold soldier parts

    public void generateIcon()
    {
        Transform soldierContained = gameObject.GetComponent<SoldierContainerManager>().SoldierContained.transform;

        // Loop through each child of SoldierContained
        foreach (Transform child in soldierContained)
        {
            SpriteRenderer originalRenderer = child.GetComponent<SpriteRenderer>();

            // Check if the child has a SpriteRenderer component
            if (originalRenderer != null)
            {
                // Create a new GameObject for each child part
                GameObject newPartObject = new GameObject();
                newPartObject.transform.parent = soldierIconObject.transform;

                // Add a SpriteRenderer component to the new GameObject and set its sprite
                SpriteRenderer newRenderer = newPartObject.AddComponent<SpriteRenderer>();
                newRenderer.sprite = originalRenderer.sprite;

                // Set the position to match the original child's position
                newPartObject.transform.position = child.position;

                // Set the other properties as needed
                // newRenderer.sortingOrder = originalRenderer.sortingOrder;
                // newRenderer.color = originalRenderer.color;
                // ...
            }
        }
    }
}
