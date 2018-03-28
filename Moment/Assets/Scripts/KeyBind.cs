using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text power1, power2, formShift, jump;
    private GameObject currentKey;

	// Use this for initialization
	void Start () {
        keys.Add("Power 1", KeyCode.Z);
        keys.Add("Power 2", KeyCode.X);
        keys.Add("Form Shift", KeyCode.C);
        keys.Add("Jump", KeyCode.Space);

        power1.text = keys["Power 1"].ToString();
        power2.text = keys["Power 2"].ToString();
        formShift.text = keys["Form Shift"].ToString();
        jump.text = keys["Jump"].ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked; 
    }

    
        

        
}
