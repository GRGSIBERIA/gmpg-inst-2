﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrivenScript : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

        }

        direction.Normalize();
    }
}