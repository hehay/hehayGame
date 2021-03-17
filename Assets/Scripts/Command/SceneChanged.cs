using UnityEngine;
using Protocols;
using UnityEngine.SceneManagement;

public class SceneChanged : MonoBehaviour
{

    public int gotoScene;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TAGS.Player))
        {
            Info info = collider.GetComponent<Info>();
            if (info)
            {
                if (info.id == GameData.UserDto.id)
                {
                    GameData.wantLoadScene = gotoScene;
                    NetIO.Instance.Write(Protocol.Map, SceneManager.GetActiveScene().buildIndex, MapProtocol.LeaveMap_CREQ, null);
                }
            }
        }
    }
}
