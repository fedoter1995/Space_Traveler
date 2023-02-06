using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = FindObjectOfType<PlayerPosition>();
                if (go != null)
                    m_instance = go;
                else
                    m_instance = new GameObject("[PLAYER_POSITION]").AddComponent<PlayerPosition>();
            }

            return m_instance;
        }

    }

    private static PlayerPosition m_instance;

    public Vector3 Position => m_instance.transform.position;
}
