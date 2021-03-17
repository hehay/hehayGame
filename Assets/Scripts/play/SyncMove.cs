using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;


public class SyncMove : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 dir;
    private Vector3 oldSelfVector3 = Vector3.zero;
    private Vector3 oldSelfRota = Vector3.zero;
    private Info info;
    private AnimatorManage animatorManage;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        info = GetComponent<Info>();
        animatorManage = GetComponent<AnimatorManage>();
        InvokeRepeating("Sync", 0, 0.1f);
    }

   
    // Update is called once per frame
    private void Update()
    {

        if (dir != Vector3.zero && info.state != AnimState.Attack && info.state != AnimState.Skill1 && info.state != AnimState.Skill2 && info.state != AnimState.Skill2 && info.state != AnimState.Control)
        {
            cc.SimpleMove(dir * info.UserDto.speed);
            if (info.state != AnimState.Run)
            {
                info.state = AnimState.Run;
                animatorManage.SetInt("state",(int)info.state);
            }
        }
     
    }

    public void SetMove(Vector3 pos, Vector3 rota, Vector3 dir)
    {
        this.dir = dir;

        if (dir == Vector3.zero)
        {
            transform.position = pos;
            transform.eulerAngles = rota;
            info.state = AnimState.Idle;
            animatorManage.SetInt("state", (int) info.state);

        }
        transform.LookAt(transform.position+dir);
    }

    void Sync()
    {
        if (oldSelfVector3 != transform.position || oldSelfRota != transform.eulerAngles)
        {
            oldSelfRota = transform.eulerAngles;
            oldSelfVector3 = transform.position;
            PosDto posDto = new PosDto(GameData.UserDto.id, transform.position.x, transform.position.y,
                transform.position.z, transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            NetIO.Instance.Write(Protocol.Pos, 0, PosProtocol.UpdatePos_CREQ, posDto);

        }
    }
}
