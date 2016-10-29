using UnityEngine;
using System.Runtime.InteropServices;

public class TestScript : MonoBehaviour {

#if UNITY_IPHONE || UNITY_XBOX360
       [DllImport ("__Internal")]
#else
    [DllImport("TreeModelsDll")]
#endif
    private static extern int TestFunc();

    void Start()
    {
        print(TestScript.TestFunc());
    }
}
