using UnityEngine;
using System.Collections;
using Protocols;
using Protocols.dto;
using UnityEngine.SceneManagement;


public class Move : MonoBehaviour
{

    private Vector3 oldVector3 = Vector3.zero;
    private bool isMove = false;

    

    void Awake()
    {
    }
	
	
	// Update is called once per frame
	void Update ()
	{

	    float x = Input.GetAxisRaw("Horizontal");
	    float y = Input.GetAxisRaw("Vertical");
	    if (x != 0 || y != 0)
	    {
            if (oldVector3 != new Vector3(x, 0, y))
            {
                NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Move_CREQ, GetMoveDto(x, y));
                isMove = true;
            }
	        
	    }
	    else
	    {
	        if (isMove)
	        {
                NetIO.Ins.Send(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.Move_CREQ, GetMoveDto(x, y));
	            isMove = false;
	        }            
	    }


	
	
	}

    MoveDto GetMoveDto(float x,float y)
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
        oldVector3 = new Vector3(x, 0, y);
        return moveDto;
    }

  
}
