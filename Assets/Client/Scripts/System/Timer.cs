//using UnityEngine;
//using UnityEngine.UI;

//public class Timer : MonoBehaviour
//{

//    [SerializeField] private float timerStart;
//    [SerializeField] private Text timerUI;
//    [SerializeField] private GameObject TimerPanel;
//    private bool check = false;
//    private LargeAsteroidPool largeAsteroidPool;

//    void Start()
//    {
//        largeAsteroidPool = GameObject.FindWithTag("LargeAsteroidPool").GetComponent<LargeAsteroidPool>();
//        timerStart = largeAsteroidPool.delayTimer;

//        timerUI.text = timerStart.ToString();
//        TimerPanel.SetActive(false);
//    }

//    void Update()
//    {
//        TimeToBegin();
//    }

//    public void CheckEvent()
//    {
//        check = true;
//        TimerPanel.SetActive(true);
//    }


//    public void TimeToBegin()
//    {
        

//        if (timerStart<=0)
//        {
//            check = false;
//            TimerPanel.SetActive(false);
//            return;
//        }
//        timerStart -= Time.deltaTime;
//        timerUI.text = Mathf.Round(timerStart).ToString();

//    }
//}
