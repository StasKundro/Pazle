using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPazle : MonoBehaviour
{
    public GameObject[] girl;
    void Start()
    {
        girl[Random.Range(0, girl.Length)].SetActive(true);
    }
}
