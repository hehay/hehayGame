using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;
using UnityEngine.UI;

public class CreatRolePanel : MonoBehaviour
{

    [SerializeField] private GameObject role1;
    [SerializeField] private GameObject role2;
    [SerializeField] private  InputField nameInput;
    [SerializeField] private GameObject selectPanle;
    private Camera cam;
    private Vector3 norVector3;
    private Quaternion quaternion;
    private GameObject selectRole;

    private UserHandler userHandler;

    public CreatRolePanel(InputField name)
    {
        this.nameInput = name;
    }

    void Awake()
    {
        userHandler = GameObject.Find("net").GetComponent<UserHandler>();
        userHandler.CreateRole += CreateRole;
        cam = Camera.main;
        norVector3 = cam.transform.position;
        quaternion = cam.transform.rotation;
    }

    void OnDestory()
    {
        userHandler.CreateRole -= CreateRole;

    }
    #region 服务器返回操作
        /// <summary>
        /// 创建角色返回结果
        /// </summary>
        /// <param name="i">返回结果</param>
        void CreateRole(int i)
        {
            switch (i)
            {
                case -1:
                    WarrningManager.warringList.Add(new WarringModel("没有此用户存在", null, 2));
                    break;
                case -2:
                    WarrningManager.warringList.Add(new WarringModel("角色已达到最大限制数量", null, 2));
                    break;
                case -3:
                    WarrningManager.warringList.Add(new WarringModel("角色名已存在", null, 2));
                    break;
                case 1:
                    cam.transform.position = norVector3;
                    cam.transform.rotation = quaternion;
                    this.gameObject.SetActive(false);
                    selectPanle.gameObject.SetActive(true);
                    NetIO.Ins.Send(Protocol.User, 0, UserProtocol.GetRoleList_CREQ, null);
                    break;

            }
        }

    #endregion




    #region 客户端操作
    public void OnRole1BtnClick()
    {
        Vector3 point = role1.transform.Find("lookPoint").position;
        cam.transform.position = point;
        cam.transform.LookAt(role1.transform.position + Vector3.up * 0.5f);
        selectRole = role1;
    }

    public void OnRole2BtnClick()
    {
        Vector3 point = role2.transform.Find("lookPoint").position;
        cam.transform.position = point;
        cam.transform.LookAt(role2.transform.position+Vector3.up*0.5f);
        selectRole = role2;
    }

    public void OnCrateRoleBtnClick()
    {
        if (nameInput.text.Length <= 0)
        {
            WarrningManager.warringList.Add(new WarringModel("名字有误", null, 2));
            return;
        }
        if (selectRole == null)
        {
            WarrningManager.warringList.Add(new WarringModel("请选择角色", null, 2));
            return;
        }
        UserDTO userDto=new UserDTO();
        userDto.name = nameInput.text;
        userDto.modelName = int.Parse(selectRole.name);
        // 发送创建角色信息
        NetIO.Ins.Send(Protocol.User,0,UserProtocol.CreateRole_CREQ,userDto);
       
    }

    public void OnBackBtnClick()
    {
        cam.transform.position = norVector3;
        cam.transform.rotation = quaternion;
        this.gameObject.SetActive(false);
        selectPanle.gameObject.SetActive(true);
        selectRole = null;
        nameInput.text = "";
    }
#endregion
}
