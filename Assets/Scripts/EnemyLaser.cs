using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy Laser Behavior
/// </summary>
/// 
/// <remarks>
/// Instantiated By:
/// Attached To: Enemy Laser Prefab
/// 
/// Enemy Laser, like the TripleShot PowerUp is composed of multiple Laser game objects.
/// In the same way, when the child laser objects have destoryed themselves, this
/// Update method will destroy this parent object.
/// 
/// Additioannly, this serves a storage location for the HasHit property.  This property
/// is used to determine if one of the lasers has hit the Player.  The Laser object
/// can then use this property to apply the damage for the first laser, but ignore the
/// second, thus eliminating an Enemy "Double Tap."
/// </remarks>
/// 

public class EnemyLaser : MonoBehaviour
{

    /// <summary>
    /// Properties (getter/setter methods)
    /// </summary>
    public bool HasHit { get; set; }

    /// <summary>
    /// Destroy this object when all child (lasers) are destroyed
    /// </summary>
    /// 

    void Update()
    {
        if (this.transform.childCount < 1) { Destroy(this.gameObject); }
    }

}
