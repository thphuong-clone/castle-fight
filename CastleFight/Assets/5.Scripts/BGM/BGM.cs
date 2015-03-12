using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {
    private static BGM instance;
    private static bool created;

    public static BGM Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BGM>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

	void Awake () {
	    if (created)
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
	}

    public void Play()
    {
        //implement play (change sound, add sound ...)
        audio.Play();
    }
	
}
