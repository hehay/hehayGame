using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarrningManager : MonoBehaviour {

    public static List<WarringModel> warringList=new List<WarringModel>();

   [SerializeField]private WarrningWindow window;
    void Awake ()
    {
    }
	
	// Update is called once per frame
	void Update () {
	    if (warringList.Count > 0)
	    {
	        WarringModel model = warringList[0];
            window.Active(model);
            warringList.RemoveAt(0);
	    }
	}
}
