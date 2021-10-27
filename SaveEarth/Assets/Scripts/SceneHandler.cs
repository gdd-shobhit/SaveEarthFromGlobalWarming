using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
   public void SceneChange()
    {
        CSVImportTool.TestMethod();
        SceneManager.LoadScene(1);
    }
}
