using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    [SerializeField]
    GameObject desti;

    [SerializeField]
    GameObject target;

    void Start () {
        StartCoroutine(three(2f));

	}
	
    IEnumerator two()
    {
        float t = 0;
        float value = 5f;

        while(t < 10)
        {
            transform.position = new Vector3(transform.position.x + t, transform.position.y + (Mathf.Sin(t) * value), 0);
            t += Time.deltaTime;
            Debug.Log(t);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    IEnumerator one()
    {
        Vector3 pos = transform.position;
        Vector3 destination = desti.transform.position;
        float t = 0;

        float x, y;
        x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0, 0)).x - pos.x;
        y = (target.transform.position.y - pos.y) / 2;

        while(t < (Mathf.PI / 2))
        {
            transform.position = new Vector3(pos.x + x * t / 1.7f, pos.y + y * Mathf.Sin(t));

            t += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    IEnumerator three(float duration)
    {
        Vector3 pos = transform.position;
        Vector3 destination = desti.transform.position;
        float t = 0;
        float v = 3f;

        float x, y;
        x = (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0, 0)).x - pos.x + 2 * v) / 2;
        y = (target.transform.position.y - pos.y) / 2;

        while (t <= duration)
        {
            t += Time.deltaTime;
            float sx = pos.x + x * t - 0.5f * v * t * t;
            float sy = pos.y + y * t;
            transform.position = new Vector3(sx, sy);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    IEnumerator four(float duration)
    {
        Vector3 s = transform.position;
        Vector3 destination = desti.transform.position;
        float t = 0;

        float ex = (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0, 0)).x - s.x) / 2;
        float ey = (target.transform.position.y - s.y) / 2;

        while (t <= duration)
        {
            t += Time.deltaTime;
            float y = (ey - s.y) / (t - 0.5f * t * t);
            float x = ey - s.y / t;
            transform.position = new Vector3(s.x + x, s.y + y);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
