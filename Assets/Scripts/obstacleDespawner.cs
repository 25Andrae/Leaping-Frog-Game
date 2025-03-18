using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleDespawner : MonoBehaviour
{
    static public bool isTheFirstObjectHasBeenDestroyed = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("coliderDespawner"))
        {
            Destroy(gameObject);
            isTheFirstObjectHasBeenDestroyed = true;
        }
    }
}
