using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpenLink : MonoBehaviour
{
    public void OpenChannel()
    {
        Application.OpenURL("https://twitter.com/home");
    }
}
