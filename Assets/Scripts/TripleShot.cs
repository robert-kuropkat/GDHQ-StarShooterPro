using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cleanup TripleShot game object
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: TripleShot Prefab
/// 
/// In the GameDevHQ tutorials, this is handled elsewhere.  However, as with the Explosion class
/// I prefer objects clean themselces up whenever possible.
/// </remarks>
/// 

public class TripleShot : MonoBehaviour
{

    /// <summary>
    /// Destroy the TripleShot PowerUp object once all its child lasers are destroyed.
    /// </summary>
    /// 
    /// <remarks>
    /// THe child laser objects are already set to destroy themselves once they are off screen.
    /// Once they are destroyed, this object will destroy itself as well.
    /// </remarks>
    void Update()
    {
        if (this.transform.childCount < 1) { Destroy(this.gameObject); }
    }
}
