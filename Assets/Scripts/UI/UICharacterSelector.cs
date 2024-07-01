using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelector : MonoBehaviour
{
	public LevelScript levelScript;

    // Start is called before the first frame update
    void Start()
    {
        levelScript = GameObject.Find("LevelObject").GetComponent<LevelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
