using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectRolePanel : MonoBehaviour
{


    [SerializeField] private GameObject createPanle;
    [SerializeField] private Transform gird;
    [SerializeField] private GameObject rolePrefab;
    private UserDTO userDto=null;
    private List<UserDTO> userDtoList=new List<UserDTO>();
    [SerializeField] private GameObject[] roles;
    private Dictionary<UserDTO ,GameObject> userTObject=new Dictionary<UserDTO, GameObject>(); 
    private Camera cam;
    private Vector3 norVector3;
    private Quaternion quaternion;
    private UserHandler userHandler;
    void Awake()
    {
        userHandler = GameObject.Find("net").GetComponent<UserHandler>();
        NetIO.Instance.Write(Protocol.User,0,UserProtocol.GetRoleList_CREQ,null);
        userHandler.GetRoleList += GetRoleList;
        userHandler.DeleteRole += DeleteRole;
        userHandler.OnLine += OnLine;
    }

    void OnDestory()
    {
        userHandler.GetRoleList -= GetRoleList;
        userHandler.DeleteRole -= DeleteRole;
        userHandler.OnLine -= OnLine;
    }
    void Start () {
        cam = Camera.main;
        norVector3 = cam.transform.position;
        quaternion = cam.transform.rotation;
	}


    #region 服务器返回结果操作
    void OnLine(UserDTO i)
    {
        if (i == null)
        {
            WarrningManager.warringList.Add(new WarringModel("登录失败", null, 2));
        }
        else
        {
            GameData.UserDto = i;
            
            // 跳转游戏界面
            SceneManager.LoadScene(2);
        }
       
    }
    /// <summary>
    /// 服务器返回初始获取角色列表
    /// </summary>
    /// <param name="userDtos"></param>
    void GetRoleList(List<UserDTO> userDtos)
    {
        if (userDtos == null) return;
        if(userDtoList.Count>0) userDtoList.Clear();
        if (userTObject.Count > 0)  userTObject.Clear();

        for (int i = 0; i < gird.childCount; i++)
        {
            Destroy(gird.GetChild(i).gameObject);
        }
        userDtoList = userDtos;
        for (int i = 0; i < userDtos.Count; i++)
        {
            GameObject role = Instantiate(rolePrefab);
            userTObject.Add(userDtos[i],role);
            role.GetComponent<Role>().SetInfo(userDtos[i]);
            role.transform.SetParent(gird);
            role.transform.localPosition = Vector3.zero;
            role.transform.localScale = Vector3.one;
        }
    }
    /// <summary>
    /// 服务器返回删除角色结果
    /// </summary>
    /// <param name="i"></param>
    void DeleteRole(int i)
    {
        switch (i)
        {
            case -1:
                WarrningManager.warringList.Add(new WarringModel("没有这个账号", null, 2));
                break;
            case -2:
                WarrningManager.warringList.Add(new WarringModel("没有这个角色", null, 2));

                break;
            case 1:
                userDtoList.Remove(userDto);
                if (userTObject.ContainsKey(userDto))
                {
                    Destroy(userTObject[userDto]);
                    userTObject.Remove(userDto);
                }
                userDto = null;
                cam.transform.position = norVector3;
                cam.transform.rotation = quaternion;
                break;
        }
    }
#endregion



    #region 客户端的操作
        /// <summary>
        /// 客户端设置选择角色
        /// </summary>
        /// <param name="userDto"></param>
        public void SetRole(UserDTO userDto)
        {
            foreach (GameObject o in roles)
            {
                if (int.Parse(o.name) == userDto.modelName)
                {
                    Vector3 point = o.transform.Find("lookPoint").position;
                    cam.transform.position = point;
                    cam.transform.LookAt(o.transform.position + Vector3.up * 0.7f+Vector3.left*0.7f);
                    this.userDto = userDto;
                    break;
                }
            }
        }
        /// <summary>
        /// 客户端创建角色
        /// </summary>
        public void OnCreateRoleBtnClick()
        {
            cam.transform.position = norVector3;
            cam.transform.rotation = quaternion;
            createPanle.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 客户端删除角色
        /// </summary>
        public void OnDeleteRoleBtnClick()
        {
            if (userDto != null)
            {
                NetIO.Instance.Write(Protocol.Pos,0,PosProtocol.DeletePos_CREQ,null);
                NetIO.Instance.Write(Protocol.User,0,UserProtocol.DeleteRole_CREQ,userDto);
            }
        }
        /// <summary>
        /// 客户端进入游戏
        /// </summary>
        public void OnEnterGameBtnClick()
        {
            if (userDto != null)
            {
                NetIO.Instance.Write(Protocol.User, 0, UserProtocol.OnLine_CREQ, userDto);
            }
            else
            {
                WarrningManager.warringList.Add(new WarringModel("请选择角色", null, 2));
                Debug.Log("没有选择角色");
            }
        }

    #endregion
}
