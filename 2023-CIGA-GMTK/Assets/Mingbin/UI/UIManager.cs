using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
private Transform canvasTf;

    private List<GameObject> uiList;//存储加载过的界面的集合

    public void Init()
    {
        //找世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        //初始化集合
        uiList = new List<GameObject>();
    }

    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="uiName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ShowUI<T>(string uiName) where T : Component
    {
        T ui = Find<T>(uiName);
        if (ui == null)
        {
            //集合中没有 需要从Resources/UI文件夹中加载
            GameObject obj = Object.Instantiate(Resources.Load("UI/" + uiName), canvasTf) as GameObject;

            //改名字
            obj.name = uiName;

            //添加需要的脚本
            ui = obj.AddComponent<T>();
            uiList.Add(obj);
        }
        else
        {
            //显示
            ui.gameObject.SetActive(true);
        }

        return ui;
    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    /// <param name="uiName"></param>
    public void HideUI(string uiName)
    {
        GameObject ui = Find(uiName);
        if (ui != null)
        {
            ui.SetActive(true);
        }
    }

    /// <summary>
    /// 关闭所有界面
    /// </summary>
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i >= 0; i--)
        {
            Object.Destroy(uiList[i].gameObject);
        }

        uiList.Clear();//清空集合
    }

    /// <summary>
    /// 关闭某个界面
    /// </summary>
    /// <param name="uiName">界面名称</param>
    public void CloseUI(string uiName)
    {
        GameObject ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            Object.Destroy(ui.gameObject);
        }
    }

    /// <summary>
    /// 从集合中找到名字对应的界面脚本
    /// </summary>
    /// <param name="uiName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Find<T>(string uiName) where T : Component
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i].GetComponent<T>();
            }
        }
        return null;
    }

    public GameObject Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 获得某个界面的脚本
    /// </summary>
    /// <param name="uiName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetUI<T>(string uiName) where T : Component
    {
        GameObject ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;
    }
}
