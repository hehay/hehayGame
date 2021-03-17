using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraFacingBillboard : MonoBehaviour
{

    Camera referenceCamera;

    public enum Axis { up, down, left, right, forward, back };
    public bool reverseFace = false;
    public Axis axis = Axis.up;

    // return a direction based upon chosen axis
    public Vector3 GetAxis(Axis refAxis)
    {
        switch (refAxis)
        {
            case Axis.down:
                return Vector3.down;
            case Axis.forward:
                return Vector3.forward;
            case Axis.back:
                return Vector3.back;
            case Axis.left:
                return Vector3.left;
            case Axis.right:
                return Vector3.right;
        }

        // default is Vector3.up
        return Vector3.up;
    }

    void Awake()
    {
        // if no camera referenced, grab the main camera
        if (!referenceCamera)
            referenceCamera = Camera.main;
    }

    void Update()
    {
        // rotates the object relative to the camera
        Vector3 targetPos = transform.position + referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
        Vector3 targetOrientation = referenceCamera.transform.rotation * GetAxis(axis);
        transform.LookAt(targetPos, targetOrientation);
    }



#region 人物生命血条
    public Text playName;
    public Text level;
    public ChangeColorByScale hpccb;
    public ChangeColorByScale mpccb;
    public void SetInfo(string name,float hp,float mp)
    {
        this.playName.text = name;
        hpccb.SetHealthVisual(hp);
        mpccb.SetHealthVisual(mp);
    }
    public void SetInfo(string name, float hp, float mp,int level)
    {
        this.playName.text = name;
        this.level.text = "lv." + level;
        hpccb.SetHealthVisual(hp);
        mpccb.SetHealthVisual(mp);
    }
    public void SetInfo(string name, float hp)
    {
        this.playName.text = name;
        hpccb.SetHealthVisual(hp);
    }
#endregion
#region 掉血

    [SerializeField] private Text fallbloodText;
    [SerializeField] private Image image;
    public void SetText(int  text,Color addBlood,int crit=0)
    {
       
        fallbloodText.text = text.ToString();
        if (crit==0)
        {
            image.gameObject.SetActive(false);
            fallbloodText.fontSize = 60;
            fallbloodText.fontStyle = FontStyle.Normal;
        }
        else
        {
            image.gameObject.SetActive(true);
            fallbloodText.fontSize = 70;
            fallbloodText.fontStyle=FontStyle.Bold;
        }

        fallbloodText.color = addBlood;
        gameObject.SetActive(true);
    }


    public void AnimEd()
    {
      gameObject.SetActive(false);
        GameManage._instance.fallbloodPool.Push(gameObject);
    }
#endregion
}
