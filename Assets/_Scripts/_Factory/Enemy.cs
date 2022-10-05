using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract string Name { get; }

    public abstract GameObject Create(GameObject _prefab);
}

public class Crab : Enemy
{
    public override string Name => "crab";

    //
    //public override string Name { get { return "crab"; }  }

    public override GameObject Create(GameObject _prefab)
    {
        GameObject enemy = Instantiate(_prefab);
        Debug.Log("Crab enemy is created");

        return enemy;
    }

}

public class Monster : Enemy
{
    public override string Name => "monster";

    public override GameObject Create(GameObject _prefab)
    {
        GameObject enemy = Instantiate(_prefab);
        Debug.Log("Monster enemy is created");

        return enemy;
    }
}