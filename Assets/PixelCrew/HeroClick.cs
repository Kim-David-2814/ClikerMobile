using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HeroClick : MonoBehaviour
{
    [SerializeField] private int _money;
    public Text _moneyText;
    public Text[] _costText;
    public int[] _costInt;
    public int[] _costBonus;
    public int _totalMoney;
    private int _clickScore = 1;

    private Save _sv = new Save();
    public GameObject _shopPan;
    public GameObject _bonusPan;
    public GameObject _achievePan;
    public GameObject _settingPan;

    public GameObject _achieve1Button;
    public GameObject _achieve2Button;
    public GameObject _achieve3Button;

    [Header("Достижения")]
    private int _achievement1Max;

    private bool _isAchievement1 = true;
    private bool _isAchievement2 = true;
    private bool _isAchievementGet2 = false;
    private bool _isAchievement3 = true;
    private bool _isAchievementGet3 = false;

    public Text[] _achievementText;
    public Text[] _achievementCost;
    public Text _achievemenstNameText;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            _sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            _money = _sv._money;
            _clickScore = _sv._clickScore;

            _achievement1Max = _sv._achievement1Max;
            _isAchievement1 = _sv._isAchievement1;

            _isAchievement2 = _sv._isAchievement2;
            _isAchievementGet2 = _sv._isAchievementGet2;

            _isAchievement3 = _sv._isAchievement3;
            _isAchievementGet3 = _sv._isAchievementGet3;

            for (int i = 0; i < 1; i++)
            {
                _costBonus[i] = _sv._costBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                _costInt[i] = _sv._costInt[i];
                _costText[i].text = _sv._costInt[i] + "$";
            }
        }
    }

    public void Start()
    {
        _totalMoney = PlayerPrefs.GetInt("totalMoney");
        StartCoroutine(BonusBuy());
    }

    public void ButtonClik()
    {
        _money += _clickScore;
        if (_isAchievement1 == true && _achievement1Max < 100)
        {
            _achievement1Max++;
        }
        _totalMoney++;
        PlayerPrefs.SetInt("money", _money);
        PlayerPrefs.SetInt("totalMoney", _totalMoney);
    }

    public void ToAchiev()
    {
        SceneManager.LoadScene(1);
    }

    public void Update()
    {
        _moneyText.text = _money + "$";
        _achievemenstNameText.text = "Нажмите " + _achievement1Max + "/100 раз";

        if (_isAchievement1 == false)
        {
            _achievementCost[0].text = "Получено";
            _achieve1Button.SetActive(false);
        }

        if (_achievement1Max == 100)
        {
            _achievementText[0].text = "Выполнено";
        }

        if (_isAchievementGet2 == true)
        {
            _achievementCost[1].text = "Получено";
            _achieve2Button.SetActive(false);
        }

        if (_isAchievement2 == false)
        {
            _achievementText[1].text = "Выполнено";
        }

        if (_isAchievementGet3 == true)
        {
            _achievementCost[2].text = "Получено";
            _achieve3Button.SetActive(false);
        }

        if (_isAchievement3 == false)
        {
            _achievementText[2].text = "Выполнено";
        }
    }

    public void ShopPanelShow()
    {
        _shopPan.SetActive(!_shopPan.activeSelf);
    }

    public void ShopPanelBonus()
    {
        _bonusPan.SetActive(!_bonusPan.activeSelf);
    }

    public void AchievePanelShow()
    {
        _achievePan.SetActive(!_achievePan.activeSelf);
    }

    public void SettingPanelShow()
    {
        _settingPan.SetActive(!_settingPan.activeSelf);
    }

    public void OnClickBuy()
    {
        if (_money >= _costInt[0])
        {
            _money -= _costInt[0];
            _costInt[0] *= 2;
            _clickScore *= 2;
            _costText[0].text = _costInt[0] + "$";
            _isAchievement2 = false;
        }
    }

    public void OnClickBonusBuy()
    {
        if (_money >= _costInt[1])
        {
            _money -= _costInt[0];
            _costInt[1] *= 2;
            _costBonus[0] += 2;
            _costText[1].text = _costInt[1] + "$";
            _isAchievement3 = false;
        }
    }

    IEnumerator BonusBuy()
    {
        while(true)
        {
            _money += _costBonus[0];
            yield return new WaitForSeconds(1);
        }
    }

    public void OnApplicationQuit()
    {
        _sv._money = _money;
        _sv._clickScore = _clickScore;
        _sv._costBonus = new int[1];
        _sv._costInt = new int[2];
        _sv._achievement1Max = _achievement1Max;
        _sv._isAchievement1 = _isAchievement1;
        _sv._isAchievement2 = _isAchievement2;
        _sv._isAchievement3 = _isAchievement3;
        _sv._isAchievementGet2 = _isAchievementGet2;
        _sv._isAchievementGet3 = _isAchievementGet3;

        for (int i = 0; i < 1; i++)
        {
            _sv._costBonus[i] = _costBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            _sv._costInt[i] = _costInt[i];
        }

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(_sv));
    }

    public void OnClicAchievementButton()
    {
        if (_isAchievement1 == true && _achievement1Max == 100)
        {
            _money += 100;
            _isAchievement1 = false; 
        }
    }

    public void OnClicAchievement2Button()
    {
        if (_isAchievement2 == false)
        {
            _money += 100;
            _isAchievement2 = false;
            _isAchievementGet2 = true;
        }
    }

    public void OnClicAchievement3Button()
    {
        if (_isAchievement3 == false)
        {
            _money += 100;
            _isAchievement3 = false;
            _isAchievementGet3 = true;
        }
    }
}

[Serializable]
public class Save
{
    public int _money;
    public int _clickScore;
    public int[] _costInt;
    public int[] _costBonus;

    public bool _isAchievement1;
    public bool _isAchievement2;
    public bool _isAchievementGet2;
    public bool _isAchievement3;
    public bool _isAchievementGet3;


    public int _achievement1Max;
}
