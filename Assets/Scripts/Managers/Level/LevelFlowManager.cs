using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlowManager : MonoSingleton<LevelFlowManager>
{

    private void OnEnable()
    {
        
    }


    public IEnumerator StartLevel()
    {



        yield return null;
    }

    public struct LevelStartEvent
    {
        public int Gold;

        public LevelStartEvent(int gold)
        {
            Gold = gold;
        }
    }
}


