using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellButton : MonoBehaviour
{
    // Potion images
    public Sprite griefEnhancement;
    public Sprite griefShield;
    public Sprite loveEnhancement;
    public Sprite loveShield;
    public Sprite mirthEnhancement;
    public Sprite mirthShield;
    public Sprite wrathEnhancement;
    public Sprite wrathShield;
    public Sprite stun;
    public Sprite pharmaka;

    // Start is called before the first frame update
    void Start()
    {
        //DisplayPotion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void DisplayPotion() {
        switch (Spell.typeOfCurrentSpell) {
            case MoveType.Shield:
                switch (Spell.emotionOfCurrentSpell) {
                    case EmotionType.Grief:
                        
                        break;
                    case EmotionType.Love:

                        break;
                    case EmotionType.Wrath:

                        break;
                    case EmotionType.Mirth:

                        break;
                }
                break;
            case MoveType.Enhancement:
                switch (Spell.emotionOfCurrentSpell) {
                    case EmotionType.Grief:

                        break;
                    case EmotionType.Love:

                        break;
                    case EmotionType.Wrath:

                        break;
                    case EmotionType.Mirth:

                        break;
                }
                break;
            case MoveType.Paralysis:

                break;
            case MoveType.Pharmaka:

                break;
            default:
                break;
        }
    }*/
}
