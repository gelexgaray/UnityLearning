using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public float spawnRateSeconds = 2.0f;
    public float gravityFactor = 1.0f;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    void OnClick() 
    {
        Debug.Log($"'{button.name}' pressed");
        GameManager.Instance.StartGame(spawnRateSeconds);
    }

}
