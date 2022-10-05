using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCommand
{
    public void Execute();
    public void Undo();
    //void Redo();
}
