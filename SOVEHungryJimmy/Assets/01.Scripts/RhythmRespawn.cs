using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmRespawn : MonoBehaviour
{
    public GameObject RhythmManager;
    public GameObject RhythmPanel;
    public Transform RhythmPanelPosition;

    // Start is called before the first frame update
    public void Respawn()
    {
        Instantiate<GameObject>(RhythmPanel, RhythmPanelPosition.position, RhythmPanelPosition.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
