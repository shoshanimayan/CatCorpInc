using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoader : MonoBehaviour
{

    private void Awake()
    {
#if UNITY_EDITOR

        if (!SceneManager.GetSceneByBuildIndex(0).isLoaded)
        {
            Debug.Log("Loading in Root");
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
            Destroy(gameObject);
            return;

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
#else
        Destroy(gameObject);
                    return;


#endif

    }
}
