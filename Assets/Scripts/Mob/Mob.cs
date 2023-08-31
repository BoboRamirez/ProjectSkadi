using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public GameObject playerGO;
    public MobBaseState currentState;

    

    // Update is called once per frame
    void Update()
    {
        currentState?.FrameUpdate();
    }

    void FixedUpdate() 
    {
        currentState?.PhysicsUpdate();
    }

    
}
