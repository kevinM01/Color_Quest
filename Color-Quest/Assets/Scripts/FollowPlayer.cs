using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
 private Vector3 offset= new Vector3(5,-1,-48);
    public GameObject player;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);

    }
}
