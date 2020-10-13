using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => ButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        switch (this.tag)
        {
            case "Tactic Card1":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[0];
                break;
            case "Tactic Card2":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[1];
                break;
            case "Tactic Card3":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[2];
                break;
            case "Tactic Card4":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[3];
                break;
            case "Tactic Card5":
                GameEngine.Instance.playerAvatars[0].sprite = GameEngine.Instance.tacticCardSprites[4];
                break;
            case "Tactic Card6":
                GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[5];
                break;
            case "Tactic Card7":
                GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[6];
                break;
            case "Tactic Card8":
                GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[7];
                break;
            case "Tactic Card9":
                GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[8];
                break;
            case "Tactic Card10":                
                GameEngine.Instance.playerAvatars[1].sprite = GameEngine.Instance.tacticCardSprites[9];
                break;
        }
    }
}
