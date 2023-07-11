using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socials : MonoBehaviour
{
    public void OpenURL(string _URL)
    {
        Application.OpenURL(_URL);
    }    
}
