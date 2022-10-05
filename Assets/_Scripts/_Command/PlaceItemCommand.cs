using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemCommand : iCommand
{
    Vector3 position;
    Transform item;

    public PlaceItemCommand(Vector3 position, Transform item)
    {
        this.position = position;
        this.item = item;
    }

    public void Execute()
    {
        itemPlacer.PlaceItem(item);
    }

    public void Undo()
    {
        itemPlacer.RemoveItem(item.position);
    }
}
