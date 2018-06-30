using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingGround : Building
{
    public int capacity = 9;
    private Character instructor;
    private List<Character> slots = new List<Character>();

    TrainingGround()
    {
        type = Type.TrainingGround;
    }

    void Awake()
    {
        this.name = "Training Ground";
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
