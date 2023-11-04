using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    [System.Serializable]
    private class Prototype
    {
        public string ID;
        public GameObject prefab;
    }
    [SerializeField] private List<Prototype> registry = new List<Prototype>();
    private GameObject findObject(string id) {
        foreach(Prototype p in registry) {
            if(p.ID.Equals(id)) return p.prefab;
        }
        return null;
    }
    public GameObject Create(string id, Vector3 position, Quaternion rotation) {
        GameObject prefab = findObject(id);
        if(prefab != null) return Instantiate(prefab, position, rotation);
        return null;
    }

}
