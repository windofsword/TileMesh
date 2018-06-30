using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBox : MonoBehaviour {

    private List<string> event_log = new List<string>();
    private string gui_text = "";

    public int max_lines = 10;

    public static LogBox get_log_box()
    {
        return GameObject.Find("LogBox").GetComponent<LogBox>();
    }


	// Use this for initialization
	void Start () {
		
	}

    void OnGUI()
    {
//        GUI.Label(new Rect(0, Screen.height - (Screen.height / 3), Screen.width, Screen.height / 3), gui_text, GUI.skin.textArea);
        GUI.Label(new Rect(0, Screen.height - (Screen.height / 3), Screen.width / 3, Screen.height / 3), gui_text, GUI.skin.textArea);

    }

    public void AddEvent(string eventString)
    {
        event_log.Add(eventString);

        if (event_log.Count >= max_lines)
            event_log.RemoveAt(0);

        gui_text = "";

        foreach (string logEvent in event_log)
        {
            gui_text += logEvent;
            gui_text += "\n";
        }
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
}
