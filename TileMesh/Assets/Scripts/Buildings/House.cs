using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    public int capacity = 4;
    public float stamina_recovery_rate = 20.0f;  //per second
    private List<Character> slots = new List<Character>();
    
    House()
    {
        Debug.Log("House()");
        type = Type.House;
    }

    void Awake()
    {
        this.name = "House";
    }

    public int vacanies()
    {
        return capacity - slots.Count;
    }

    public override bool enter(Character character)
    {
        if (slots.Count < capacity)
        {
            slots.Add(character);        
            return true;
        }
        else
            return false;
    }

    void leave(Character character)
    {
        character.gameObject.SetActive(true);
    }

    void Update()
    {
        if (slots.Count > 0)
        {
            for ( int i= slots.Count - 1; i >=0; --i)
            {
                Character character = slots[i];
                character.character_info.stamina += stamina_recovery_rate * Time.deltaTime;
                character.character_info.stamina = Mathf.Clamp(character.character_info.stamina, 0f, 100f);
                //Debug.Log("character.character_info.stamina = " + character.character_info.stamina);
                if (character.character_info.stamina >= 95f)
                {
                    slots.RemoveAt(i);
                    leave(character);
                }
            }
        }
        
    }


}
