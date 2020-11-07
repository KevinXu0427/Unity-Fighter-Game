using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObject/CreateCS", order = 1)]
public class CharacterSelection1 : ScriptableObject
{
    public bool player1Ready = false;
    public bool player2Ready = false;
}
