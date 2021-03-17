using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Undue : MonoBehaviour
{

    void Awake()
    {
        GameData.lastScene = SceneManager.GetActiveScene().buildIndex;
        //TODO 向服务器获取数据
        SceneManager.LoadScene(GameData.UserDto.map);
    }
}


