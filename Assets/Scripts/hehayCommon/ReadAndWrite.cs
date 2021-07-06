using komal;
using UnityEngine;

public class ReadAndWrite
{
    // <summary>
    /// 保存数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <param name="data"></param>
    public static void SaveData<T>(string fileName, T data)
    {
        fileName += ".json";
        if (KomalUtil.Instance.IsFileExistInPersistentDataPath(fileName))
        {
            //存在文件，就删除
            KomalUtil.Instance.RemoveFile(fileName);
        }

        KomalUtil.Instance.WriteToPersistentData(fileName, data);
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T LoadData<T>(string fileName)
    {
        fileName += ".json";
        //判断是否存在文件
        if (KomalUtil.Instance.IsFileExistInPersistentDataPath(fileName))
        {
            //存在 读取
            return KomalUtil.Instance.ReadFromPersistentData<T>(fileName);
        }
        return default;
    }
}
