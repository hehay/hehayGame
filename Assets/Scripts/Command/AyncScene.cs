using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AyncScene : MonoBehaviour {

 [SerializeField]private Slider progressBar;
    [SerializeField]private Text text;
    private bool isAsyn = false;
    private AsyncOperation ao = null;
    private float progress = 0;
	void Awake () {
        Debug.Log(GameData.wantLoadScene);
        SetScenen(GameData.wantLoadScene);
	}
    public void SetScenen(int i)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(i);
        Show(asyncOperation);

    }
    void Show(AsyncOperation asyncOperation)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        isAsyn = true;
        this.ao = asyncOperation;
    }
    void Update()
    {
        if (isAsyn)
        {
            progress = (int)(ao.progress * 100);
            if (progress >= 89)
            {
                progress = 99;
                isAsyn = false;

            }
            text.text = "Loading " + progress + "%";
            progressBar.value = (progress / 100);
        }

    }
}
