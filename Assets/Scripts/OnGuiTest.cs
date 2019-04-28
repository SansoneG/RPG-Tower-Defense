using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuiTest : MonoBehaviour
{
    
    void OnGUI () {
		GUILayout.BeginArea(new Rect(4f, 4f, 150f, 500f));
		if(GUILayout.Button("Send Message 1"))
        {
            Messager.NewMessage("Hallo");
        }
        if(GUILayout.Button("Send Message 2"))
        {
            Messager.NewMessage("World");
        }
		GUILayout.EndArea();
	}

}
