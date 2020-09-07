using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public Transform player;
    private float objectWidth;
    private float objectHeight;
    
    // Update is called once per frame
    void LateUpdate()
    {
        player = GameObject.Find("Player(Clone)").transform;
        objectWidth = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = player.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        player.position = new Vector2(
            Mathf.Clamp(player.position.x, -69f + objectWidth, 72f - objectWidth),
            Mathf.Clamp(player.position.y, -46f + objectHeight, 54f - objectHeight));
    }
}