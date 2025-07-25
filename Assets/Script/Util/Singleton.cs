using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_ins;
    public static T Ins
    {
        get
        {
            if (m_ins == null)
            {
                m_ins = GameObject.FindFirstObjectByType<T>();

                if (m_ins == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_ins = singleton.AddComponent<T>();
                }
            }
            return m_ins;
        }
    }
}
