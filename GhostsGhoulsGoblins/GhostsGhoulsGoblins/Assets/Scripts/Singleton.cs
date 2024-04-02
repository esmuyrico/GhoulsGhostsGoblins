using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            // makes sure there is no instance
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // create a new gameobject
                    GameObject obj = new GameObject();
                    // set the name of the object
                    obj.name = typeof(T).Name;
                    // add the singleton to 
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
    public virtual void Awake()
    {
        if (_instance == null)
        {
            // treat this as the object it is attached to
            // ex: treat this as a gameManager, not a singleton
            _instance = this as T;
            // save yourself when a new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
