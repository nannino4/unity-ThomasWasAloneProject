using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelector : MonoBehaviour
{
	private LevelScript levelScript;
	private PlayerController[] characters;
	[SerializeField] private GameObject UICharacterPrefab;

    // Start is called before the first frame update
    void Start()
    {
		// fetch the UICharacter prefab
		// UICharacterPrefab = Resources.Load<GameObject>("Prefabs/UI/UICharacter");
		// fetch the characters
        levelScript = GameObject.Find("LevelObject").GetComponent<LevelScript>();
		characters = levelScript.GetCharacters();
		// add a UICharacter for every character
		for (int i = 0; i < characters.Length; i++)
		{
			GameObject UICharacter = Instantiate(UICharacterPrefab, transform);
			// set UICharacter's color
			UICharacter.transform.Find("Sprite").GetComponent<Image>().color = characters[i].GetColor();
			// set UICharacter's activation key
			UICharacter.transform.Find("ActivationKey").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
