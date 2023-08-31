using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    [SerializeField]
    private float interval;
    private float timeCount;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        
    }

    // UpdateState is called once per frame
    void Update()
    {
        if (timeCount >= interval)
        {
            timeCount -= interval;
            BroadCast();
        }
        else timeCount += Time.deltaTime;
    }

    private void BroadCast()
    {
        Vector2 speed = player.GetComponent<Rigidbody2D>().velocity;
        Debug.Log($"{speed}");
    }
}
