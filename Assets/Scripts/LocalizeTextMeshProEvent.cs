using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Localize
{
    /// <summary>
    /// TextMeshProUGUIをローカライズするためのコンポーネント
    /// </summary>
    public class LocalizeTextMeshProEvent : MonoBehaviour
    {
        [Serializable]
        public class StringUnityEvent : UnityEvent<string> { };
        [SerializeField]
        private LocalizedString _localizedString = new LocalizedString();
        [SerializeField]
        private StringUnityEvent _updateString = new StringUnityEvent();
        [SerializeField]
        private int _argument = 0;

        /// <summary>
        /// Localizeされたテキストが更新されたときに呼ばれるコールバック
        /// </summary>
        public StringUnityEvent OnUpdateString
        {
            get => _updateString;
            set => _updateString = value;
        }

        /// <summary>
        /// LocalizedStringに変更があったら、Textを更新する
        /// </summary>
        /// <param name="value">更新されたテキスト内容</param>
        private void UpdateString(string value)
        {
            OnUpdateString.Invoke(value);
        }

        /// <summary>
        /// LocalizedStringに変更があった際に呼ぶイベントを登録する
        /// </summary>
        private void RegisterChangeHandler()
        {
            _localizedString.Arguments = new object[] { _argument };
            _localizedString.StringChanged += UpdateString;
        }

        /// <summary>
        /// LocalizedStringに変更があった際に呼ぶイベントを解除する
        /// </summary>
        private void ClearChangeHandler()
        {
            _localizedString.StringChanged -= UpdateString;
        }

        #region UNITY_EVENT

        private void OnEnable()
        {
            RegisterChangeHandler();
        }

        private void OnDisable()
        {
            ClearChangeHandler();
        }

        #endregion
    }
}