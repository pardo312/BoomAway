using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLevelPlay : MonoBehaviour
{
    public void addToLVL(string pLvl)
    {
        Grid.gameStateManager.frequency.addToLVL(pLvl);
    }
}
