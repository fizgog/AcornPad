using System;
using System.Collections;
using System.Collections.Generic;

namespace AcornPad.Common
{
    public class SettingInfo : IComparable
    {
        private readonly int index;
        private readonly string fullName;
        private readonly string category;
        private readonly string name;

        public int Index => index;
        public string FullName => fullName;
        public string Category => category;
        public string Name => name;
        public Object Value { get; set; }

        public SettingInfo(string propertyName)
        {
            fullName = propertyName;

            string[] words = propertyName.Split('_');

            if (words.Length >= 1) category = words[0];
            if (words.Length >= 2) name = words[1];
            if (words.Length >= 3) index = words[2].ToInteger();

            Value = Properties.Settings.Default[fullName];
        }

        public void UpdateValue(object value)
        {
            Value = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is SettingInfo info)
            {
                return Index.CompareTo(info.Index);
            }
            return 0;
        }
    }

    public class SettingsInfoCollection : CollectionBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SettingInfo Item(int index) => (SettingInfo)List[index];

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public void Add(SettingInfo value)
        {
            List.Add(value);
        }

        /// <summary>
        ///
        /// </summary>
        public void Sort()
        {
            InnerList.Sort();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public SettingInfo[] GetByCategory(string category)
        {
            List<SettingInfo> result = new List<SettingInfo>();

            foreach (SettingInfo itm in List)
            {
                if (itm.Category == category) result.Add(itm);
            }

            return result.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="value"></param>
        public void SetValueByFullName(string fullName, object value)
        {
            for (int i = 0; i < Count; i++)
            {
                SettingInfo info = (SettingInfo)List[i];

                if (info.FullName == fullName)
                {
                    //info.UpdateValue(value);
                    info.Value = value;
                    return;
                }
            }
        }
    }
}