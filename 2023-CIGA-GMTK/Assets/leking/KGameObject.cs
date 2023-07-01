using System.Collections;
using UnityEngine;

public class KGameObject : MonoBehaviour
{
    private Transform _transform;
    private GameObject _gameObject;

    // ReSharper disable once InconsistentNaming
    public new Transform transform
    {
        get => _transform;
        set => _transform = value;
    }
    // ReSharper disable once InconsistentNaming
    public new GameObject gameObject
    {
        get => _gameObject;
        set => _gameObject = value;
    }

    /// <summary>
    /// 移动到指定位置(具有动画过渡)
    /// </summary>
    /// <param name="pos">目标位置</param>
    public void MoveTo(Vector3 pos)
    {
        StopCoroutine(nameof(AnimeMove));
        _endPos = pos;
        StartCoroutine(nameof(AnimeMove));
    }

    /// <summary>
    /// 旋转到指定欧拉角(具有动画过渡)
    /// </summary>
    /// <param name="rot">目标欧拉角</param>
    public void RotateTo(Vector3 rot)
    {
        StopCoroutine(nameof(AnimeRotate));
        _endRot = rot;
        StartCoroutine(nameof(AnimeRotate));
    }
    public  T AddComponent<T>() where T : Component
    {
        return _gameObject.AddComponent<T>();
    }

    public new T GetComponent<T>() where T : Component
    {
        return _gameObject.GetComponent<T>();
    }
    public void SetActive(bool value)
    {
        _gameObject.SetActive(value);
    }
    public static KGameObject Instantiate(GameObject original)
    {
        var o = GameObject.Instantiate(original);
        var ko = o.AddComponent<KGameObject>();
        ko._transform = o.transform;
        ko._gameObject = o.gameObject;
        return ko;
    }
    
    private Vector3 _endPos;
    private Vector3 _endRot;
    
    private IEnumerator AnimeMove()
    {
        var initPos = _transform.position;
        for (var i = 0; i < 60; i++)
        {
            _transform.position = Vector3.Lerp(initPos, _endPos, (float)i / 9);
            yield return new WaitForSeconds(1.0f/60);
        }
    }
    
    private IEnumerator AnimeRotate()
    {
        var initRot = new Vector3(
            _transform.eulerAngles.x>180?_transform.eulerAngles.x-360:_transform.eulerAngles.x,
            _transform.eulerAngles.y>180?_transform.eulerAngles.y-360:_transform.eulerAngles.y,
            _transform.eulerAngles.z>180?_transform.eulerAngles.z-360:_transform.eulerAngles.z);
        print($"{initRot}|{_endRot}");
        for (var i = 0; i < 60; i++)
        {
            _transform.eulerAngles = Vector3.Lerp(initRot, _endRot, (float)i / 9);
            yield return new WaitForSeconds(1.0f/60);
        }
    }
}