using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Item> _Items = new List<Item>();
    [SerializeField] private Text _priceTxt;
    [SerializeField] private Text _goldTxt;

    GameObject currentPref;
    int gold;

    int PriceCount;

    public int count;

    private void OnEnable()
    {
        count = PlayerPrefs.GetInt("Count");

        currentPref = Instantiate(_Items[count].Prefab);
        _priceTxt.text = $"Price: {_Items[count].price}";
        gold = PlayerPrefs.GetInt("Gold");
        _goldTxt.text = $"Gold = {gold}";

        for (int i = 0; i < _Items.Count; i++)
        {
            if (PlayerPrefs.GetInt($"{i}") != 1)
            {
                PlayerPrefs.GetInt($"{i}", 0);
            }
        }
    }

    private void OnDisable()
    {
        Destroy(currentPref.gameObject);
    }


    private void Update()
    {
        if (currentPref != null)
        {
            currentPref.transform.Rotate(Vector3.up * 75f * Time.deltaTime, Space.World);
        }
    }

    public void NextChangePlayer()
    {
        if (count >= 0 && count < _Items.Count - 1)
        {
            count++;
            Destroy(currentPref.gameObject);
            currentPref = Instantiate(_Items[count].Prefab);
            if (PlayerPrefs.GetInt($"{count}") == 0)
            {
                _priceTxt.text = $"Price: {_Items[count].price}";
            }
            else
            {
                _priceTxt.text = $"Price: {0}";
            }
        }
        else if(count == _Items.Count - 1)
        {
            count = 0;
            Destroy(currentPref.gameObject);
            currentPref = Instantiate(_Items[count].Prefab);
            if (PlayerPrefs.GetInt($"{count}") == 0)
            {
                _priceTxt.text = $"Price: {_Items[count].price}";
            }
            else
            {
                _priceTxt.text = $"Price: {0}";
            }
        }
    }

    public void PreviousChangePlayer()
    {
        if (count > 0 && count <= _Items.Count - 1)
        {
            count--;
            Destroy(currentPref.gameObject);
            currentPref = Instantiate(_Items[count].Prefab);
            if (PlayerPrefs.GetInt($"{count}") == 0)
            {
                _priceTxt.text = $"Price: {_Items[count].price}";
            }
            else
            {
                _priceTxt.text = $"Price: {0}";
            }
        }
        else if (count == 0)
        {
            count = _Items.Count - 1;
            Destroy(currentPref.gameObject);
            currentPref = Instantiate(_Items[count].Prefab);
            if (PlayerPrefs.GetInt($"{count}") == 0)
            {
                _priceTxt.text = $"Price: {_Items[count].price}";
            }
            else
            {
                _priceTxt.text = $"Price: {0}";
            }
        }
    }

    public void BuyPrefPlayer()
    {
        if (PlayerPrefs.GetInt("Gold") > _Items[count].price)
        {
            gold -= _Items[count].price;
            PlayerPrefs.SetInt("Gold", gold);
            PlayerPrefs.Save();
            _Items[count].price = 0;
            _priceTxt.text = $"Price: {_Items[count].price}";
            _goldTxt.text = $"Gold = {gold}";

            PlayerPrefs.SetInt("Count", count);
            PlayerPrefs.SetInt($"{count}", 1);
            PlayerPrefs.Save();
        }
    }
}
