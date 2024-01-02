using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SoldierIconCreator : MonoBehaviour
{
    private GameObject soldierPrefab; // Reference to the soldier prefab
    public GameObject soldierIconObject; // Reference to the GameObject to hold the soldier icon

    public void GenerateIcon()
    {
        soldierPrefab = gameObject.GetComponent<SoldierContainerManager>().SoldierContained;

        // Instantiate the soldier prefab
        GameObject newSoldierIcon = Instantiate(soldierPrefab, soldierIconObject.transform.position, Quaternion.identity);
        
        // Set the parent of the instantiated soldier to the soldier icon object
        newSoldierIcon.transform.SetParent(soldierIconObject.transform);

        // Optionally, you can adjust the scale and position of the new soldier icon
        // newSoldierIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        // newSoldierIcon.transform.localPosition = Vector3.zero;

        // Get all components attached to the newly instantiated object
        Component[] components = newSoldierIcon.GetComponents<Component>();

        // Loop through each component and remove unnecessary ones (keeping SpriteRenderer and Rigidbody)
        foreach (Component comp in components)
        {
            if (!(comp is SpriteLibrary) && !(comp is Rigidbody2D))
            {
                Destroy(comp);
            }
        }
    }
}
