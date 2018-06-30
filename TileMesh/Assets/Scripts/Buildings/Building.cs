using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{
    public enum Type{House, Canteen, TrainingGround};

    public Type type;
    public Direction direction = Direction.Up;
	public int size_x;
	public int size_y;
    public int size_z;

    public GameObject model;

    public void Start()
    {
        Debug.Log("Building.Start()");
        if (model == null)
        {
            //GameObject cube = GetComponent<PrimitiveType.Cube>();
            model = GameObject.CreatePrimitive(PrimitiveType.Cube);
            model.name = "BuildingCube";
            Transform offset = this.transform.Find("Offset");
            model.transform.SetParent(offset);
            //model.transform.SetParent(this.transform);

            Vector3 scale = model.transform.localScale;
            scale.x = size_x;
            scale.y = size_y;
            scale.z = size_z;
            model.transform.localScale = scale;


            Vector3 local_pos = model.transform.localPosition;
            local_pos.x = 0f;
            local_pos.y = 0f;
            local_pos.z = 0f;
            model.transform.localPosition = local_pos;


            gameObject.layer = LayerMask.NameToLayer("Building");
            model.layer = LayerMask.NameToLayer("Building");
            model.tag = "Building";
            //Debug.Log("cube.layer = " + cube.layer );

            //Debug.Log("cube local pos: " + cube.transform.localPosition);


        }
    }

    public virtual bool enter(Character character){
        return true;
    }

    //public virtual void leave(Character character){}

    public void rotate()
    {
        Vector3 rotate_angle = new Vector3(0f, 90f, 0f);

        Transform offset = this.transform.Find("Offset");
        if (offset)
        {
            offset.Rotate(rotate_angle);
            //model.transform.Rotate(rotate_angle);
            ++direction;
            if (direction == Direction.Unknown)
                direction = Direction.Up;
            if (direction == Direction.Up || direction == Direction.Down)
            {
                Vector3 local_pos = offset.localPosition;
                local_pos.x = size_x / 2.0f;
                local_pos.y = size_y / 2.0f;
                local_pos.z = size_z / 2.0f;
                offset.localPosition = local_pos;
            }
            else if (direction == Direction.Right || direction == Direction.Left)
            {
                Vector3 local_pos = offset.localPosition;
                local_pos.x = size_z / 2.0f;
                local_pos.y = size_y / 2.0f;
                local_pos.z = size_x / 2.0f;
                offset.localPosition = local_pos;
            }
        }
        
            
    }




}
