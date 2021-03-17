using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Protocols.dto;
using UnityEngine.UI;

public class Role : MonoBehaviour {

	// Use this for initialization
    [SerializeField] private Text playname;
    [SerializeField] private Text role;
    [SerializeField] private Text level;
    [SerializeField] private Image head;
    [SerializeField] private Text scene;
    [SerializeField] private SelectRolePanel selectRolePanel;
    private UserDTO userDto=new UserDTO();

    void Start()
    {
        selectRolePanel = GameObject.Find("SelectPanel").GetComponent<SelectRolePanel>();
    }
    public void SetInfo(UserDTO userDto)
    {
        this.userDto = userDto;
        playname.text = userDto.name;
        level.text = "Lv." + userDto.level;

        string modelName="";
        switch (userDto.modelName)
        {
            case ModelName.LichModel:
                modelName = "巫妖";
                break;
        }
        role.text = "职业 ：" + modelName;

        string sceneName="";
        switch (userDto.map)
        {
            case 3:
                sceneName = "丘陵之地";
                break;
            case 4:
                sceneName = "无人小道";
                break;
        }
        scene.text = "场景 ："+sceneName;
        head.sprite = Resources.Load<Sprite>("Ui/Head/" + userDto.modelName);
    }

    public void OnClick()
    {
        selectRolePanel.SetRole(userDto);
    }
}
