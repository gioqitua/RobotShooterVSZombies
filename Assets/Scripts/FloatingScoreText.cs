using TMPro;
using UnityEngine;

public class FloatingScoreText : MonoBehaviour
{
    [SerializeField] float _floatSpeed = 5f;

    public void SetScoreValue(int multiplier)
    {
        var text = GetComponent<TMP_Text>();
        text.SetText("x " + multiplier);

        if (multiplier < 3)
        {
            text.color = Color.white;
        }
        else if (multiplier < 10)
        {
            text.color = Color.green;
        }
        else if (multiplier < 20)
        {
            text.color = Color.yellow;
        }
        else if (multiplier < 30)
        {
            text.color = Color.red;
        }
        else if (multiplier < 50)
        {
            text.color = Color.magenta;
        }
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * _floatSpeed;
    }

}