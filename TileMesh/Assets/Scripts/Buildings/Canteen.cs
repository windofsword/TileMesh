using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canteen : Building
{
    public int capacity = 9;
    private List<Character> slots = new List<Character>();

    Canteen()
    {
        Debug.Log("Canteen()");
        type = Type.Canteen;
    }

    void Awake()
    {
        this.name = "食堂";
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
        }
    }


}
