using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.handler;
using Protocols.dto;
using UnityEngine.UI;

public class CanvasManage : MonoBehaviour
{

    public static CanvasManage _instance;
    [SerializeField]
    public Shortcut[] shortcuts;

    [SerializeField] public GameObject mask;
    [SerializeField] public Text fuhuoText;
    #region Head

    [SerializeField] private Image head;
    [SerializeField] private Text level;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text hp;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Text mp;


    public void userDtoChanged(ChangedType changedType)
    {
        if (changedType == ChangedType.HPMP || changedType == ChangedType.EXP || changedType == ChangedType.LEVEL || changedType == ChangedType.All)
        {
            SetHeadInfo(GameData.UserDto);
        }
    }
    #endregion


    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (GameData.UserDto != null)
        {
            SetHeadInfo(GameData.UserDto);
        }
        GameData.userDtoChanged += userDtoChanged;
        mask.SetActive(false);
        fuhuoText.gameObject.SetActive(false);
    }

   

    void OnDestroy()
    {
        GameData.userDtoChanged -= userDtoChanged;


    }
    void Update()
    {

    }
    public void SetHeadInfo(UserDTO userDto)
    {
        head.sprite = Resources.Load<Sprite>("Ui/Head/" + userDto.modelName);
        level.text = userDto.level.ToString();
        hpSlider.value = (float)userDto.hp/userDto.maxHp;
        hp.text = userDto.hp + "/" + userDto.maxHp;
        mpSlider.value = (float)userDto.mp / userDto.maxMp;
        mp.text = userDto.mp + "/" + userDto.maxMp;

    }


  



    public void OnSKillClick()
    {
        Skill._instance.Show();
    }

    public void OnknapsackClick()
    {
        Knapsack._instance.Show();
    }

    public void OnTalkClick()
    {
        Talk._instance.Show();
    }
}
