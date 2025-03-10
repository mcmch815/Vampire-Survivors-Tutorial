using System.Net;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    public GameObject targetMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mc = FindAnyObjectByType<MapController>();
    }

    //As long as the player stays on a chunk's box collider, the map controller's 
    //ccurent chunk is set to this chunk
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if(mc.currentChunk == targetMap)
            {
                mc.currentChunk = null;
            }
        }
    }
}
