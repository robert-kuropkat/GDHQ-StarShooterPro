using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Explosion sound effect.</summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Explosion Prefab
/// 
/// This is not part of the GameDevHQ recommended layout.  They destory the sound effect
/// where it is invooked.  I prefer for objects to cleanup after themselves.  So this
/// class does nothing more than destroy itself 2.5 seconds after being invoked.
/// </remarks>
/// 

public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
