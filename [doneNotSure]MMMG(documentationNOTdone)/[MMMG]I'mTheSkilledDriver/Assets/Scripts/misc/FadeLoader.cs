using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLoader : MonoBehaviour {

    public static FadeLoader FadeSLoad;
    public GameObject FadeScreen;

    public string TitleScreen = "TitleScreen";
    public string GameScreen = "SampleScene";
    public string CarRadioScreen = "CarRadioRolls";

    // Use this for initialization
    void Awake () {
        FadeSLoad = this;
	}

    //Animation Controller - fade screen
    public IEnumerator Fading(string name_of_scene)
    {
        Animator FadeAnim = FadeScreen.GetComponent<Animator>();
        FadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(FadeAnim.GetCurrentAnimatorStateInfo(0).length);
        if (name_of_scene != "null")
            SceneManager.LoadScene(name_of_scene);
    }
}
