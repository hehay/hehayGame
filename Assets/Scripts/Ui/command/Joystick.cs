using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocols;
using Protocols.dto;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Joystick _instance;
    private Vector3 dir;
    private Vector3 originPosition = Vector3.zero;
    [SerializeField]
    private float radius;
    private Vector3 oldVector3 = Vector3.zero;

    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPos()
    {
        transform.position = originPosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition=Vector3.zero;
        originPosition = transform.position;//记录原点位置
    }

    public void OnDrag(PointerEventData eventData)
    {
        dir = (Input.mousePosition - originPosition).normalized;
        transform.position = Input.mousePosition;
        if (Vector3.Distance(transform.position, originPosition) >= radius)
        {
            transform.position = dir * radius + originPosition;
        }
        float vertical = (transform.position.y - originPosition.y) / radius;
        float horizontal = (transform.position.x - originPosition.x) / radius;

        if (oldVector3 != new Vector3(horizontal, 0, vertical))
        {
            oldVector3 = new Vector3(horizontal, 0, vertical);
            Vector3 v = oldVector3.normalized;
            Send(v.x, v.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        transform.position = originPosition;
        Send(0,0);
    }

    void Send(float vertical, float horizontal)
    {
        if (GameData.play == null) return;
        Info info = GameData.play.GetComponent<Info>();
        if (info != null)
        {
            if (info.state != AnimState.Die && info.state != AnimState.Control)
                NetIO.Instance.Write(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Move_CREQ,
                    GetMoveDto(vertical, horizontal));
        }
    }
    MoveDto GetMoveDto(float x, float y)
    {
        Transform model = GameData.play.transform;
        MoveDto moveDto = new MoveDto();
        moveDto.userId = GameData.UserDto.id;
        moveDto.posx = model.position.x;
        moveDto.posy = model.position.y;
        moveDto.posz = model.position.z;
        moveDto.rotax = model.eulerAngles.x;
        moveDto.rotay = model.eulerAngles.y;
        moveDto.rotaz = model.eulerAngles.z;
        moveDto.dirx = x;
        moveDto.diry = 0;
        moveDto.dirz = y;
        return moveDto;
    }
}

