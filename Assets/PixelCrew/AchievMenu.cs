using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievMenu : MonoBehaviour
{
    public int _money;
    public int _totalMoney;
    [SerializeField] bool _isfirst;

    public string[] _arrayTitles;
    public Sprite[] _arraySrites;
    public GameObject _button;
    public GameObject _content;

    private List<GameObject> _list = new List<GameObject>();
    private VerticalLayoutGroup _group;

    public void Start()
    {
        _money = PlayerPrefs.GetInt("money");
        _totalMoney = PlayerPrefs.GetInt("totalMoney");
        _isfirst = PlayerPrefs.GetInt("isFirst") == 1 ? true : false;

        RectTransform rectT = _content.GetComponent<RectTransform>();
        rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        _group = GetComponent<VerticalLayoutGroup>();
        setAchievs();
    }

    private void RemoveList()
    {
        foreach (var item in _list)
        {
            Destroy(item);
        }
        _list.Clear();
    }

    private void setAchievs()
    {
        RectTransform rectT = _content.GetComponent<RectTransform>();
        rectT.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        RemoveList();
        if(_arrayTitles.Length > 0)
        {
            var pr1 = Instantiate(_button, transform);
            var h = pr1.GetComponent<RectTransform>().rect.height;
            var tr = GetComponent<RectTransform>();
            tr.sizeDelta = new Vector2(tr.rect.width, h * _arrayTitles.Length);
            Destroy(pr1);
            for (var i = 0; i < _arrayTitles.Length; i++)
            {
                var pr = Instantiate(_button, transform);
                pr.GetComponentInChildren<Text>().text = _arrayTitles[i];
                pr.GetComponentsInChildren<Image>()[1].sprite = _arraySrites[i];
                var i1 = i;
                pr.GetComponent<Button>().onClick.AddListener(() => GetAchievement(i1));
            }
        }
    }

    public void GetAchievement(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log(id);
                break;
            case 1:
                Debug.Log(id);
                _money += 5;
                PlayerPrefs.SetInt("money", _money);
                break;
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
