using System.Collections.Generic;
using UnityEngine;

// TODO: Should this be a scriptable object?
public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public float dropRate;
        public GameObject itemPrefFab;
    }


    public List<Drops> drops;

    

    // OnDestroy method called when the object is destroyed.
    // Determines a random drop rate for each item in the list.
    private void OnDestroy()
    {
        float dropChance = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        // Get the drops that have a drop rate less than or equal to the drop chance.
        foreach (Drops drop in drops)
        {
            
            if (dropChance <= drop.dropRate)
            {
                possibleDrops.Add(drop);
            }
        }
  

        // If there are some possible drops, spawn the rarest one
        if (possibleDrops.Count > 0)
        {

            // TODO: Calling this every time an enemy dies is inefficient.
            // Sort the possible drops by drop rate in descending order
            possibleDrops.Sort((a, b) => b.dropRate.CompareTo(a.dropRate));

            // Print the list of possible drops with their name and drop rate to debug log
            foreach (Drops possibleDrop in possibleDrops)
            {
                Debug.Log(possibleDrop.name + " " + possibleDrop.dropRate);
            }

            Drops drop = possibleDrops[0];
            Instantiate(drop.itemPrefFab, transform.position, Quaternion.identity);
            Debug.Log("Dropped item" + drop.name + " " + drop.dropRate);
        }
        
    }

}
