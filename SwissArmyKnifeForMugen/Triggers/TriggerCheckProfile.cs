// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerCheckProfile
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using System;
using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen.Triggers
{
    /// <summary>
    /// Profile representing breakpoint triggers that were set.
    /// </summary>
    internal class TriggerCheckProfile : ProfileBase
    {
        // defaults (not set up yet)
        private string _playerString = "(Not set)";
        private string _triggerString = "(Not set)";
        private string _valueString = "(Not set)";
        // keys for config in profile
        private string player_key = "TargetPlayer";
        private string trigger_key = "TargetTrigger";
        private string value_key = "TargetValue";

        public string GetPlayerString() => _playerString;

        public void SetPlayerString(string s) => _playerString = s;

        public string GetTriggerString() => _triggerString;

        public void SetTriggerString(string s) => _triggerString = s;

        public string GetValueString() => _valueString;

        public void SetValueString(string s) => _valueString = s;

        public void ResetConf()
        {
            _playerString = "(Not set)";
            _triggerString = "(Not set)";
            _valueString = "(Not set)";
        }

        private void _LoadParams(string stBuffer)
        {
            if (stBuffer.IndexOf(player_key, 0) == 0)
                _playerString = GetValue(player_key, stBuffer);
            else if (stBuffer.IndexOf(trigger_key, 0) == 0)
            {
                _triggerString = GetValue(trigger_key, stBuffer);
            }
            else
            {
                if (stBuffer.IndexOf(value_key, 0) != 0)
                    return;
                _valueString = GetValue(value_key, stBuffer);
            }
        }

        protected override bool LoadCfgFile(string the_file)
        {
            ResetConf();
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(the_file, Encoding.Default);
                while (streamReader.Peek() >= 0)
                {
                    string stBuffer = streamReader.ReadLine().Trim();
                    if (stBuffer.IndexOf(";", 0) != 0)
                    {
                        int length = stBuffer.IndexOf(';');
                        if (length > 0)
                            stBuffer = stBuffer.Substring(0, length);
                        _LoadParams(stBuffer);
                    }
                }
            }
            catch
            {
                streamReader?.Close();
                return false;
            }
            streamReader?.Close();
            return true;
        }

        public bool SaveConfigData()
        {
            string profilePath = GetProfilePath();
            Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
            string contents = ";" + Environment.NewLine + "; Filename:" + GetProfileFile() + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + "[TriggerChecker]" + Environment.NewLine + "TargetPlayer = " + _playerString + Environment.NewLine + "TargetTrigger = " + _triggerString + Environment.NewLine + "TargetValue = " + _valueString + Environment.NewLine;
            try
            {
                File.WriteAllText(profilePath, contents, encoding);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
