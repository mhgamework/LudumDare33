using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectPoolComponent
    : MonoBehaviour
{
    // -- PUBLIC

    // .. CONSTRUCTORS

    // .. OPERATIONS

    public GameObject GetPooledObject()
    {
        for ( int index = PoolTable.Count; index < Size; index++ )
        {
            CreateNewObject();
        }

        foreach (GameObject game_object in PoolTable)
        {
            if (!game_object.activeSelf)
            {
                return game_object;
            }
        }

        if (ItHasWildGrowth)
        {
            return CreateNewObject();
        }

        return null;
    }

    /// <summary>
    /// Excludes given object from the pool, making sure it doesn't get recycled when not active.
    /// </summary>
    /// <param name="game_object"></param>
    public void ExcludePooledObject(GameObject game_object)
    {
        if (!PoolTable.Contains(game_object))
            return;

        PoolTable.Remove(game_object);
        ExcludedPoolTable.Add(game_object);
    }

    /// <summary>
    /// Includes a previously excluded object in the pool.
    /// </summary>
    /// <param name="game_object"></param>
    public void IncludePooledObject(GameObject game_object)
    {
        if (!ExcludedPoolTable.Contains(game_object))
            return;

        ExcludedPoolTable.Remove(game_object);
        PoolTable.Add(game_object);
    }

    // .. INQUIRIES

    public bool HasPooledObjects()
    {
        return PoolTable.Count > 0;
    }

    // -- PRIVATE

    // .. OPERATIONS

    GameObject CreateNewObject()
    {
        GameObject
            new_game_object;

        new_game_object = (GameObject)GameObject.Instantiate(PrefabToClone);

        new_game_object.SetActive(false);

        new_game_object.GetComponent<Transform>().SetParent(GetComponent<Transform>(), false);

        PoolTable.Add(new_game_object);

        return new_game_object;
    }

    // .. ATTRIBUTES

    [SerializeField]
    GameObject
        PrefabToClone = null;
    [SerializeField]
    int
        Size = 20;
    [SerializeField]
    bool
        ItHasWildGrowth = false;
    List<GameObject>
        PoolTable = new List<GameObject>(20);
    private List<GameObject>
        ExcludedPoolTable = new List<GameObject>();
}