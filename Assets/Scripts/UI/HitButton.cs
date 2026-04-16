using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitBut{
    None = 0,
    S = 1,
    D = 2,
    F = 3,
    J = 4,
    K = 5,
    L = 6
}

public class HitButton : MonoBehaviour
{
    [SerializedFeild] private HitBut button;


}
