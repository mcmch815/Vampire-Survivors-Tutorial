using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ExperienceGem : Pickup, ICollectible
{

    public int experienceGranted;
    public void Collect()
    {
        PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();
        playerStats.IncreaseExperience(experienceGranted);
       

    }



}
