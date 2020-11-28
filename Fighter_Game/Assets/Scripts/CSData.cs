using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObject/CreateCS", order = 1)]
public class CSData : ScriptableObject
{
    public bool masterPlayerReady = false;
    public bool localPlayerReady = false;
}
