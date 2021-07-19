using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.handler;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public static GameManage _instance;

    private MapHandler map;
    private PosHandler pos;
    [SerializeField] private Transform fuhuoTransform;

    [SerializeField] private Transform spawan;

    [SerializeField] private Transform spawanLast;

    [SerializeField] private Transform spawanNext;

    private GameObject mask;
    private Text fuhuoText;
    public  Stack<GameObject> lichModelPool = new Stack<GameObject>();
    public Stack<GameObject> fallbloodPool=new Stack<GameObject>();
    [SerializeField] private GameObject fallBloodPrefab;
    public  Dictionary<int, GameObject> idToGameObjectDic = new Dictionary<int, GameObject>();
    public Dictionary<int, AbsRoleModel> IdToUserDtoDic = new Dictionary<int, AbsRoleModel>(); 

	void Awake ()
    {

	    _instance = this;
	    mask = CanvasManage._instance.mask;
	    fuhuoText = CanvasManage._instance.fuhuoText;
	    GameObject net = GameObject.Find("net");
        map = net.GetComponent<MapHandler>();
	    pos = net.GetComponent<PosHandler>();
	    pos.GetPos += GetPos;
        map.Move += Move;
        map.EnterScene += EnterScene;
        map.EnterSceneBRO += EnterSceneBro;
	    map.LeaveScene += LeaveScene;
        map.LeaveSceneBRO += LeaveSceneBro;
	    map.AttackBro += AttackBro;
        map.SkillBro += SkillBro;
	    map.UseInventory += UseInventory;
        map.UnUseInventory += UnUseInventory;
	    map.ReviveBro += ReviveBro;
	    map.DamageBro += DamageBro;
	    map.Kill += Kill;
        if (GameData.lastScene < 3) //判断是不是在选择角色界面开始游戏，获取上次下线位置
        {
            NetIO.Ins.Send(Protocol.Pos, 0, PosProtocol.GetPos_CREQ, null);
        }
        else
        {
            if (GameData.lastScene < SceneManager.GetActiveScene().buildIndex)
            {
                spawan = spawanLast;
            }
            else
            {
                spawan = spawanNext;
            }
        }
        NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.EnterMap_CREQ,
             GameData.UserDto);

	}

    void Start()
    {

    }

    private void UseInventory(InventoryItemDTO itemDto)
    {

        if (itemDto != null)
        {
            UserDTO userDto = IdToUserDtoDic[itemDto.userId] as UserDTO;
            GameObject model = idToGameObjectDic[itemDto.userId];
            Info info = model.GetComponent<Info>();
            if (userDto != null)
            {

                switch (itemDto.inventory.inventoryType)
                {
                    case InventoryType.Equip:
                        userDto.attack += itemDto.attack;
                            userDto.def += itemDto.def;
                            userDto.armour += itemDto.armour;
                            userDto.crit += itemDto.crit;
                            userDto.exemptCrit += itemDto.exemptCrit;
                            userDto.maxHp += itemDto.hp;
                            userDto.maxMp += itemDto.mp;
                            userDto.speed += itemDto.speed;
                        info.UserDto = userDto;
                        if (userDto.id == GameData.UserDto.id) //使用者是自己的话，更改ui界面的数据
                        {
                            GameData.UserDto = userDto;
                            GameData.SetEquips((int) itemDto.inventory.equipType, itemDto.id);
                        }
                        else
                        {
                            userDto.equips[(int) itemDto.inventory.equipType] = itemDto.id;
                        }
  
                        break;
                    case InventoryType.Drug:
                        switch (itemDto.inventory.infoType)
                        {
                            case InfoType.Hp:
                                userDto.hp += itemDto.inventory.applyValue;
                                if (userDto.hp > userDto.maxHp) userDto.hp = userDto.maxHp;
                                InitFallBoold(info, itemDto.inventory.applyValue, Color.green); //实例化加血
                                break;
                            case InfoType.Mp:
                                userDto.mp += itemDto.inventory.applyValue;
                                if (userDto.mp > userDto.maxMp) userDto.mp = userDto.maxMp;
                                InitFallBoold(info, itemDto.inventory.applyValue, Color.blue); //实例化加蓝
                                break;
                            case InfoType.Exp:
                                userDto.exp += itemDto.inventory.applyValue;
                                InitFallBoold(info, itemDto.inventory.applyValue, Color.magenta); //实例化加经验
                                int totalExp = 100 + userDto.level*30;
                                while (userDto.exp >= totalExp)
                                {
                                    // 升级
                                    userDto.level++;
                                    userDto.exp -= totalExp;
                                    userDto.hp += userDto.level * 50;
                                    userDto.maxHp += userDto.level * 50;
                                    userDto.mp += userDto.level * 25;
                                    userDto.maxMp += userDto.level * 25;
                                    totalExp = 100 + userDto.level*30;
                                }
                                break;
                        }
                        if (userDto.id == GameData.UserDto.id) //使用者是自己的话，更改ui界面的数据
                        {
                            GameData.SetHpMp(itemDto.inventory.applyValue);
                            GameData.SetHpMp(0, itemDto.inventory.applyValue);
                            GameData.SetExp(itemDto.inventory.applyValue);
                            if (itemDto.count <= 0)
                            {
                                Knapsack._instance.druginventoryGridUis[itemDto.inventoryGridId - 1].CleraInfo();
                                //清除背包的数据     
                                CanvasManage._instance.shortcuts[itemDto.shortcutid - 1].Clear(); //清除快捷栏的数据
                            }
                            else
                            {
                                Knapsack._instance.druginventoryGridUis[itemDto.inventoryGridId - 1].UpdateNum(
                                    itemDto.count);
                                //更新数据
                            }
                        }
                        model.transform.Find("infoUi")
                            .GetComponent<CameraFacingBillboard>()
                            .SetInfo(userDto.name, (float) userDto.hp/userDto.maxHp, (float) userDto.mp/userDto.maxMp,
                                userDto.level);

                        break;
                    case InventoryType.Box:
                        break;
                }
            }
        }
    }
    private void UnUseInventory(InventoryItemDTO itemdto)
    {
        if (itemdto != null)
        {
            UserDTO userDto = IdToUserDtoDic[itemdto.userId] as UserDTO;
            GameObject model = idToGameObjectDic[itemdto.userId];
            Info info = model.GetComponent<Info>();
            switch (itemdto.inventory.inventoryType)
            {
                case InventoryType.Equip:
                    userDto.attack -= itemdto.attack;
                    userDto.def -= itemdto.def;
                    userDto.armour -= itemdto.armour;
                    userDto.crit -= itemdto.crit;
                    userDto.exemptCrit -= itemdto.exemptCrit;
                    userDto.maxHp -= itemdto.hp;
                    userDto.maxMp -= itemdto.mp;
                    userDto.speed -= itemdto.speed;
                    info.UserDto = userDto;
                    if (userDto.id == GameData.UserDto.id) //使用者是自己的话，更改ui界面的数据
                    {
                        GameData.UserDto = userDto;
                        GameData.SetEquips((int)itemdto.inventory.equipType, 0);
                        Knapsack._instance.AddInventory(itemdto);
                    }
                    else
                    {
                        userDto.equips[(int)itemdto.inventory.equipType] = 0;
                    }
          
                    break;
            }
        }
    }
    void OnDestroy()
    {
        pos.GetPos -= GetPos;
        map.EnterScene -= EnterScene;
        map.LeaveScene -= LeaveScene;
        map.EnterSceneBRO -= EnterSceneBro;
        map.LeaveSceneBRO -= LeaveSceneBro;
        map.AttackBro -= AttackBro;
        map.SkillBro -= SkillBro;
        map.UseInventory -= UseInventory;
        map.UnUseInventory -= UnUseInventory;
        map.ReviveBro -= ReviveBro;
        map.Kill -= Kill;

        map.DamageBro -= DamageBro;
        map.Move -= Move;

        

    }


    void GetPos(PosDto posDto)
    {
        if (posDto != null)
        {
            spawan.position = new Vector3(posDto.posx, posDto.posy, posDto.posz);
            spawan.eulerAngles = new Vector3(posDto.rotax, posDto.rotay, posDto.rotaz);
        }
    }

     
    /// <summary>
    /// 服务器返回场景角色列表，并创建角色
    /// </summary>
    /// <param name="userDtos"></param>
    void EnterScene(List<AbsRoleModel> userDtos)
    {
        if (GameData.UserDtos.Count > 0) GameData.UserDtos.Clear();
        if(GameData.ModeList.Count>0)GameData.ModeList.Clear();
        GameData.UserDtos = userDtos;
        for (int i = 0; i < userDtos.Count; i++)
        {
            switch (userDtos[i].modelName)
            {
                case ModelName.LichModel:
                    GameObject model=null;
                    if (lichModelPool.Count > 0)
                    {
                        model = lichModelPool.Pop();
                        model.SetActive(true);
                    }
                    else
                    {
                        model = Instantiate(Resources.Load<GameObject>("Model/" + userDtos[i].modelName));
                    }
                    if (userDtos[i].id == GameData.UserDto.id)
                    {
                        GameData.UserDto = (UserDTO)userDtos[i];
                        GameData.play = model;
                        model.transform.position = spawan.position;
                        model.transform.eulerAngles = spawan.eulerAngles;
                        Camera.main.transform.position = model.transform.position + new Vector3(0, 6, -5);
                        Camera.main.transform.eulerAngles = new Vector3(45, 0, 0);
                        Camera.main.GetComponent<myCamera>().SetPlay(model);
                    }
                    idToGameObjectDic.Add(userDtos[i].id, model); //根据id保存到字典里
                    IdToUserDtoDic.Add(userDtos[i].id,userDtos[i]);
                    GameData.ModeList.Add(model);
                    // 后续给模型赋值
                    model.GetComponent<Info>().Init(userDtos[i].id ,userDtos[i]);
                    if (userDtos[i].id >= 0)
                    {
                        UserDTO userDto =userDtos[i] as UserDTO;
                        model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp, (float)userDto.mp / userDto.maxMp,userDto.level);
                    }
                    else
                    {
                        model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDtos[i].name, (float)userDtos[i].hp / userDtos[i].maxHp);
                    }
                    break;
            }
        }
      
        // 向服务器发送位置同步信息
        SyncPos();
    }




    /// <summary>
    /// 自身离开场景
    /// </summary>
    void LeaveScene()
    {
        GameData.lastScene = SceneManager.GetActiveScene().buildIndex;
        Joystick._instance.SetPos();
        SceneManager.LoadScene(GameData.wantLoadScene);
    }


    /// <summary>
    /// 服务器返回有人进入场景
    /// </summary>
    /// <param name="userDto"></param>
    void EnterSceneBro(UserDTO userDto)
    {
        if (!GameData.UserDtos.Contains(userDto))
        {
            GameData.UserDtos.Add(userDto);
            if (!idToGameObjectDic.ContainsKey(userDto.id))
            {
                GameObject model = null;               
                switch (userDto.modelName)
                {
                    case ModelName.LichModel:
                        if (lichModelPool.Count > 0)
                        {
                            model = lichModelPool.Pop();
                            model.SetActive(true);
                        }
                        else
                        {
                            model = Instantiate(Resources.Load<GameObject>("Model/" + userDto.modelName));
                        }
                        idToGameObjectDic.Add(userDto.id, model);//根据id保存到字典里
                        IdToUserDtoDic.Add(userDto.id, userDto);
                        GameData.ModeList.Add(model);
                        break;
                }
                // 后续给模型赋值
                if (model != null)
                {
                    model.GetComponent<Info>().Init(userDto.id,userDto);
                    if (userDto.id >= 0)
                    {
                        model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp, (float)userDto.mp / userDto.maxMp, userDto.level);
                    }
                    else
                    {
                        model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp);
                    }
                }
            }
            // 向服务器发送位置同步信息
           SyncPos();
        }     
    }


    /// <summary>
    /// 服务器返回有人离开场景
    /// </summary>
    /// <param name="userDtoId"></param>
    void LeaveSceneBro(int userDtoId)
    {
        if (IdToUserDtoDic.ContainsKey(userDtoId))
        {
            if (idToGameObjectDic.ContainsKey(userDtoId))
            {
                GameObject model = idToGameObjectDic[userDtoId].gameObject;
               
                model.SetActive(false);
                switch (IdToUserDtoDic[userDtoId].modelName)
                {
                    case ModelName.LichModel:
                        lichModelPool.Push(model);
                        break;
                }
                idToGameObjectDic.Remove(userDtoId);
                GameData.UserDtos.Remove(IdToUserDtoDic[userDtoId]);
                GameData.ModeList.Remove(model);
                IdToUserDtoDic.Remove(userDtoId);
            }
        }
       
    }


    /// <summary>
    /// 同步位置
    /// </summary>
    void SyncPos()
    {
        MoveDto moveDto = new MoveDto();
        GameObject self = GameData.play;
        moveDto.userId = GameData.UserDto.id;
        moveDto.posx = self.transform.position.x;
        moveDto.posy = self.transform.position.y;
        moveDto.posz = self.transform.position.z;
        moveDto.rotax = self.transform.eulerAngles.x;
        moveDto.rotay = self.transform.eulerAngles.y;
        moveDto.rotaz = self.transform.eulerAngles.z;
        moveDto.dirx = 0;
        moveDto.diry = 0;
        moveDto.dirz = 0;
        NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Move_CREQ, moveDto);
    }


    void AttackBro(AttackDTO attackDto)
    {
        int userId = attackDto.userId;
        int[] targetsId = attackDto.targetsId;
        List<Transform> targets=new List<Transform>();
        for (int i = 0; i < targetsId.Length; i++)
        {
            targets.Add(idToGameObjectDic[targetsId[i]].transform);
        }
        idToGameObjectDic[userId].GetComponent<Info>().Attack(targets.ToArray());
    }

    void SkillBro(SkillAttackDTO skillAttackDto)
    {
        if (skillAttackDto == null)
        {
            WarrningManager.warringList.Add(new WarringModel("能量不足", null, 2));
            return;
        }

        int userId = skillAttackDto.userId;
        if (userId == GameData.UserDto.id)
        {
            GameData.SetHpMp(0, -skillAttackDto.skillDto.mp);
            GameData.play.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(GameData.UserDto.name, (float)GameData.UserDto.hp / GameData.UserDto.maxHp, (float)GameData.UserDto.mp / GameData.UserDto.maxMp);
            CanvasManage._instance.shortcuts[skillAttackDto.skillDto.shortcutId - 1].SetCold();//进入冷却
        }
        else
        {
            GameObject model = idToGameObjectDic[userId];
            UserDTO userDto = (UserDTO) IdToUserDtoDic[userId];
            userDto.mp -= skillAttackDto.skillDto.mp;
            if (userDto.mp <= 0)
            {
                userDto.mp = 0;
            }
            model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp, (float)userDto.mp / userDto.maxMp);
        }

        int[] targetsId = skillAttackDto.targetsId;
        List<Transform> targets = new List<Transform>();
        for (int i = 0; i < targetsId.Length; i++)
        {
            targets.Add(idToGameObjectDic[targetsId[i]].transform);
        }
        idToGameObjectDic[userId].GetComponent<Info>().Skill(targets.ToArray(),skillAttackDto);


    }
    /// <summary>
    /// 服务器返回移动
    /// </summary>
    /// <param name="moveDto"></param>
    void Move(MoveDto moveDto)
    {
        if (idToGameObjectDic.ContainsKey(moveDto.userId))
        {           
                GameObject model = idToGameObjectDic[moveDto.userId];
                Vector3 pos = new Vector3(moveDto.posx, moveDto.posy, moveDto.posz);
                Vector3 rota = new Vector3(moveDto.rotax, moveDto.rotay, moveDto.rotaz);
                Vector3 dir = new Vector3(moveDto.dirx, moveDto.diry, moveDto.dirz);
                model.GetComponent<SyncMove>().SetMove(pos, rota, dir);
        }
    }

    private void DamageBro(DamageDTO damagedto)//damagedto.targets[i][0]//0为被攻击者id,1为收到伤害的值，2被攻击者是否死亡，3是否触发暴击
    {
        for (int i = 0; i < damagedto.targets.Length; i++)
        {
            UserDTO userDto = IdToUserDtoDic[damagedto.targets[i][0]]as UserDTO;
            GameObject model = idToGameObjectDic[damagedto.targets[i][0]];
            if (userDto != null)
            {
             
                if (userDto.id >= 0)//受到伤害的是否为玩家
                {
                    if (userDto.id == GameData.UserDto.id)//受到攻击为本人，更新ui界面的数据
                    {
                        GameData.SetHpMp(-damagedto.targets[i][1]);
                    }
                    else
                    {
                        userDto.hp -= damagedto.targets[i][1];
                        if (userDto.hp <= 0) userDto.hp = 0;
                    }
                    //更新玩家血条数据
                    model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp, (float)userDto.mp / userDto.maxMp);
                }
                else
                {
                    model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp);
                }
                Info info = model.GetComponent<Info>();
                InitFallBoold(info, damagedto.targets[i][1], Color.red, damagedto.targets[i][3]);//实例化掉血
                AnimatorManage animatorManage = model.GetComponent<AnimatorManage>();
                info.state = AnimState.Control;
                animatorManage.SetInt("state", (int)AnimState.Control);
                if (damagedto.targets[i][2] == 0 || userDto.hp<=0)
                {
                    
                    if (userDto.id == GameData.UserDto.id)//自己死亡，显示复活时间和开启ui遮罩
                    {
                        mask.gameObject.SetActive(true);
                        fuhuoText.gameObject.SetActive(true);
                        timer = 11;
                        isFuhuo = true;
                    }                
                    info.state = AnimState.Die;
                    animatorManage.SetInt("state", (int)AnimState.Die);
                }
            }
        }
      
    }

    private void Kill(UserDTO userDto)
    {
        if (userDto != null)
        {
            IdToUserDtoDic[userDto.id] = userDto;
            GameObject model = idToGameObjectDic[userDto.id];
            model.transform.Find("infoUi").GetComponent<CameraFacingBillboard>().SetInfo(userDto.name, (float)userDto.hp / userDto.maxHp, (float)userDto.mp / userDto.maxMp,userDto.level);
            if (userDto.id == GameData.UserDto.id)
            {
                GameData.UserDto = userDto;
                GameData.SavaHpMp(userDto.hp,userDto.mp);
            }
        }
      
    }
    private void ReviveBro(int id)
    {
        UserDTO userDto=IdToUserDtoDic[id]as UserDTO;
        GameObject go = idToGameObjectDic[id];
        if (userDto != null)
        {
            userDto.hp = (int) (userDto.maxHp*0.5);
            userDto.mp = (int) (userDto.maxMp*0.5);
            Info info = go.GetComponent<Info>();
            AnimatorManage animatorManage = go.GetComponent<AnimatorManage>();
            info.state = AnimState.Idle;
            animatorManage.SetInt("state", info.state);
            go.transform.Find("infoUi")
                .GetComponent<CameraFacingBillboard>()
                .SetInfo(userDto.name, (float) userDto.hp/userDto.maxHp, (float) userDto.mp/userDto.maxMp);
            go.transform.position = fuhuoTransform.position;


            if (id == GameData.UserDto.id)
            {
                isFuhuo = false;
                mask.gameObject.SetActive(false);
                fuhuoText.gameObject.SetActive(false);
                GameData.SavaHpMp(userDto.hp, userDto.mp);
            }

        }
    }
    void InitFallBoold(Info info, int text, Color addBlood, int crit = 0)
    {
        GameObject go;
        if (fallbloodPool.Count > 0)//实现掉血
        {
            go = fallbloodPool.Pop();//从对象池里面获取掉血载体
           
        }
        else
        {
            go = Instantiate(fallBloodPrefab);//实例化载体
        }
        if (go != null)
        {
            go.transform.SetParent(info.fallbloodSpawn);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<CameraFacingBillboard>().SetText(text, addBlood, crit);//赋值
        }
         

    }

    private bool isFuhuo = false;
    private float timer;
    void Update()
    {
        if (isFuhuo)
        {
            timer -= Time.deltaTime;
            if (timer <= 1)
            {
                timer = 1;
                isFuhuo = false;
            }
            fuhuoText.text = ((int)timer)+"秒后复活";

        }
    }
}
