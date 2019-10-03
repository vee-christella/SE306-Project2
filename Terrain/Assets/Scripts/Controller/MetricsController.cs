using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsController : MonoBehaviour
{

    public Text coinCount;
    public Text greenCount;
    public Text happinessCount;
    public Text currentTurn;
    public Text maxTurn;

    // Start is called before the first frame update
    void Start()
    {
        coinCount.text = "200";
        greenCount.text = "0";
        happinessCount.text = "50";
        currentTurn.text = "0";
        maxTurn.text = "50";  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMetrics(float coin, float green, float happiness)
    {
        coinCount.text = coin.ToString();
        greenCount.text = green.ToString();
        happinessCount.text = happiness.ToString();
    }
}
