using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ulit : MonoBehaviour
{
    public static bool Randomizer(float successProbability)
    {
        var a= Random.Range(0, 1000);
        return a < 1000 * successProbability;
    }

    public static void SetSpriteAlpha(ref SpriteRenderer spriteRenderer,float alpha)
    {
        if(spriteRenderer == null) return;
        var color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
    public static bool GetTopCollider2D(Camera targetCamera,Action<Collider2D> action = null,LayerMask layerMask = new())
    {
        if (DialogueManager.InDialogue) return false;
        //获取碰撞信息
        var results = new RaycastHit2D[10];
        var size = Physics2D.RaycastNonAlloc(targetCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,results,layerMask);
        var nResults = new RaycastHit2D[size];
            
        //清除空的RaycastHit2D
        for (int i = 0; i < size; i++)
        {
            nResults[i] = results[i];
        }
        if (size > 0)
        {
            //获取最上层的collider2D
            var collider2Ds = 
                from result in nResults
                orderby result.collider.GetComponent<SpriteRenderer>().sortingOrder descending 
                select result.collider;
            var topCollider = collider2Ds.ToArray()[0];
            action?.Invoke(topCollider);
            return true;
        }

        return false;
    }
}
