using UnityEngine;
using System.Collections.Generic;
public class Token : MonoBehaviour
{
    List<string> tokens = new List<string> { "var", " ", "<-", "Spawn", "Color" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckInput(string input)
    {
        string[] splitInput = input.Split(" ");


    }
}
