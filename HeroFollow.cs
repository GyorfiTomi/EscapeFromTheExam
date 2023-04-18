using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] GameObject Hero;
    [SerializeField] GameObject Circle;
    float FollowMouseAmount = 0.1f;
    float FollowZoomOut = -10;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(Hero.transform.position.x, Hero.transform.position.y, -10f), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,FollowZoomOut)), FollowMouseAmount);
    }
}
