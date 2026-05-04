using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("시간 관리")]
    public int currentDay = 1;      // 현재 날짜 (1일차)
    public int currentHour = 8;     // 현재 시간 (아침 8시)

    [Header("플레이어 스탯")]
    public int maxStamina = 100;    // 최대 스태미너
    public int currentStamina = 100;// 현재 스태미너
    public int currentMoney = 0;    // 소지금
    public int charmStat = 0;       // 매력도

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 시간을 흐르게 하고, 24시가 넘어가면 다음 날로 변경하는 핵심 함수입니다.
    /// </summary>
    /// <param name="hoursToPass">흐르게 할 시간</param>
    private void PassTime(int hoursToPass)
    {
        currentHour += hoursToPass;

        if (currentHour >= 24)
        {
            currentDay++;
            currentHour -= 24;
            Debug.Log($"🌙 하루가 지났습니다! 현재 {currentDay}일차입니다.");
        }
        
        Debug.Log($"현재 시간: {currentHour}시");
    }

    // ==========================================
    // 🎯 플레이어 행동 (Action) 함수들
    // ==========================================

    public void DoPartTimeJob()
    {
        int requiredStamina = 30; // 알바에 필요한 스태미너
        int timeCost = 6;         // 소모 시간
        int moneyReward = 50000;  // 알바비

        if (currentStamina >= requiredStamina)
        {
            currentStamina -= requiredStamina;
            currentMoney += moneyReward;
            Debug.Log($"💰 알바 완료! 소지금이 {moneyReward}원 증가했습니다.");
            
            PassTime(timeCost);
        }
        else
        {
            Debug.Log("⚠️ 스태미너가 부족해서 알바를 할 수 없습니다!");
        }
    }

    public void DoSleep()
    {
        int timeCost = 8;         // 수면 시간

        currentStamina = maxStamina;
        Debug.Log("💤 잠을 잤습니다. 스태미너가 모두 회복되었습니다.");

        PassTime(timeCost);
    }
    
    public void DoSelfImprovement()
    {
        int requiredStamina = 20;
        int timeCost = 3;
        int charmReward = 5;

        if (currentStamina >= requiredStamina)
        {
            currentStamina -= requiredStamina;
            charmStat += charmReward;
            Debug.Log($"✨ 자기 계발 완료! 매력도가 {charmReward} 상승했습니다.");

            PassTime(timeCost);
        }
        else
        {
            Debug.Log("⚠️ 스태미너가 부족해서 자기 계발을 할 수 없습니다!");
        }
    }
}
