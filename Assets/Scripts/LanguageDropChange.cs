
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageDropChange : MonoBehaviour
{
    //Dropdownを格納する変数
    [SerializeField] private TMP_Dropdown dropdown;

    public void Change()
    {
        var _ = ChangeSelectedLocale();
    }

    private async Task ChangeSelectedLocale()
    {
        Debug.Log("Localization Result : " + dropdown.value);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
        await LocalizationSettings.InitializationOperation.Task;
        ////DropdownのValueが0のとき（日本語）
        //if (dropdown.value == 0)
        //{
        //    //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
        //    //await LocalizationSettings.InitializationOperation.Task;
        //}
        ////DropdownのValueが1のとき（中国語（簡体字））
        //else if (dropdown.value == 1)
        //{
        //    LocalizationSettings.SelectedLocale = Locale.CreateLocale("ch");
        //    await LocalizationSettings.InitializationOperation.Task;
        //}
        ////DropdownのValueが2のとき（英語）
        //else if (dropdown.value == 2)
        //{
        //    LocalizationSettings.SelectedLocale = Locale.CreateLocale("en");
        //    await LocalizationSettings.InitializationOperation.Task;
        //}
    }
}
