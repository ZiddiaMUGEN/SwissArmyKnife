// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MugenProfile
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen.Configs
{
    /// <summary>
    /// Represents a profile for a MUGEN instance.
    /// </summary>
    internal class MugenProfile : ProfileBase
    {
        private int default_color = 1;
        // number of rounds to play
        private int rounds_count = 1;
        // max round length
        private int max_round_time = 10;
        // Profile for Quick VS mode
        private QuickVSProfile _qvsProfile = new QuickVSProfile();
        private int _temp_game_count = -1;
        // list of characters used on this Profile
        private List<Character_t> _characters = new List<Character_t>();
        private string mode_mugen_key = "[Mugen]";
        private string mode_auto_mode_key = "[AutoMode]";
        private string mode_characters_key = "[Characters]";
        private ParamMode _paramMode = ParamMode.MUGEN;
        // name of profile
        private const string profile_name_key = "ProfileName";
        // path to MUGEN
        private const string mugen_path_key = "MugenPath";
        // MUGEN exe path
        private const string mugen_exe_key = "MugenExe";
        // path to select.def
        private const string mugen_select_cfg_key = "SelectDotDef";
        // custom cmd line options
        private const string mugen_cmdline_option_key = "MugenCommandLineOptions";
        // enable high-speed mode by default
        private const string speed_mode_key = "SpeedUp";
        // enable skip mode by default
        private const string skip_mode_key = "SkipMode";
        // enable experimental breakpoint system (non-hardware BPs)
        private const string exp_bps_key = "ExperimentalBPS";
        // enable debug mode by default
        private const string debug_mode_key = "DebugMode";
        private const string auto_mode_key = "AutoMode";
        // round count
        private const string rounds_count_key = "Rounds";
        private const string error_retry_key = "ErrorRetry";
        private const string default_color_key = "DefaultColor";
        private const string max_round_time_key = "MaxRoundTime";
        private const string strict_round_mode_key = "StrictRoundMode";
        private const string default_game_mode_key = "DefaultGameMode";
        private const string match_mode_key = "MatchMode";
        private const string team_side_key = "TeamSide";
        private int _profileNo;
        private string profile_name;
        private string mugen_path;
        private string mugen_exe;
        private string mugen_select_cfg;
        private string mugen_cmdline_options_base;
        private string mugen_cmdline_options;
        private bool speed_mode;
        private bool skip_mode;
        private bool experimental_bps;
        private bool debug_mode;
        private bool auto_mode_available;
        private int error_retry;
        private bool strict_round_mode;
        private MatchMode match_mode;
        private TeamSideMode team_side;
        private string char_list_raw_data;
        private GameMode default_game_mode;
        private GameMode current_game_mode;
        private bool _countIncremented;

        public QuickVSProfile GetQuickVSProfile() => _qvsProfile;

        public int GetTempGameCount()
        {
            lock (this)
                return _temp_game_count;
        }

        public void DecrementTempGameCount()
        {
            lock (this)
                _temp_game_count = _temp_game_count > 0 ? _temp_game_count - 1 : 0;
        }

        public void IncrementTempGameCount()
        {
            lock (this)
            {
                if (_temp_game_count < 0)
                    _temp_game_count = 1;
                else
                    ++_temp_game_count;
                _countIncremented = true;
            }
        }

        public bool CheckIncremented(bool reset)
        {
            bool flag = false;
            lock (this)
            {
                flag = _countIncremented;
                if (reset)
                    _countIncremented = false;
            }
            return flag;
        }

        public void SetIncremented()
        {
            lock (this)
                _countIncremented = true;
        }

        public void SetTempGameCount(int count)
        {
            lock (this)
                _temp_game_count = count;
        }

        public void SetProfileNo(int num)
        {
            if (_profileNo != 0)
                return;
            _profileNo = num;
        }

        public int GetProfileNo() => _profileNo;

        public string GetProfileName() => profile_name != null ? profile_name : "";

        public string GetMugenPath()
        {
            if (mugen_path == null && mugen_exe != null && Path.IsPathRooted(mugen_exe))
                mugen_path = Path.GetDirectoryName(mugen_exe);
            return mugen_path;
        }

        public string GetMugenExePath()
        {
            if (mugen_exe == null || mugen_exe == "")
                return null;
            if (Path.IsPathRooted(mugen_exe))
                return mugen_exe;
            return GetMugenPath() != null ? Path.Combine(GetMugenPath(), mugen_exe) : null;
        }

        public string GetMugenSelectCfgPath()
        {
            if (mugen_select_cfg == null || mugen_select_cfg == "")
                return null;
            if (Path.IsPathRooted(mugen_select_cfg))
                return mugen_select_cfg;
            return GetMugenPath() != null ? Path.Combine(GetMugenPath(), mugen_select_cfg) : null;
        }

        private void _GetNextP1NameEx(int charNo, out string p1Name, out string p3Name)
        {
            p1Name = null;
            p3Name = null;
            if (_characters.Count < 1 || _characters.Count <= charNo)
                return;
            Character_t character = _characters[charNo];
            if (character == null)
                return;
            string name = character.name;
            int length = name.IndexOf("&&");
            if (length < 0)
            {
                p1Name = character.name;
            }
            else
            {
                p1Name = name.Substring(0, length);
                p3Name = name.Substring(length + 2);
            }
        }

        private void _GetNextP2NameEx(int charNo, out string p2Name, out string p4Name)
        {
            int index = charNo;
            p2Name = null;
            p4Name = null;
            if (_characters.Count <= index)
                return;
            Character_t character = _characters[index];
            if (character == null)
                return;
            string name = character.name;
            int length = name.IndexOf("&&");
            if (length < 0)
            {
                p2Name = character.name;
            }
            else
            {
                p2Name = name.Substring(0, length);
                p4Name = name.Substring(length + 2);
            }
        }

        private void _ExtractName(string name_and_color, out string name, out int colorNo)
        {
            name = null;
            colorNo = 0;
            switch (name_and_color)
            {
                case "":
                    break;
                default:
                    int length = name_and_color.LastIndexOf(',');
                    string s;
                    if (length > 0)
                    {
                        name = name_and_color.Substring(0, length);
                        s = name_and_color.Substring(length + 1);
                        name.Trim();
                        s.Trim();
                    }
                    else
                    {
                        name = name_and_color;
                        s = default_color.ToString();
                    }
                    int result;
                    if (int.TryParse(s, out result))
                    {
                        colorNo = result;
                        break;
                    }
                    colorNo = default_color;
                    break;
            }
        }

        public MatchMode GetMatchMode() => match_mode;

        public TeamSideMode GetTeamSideMode() => team_side;

        private void _CalcCharIndecis(int charNo, out int charNo1, out int charNo2)
        {
            if (match_mode == MatchMode.ALLvsALL)
            {
                int num1 = charNo;
                if (team_side == TeamSideMode.BOTH_SIDE)
                    num1 = charNo / 2;
                int num2 = num1;
                int num3;
                for (num3 = 0; num3 < _characters.Count && num2 > _characters.Count - 1 - num3; ++num3)
                    num2 -= _characters.Count - 1 - num3;
                charNo1 = num3;
                charNo2 = num2 + num3 + 1;
                if (charNo2 <= _characters.Count - 1)
                    return;
                ++charNo1;
                charNo2 = charNo1 + 1;
            }
            else
            {
                charNo1 = 0;
                if (team_side == TeamSideMode.BOTH_SIDE)
                    charNo2 = charNo / 2 + 1;
                else
                    charNo2 = charNo + 1;
            }
        }

        public bool WasLastFight()
        {
            int charNo = _temp_game_count;
            if (charNo < 0)
                charNo = 0;
            int charNo1 = 0;
            int charNo2 = 0;
            _CalcCharIndecis(charNo, out charNo1, out charNo2);
            string p1Name;
            _GetNextP1NameEx(charNo1, out p1Name, out string _);
            string p2Name;
            _GetNextP2NameEx(charNo2, out p2Name, out string _);
            return p1Name == null || p2Name == null;
        }

        private string _PrepareNextMatch(
          string p1name,
          int p1color,
          string p2name,
          int p2color,
          string p3name,
          int p3color,
          string p4name,
          int p4color)
        {
            string str1 = "";
            if (p1name != null && p1name != "")
            {
                string path = p1name;
                if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
                    p1name = path + ".def";
                string str2 = str1 + " -p1 \"" + p1name + "\"";
                if (p1color >= 1 && p1color <= 12)
                    str2 = str2 + " -p1.color " + p1color;
                str1 = str2 + " -p1.ai 1";
            }
            if (p3name != null && p3name != "")
            {
                string path = p3name;
                if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
                    p3name = path + ".def";
                string str2 = str1 + " -p3 \"" + p3name + "\"";
                if (p3color >= 1 && p3color <= 12)
                    str2 = str2 + " -p3.color " + p3color;
                str1 = str2 + " -p3.ai 1";
            }
            if (p2name != null && p2name != "")
            {
                string path = p2name;
                if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
                    p2name = path + ".def";
                string str2 = str1 + " -p2 \"" + p2name + "\"";
                if (p2color >= 1 && p2color <= 12)
                    str2 = str2 + " -p2.color " + p2color;
                str1 = str2 + " -p2.ai 1";
            }
            if (p4name != null && p4name != "")
            {
                string path = p4name;
                if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
                    p4name = path + ".def";
                string str2 = str1 + " -p4 \"" + p4name + "\"";
                if (p4color >= 1 && p4color <= 12)
                    str2 = str2 + " -p4.color " + p4color;
                str1 = str2 + " -p4.ai 1";
            }
            return str1;
        }

        public bool PrepareNextMatch(int charNo)
        {
            if (charNo < 0)
                charNo = 0;
            if (_temp_game_count < 0)
                _temp_game_count = 0;
            int charNo1 = 0;
            int charNo2 = 0;
            _CalcCharIndecis(charNo, out charNo1, out charNo2);
            string p1Name = null;
            string p2Name = null;
            string p3Name = null;
            string p4Name = null;
            _GetNextP1NameEx(charNo1, out p1Name, out p3Name);
            _GetNextP2NameEx(charNo2, out p2Name, out p4Name);
            if (p1Name == null || p2Name == null)
                return false;
            string name1 = null;
            int colorNo1 = 0;
            _ExtractName(p1Name, out name1, out colorNo1);
            string name2 = null;
            int colorNo2 = 0;
            _ExtractName(p2Name, out name2, out colorNo2);
            string name3 = null;
            int colorNo3 = 0;
            _ExtractName(p3Name, out name3, out colorNo3);
            string name4 = null;
            int colorNo4 = 0;
            _ExtractName(p4Name, out name4, out colorNo4);
            mugen_cmdline_options = mugen_cmdline_options_base;
            switch (GetTeamSideMode())
            {
                case TeamSideMode.P1_SIDE:
                    mugen_cmdline_options += _PrepareNextMatch(name1, colorNo1, name2, colorNo2, name3, colorNo3, name4, colorNo4);
                    break;
                case TeamSideMode.P2_SIDE:
                    mugen_cmdline_options += _PrepareNextMatch(name2, colorNo2, name1, colorNo1, name4, colorNo4, name3, colorNo3);
                    break;
                case TeamSideMode.BOTH_SIDE:
                    mugen_cmdline_options = charNo != charNo / 2 * 2 ? mugen_cmdline_options + _PrepareNextMatch(name2, colorNo2, name1, colorNo1, name4, colorNo4, name3, colorNo3) : mugen_cmdline_options + _PrepareNextMatch(name1, colorNo1, name2, colorNo2, name3, colorNo3, name4, colorNo4);
                    break;
            }
            mugen_cmdline_options = !strict_round_mode ? mugen_cmdline_options + " -rounds " + rounds_count : mugen_cmdline_options + " -rounds " + (rounds_count + 1);
            return true;
        }

        private int _GetCharacterCount(int nCount)
        {
            switch (GetMatchMode())
            {
                case MatchMode.P1vsALL:
                    return nCount - 1;
                case MatchMode.ALLvsALL:
                    return nCount * (nCount - 1) / 2;
                default:
                    return nCount - 1;
            }
        }

        public int GetCharacterCount()
        {
            if (_characters == null || _characters.Count < 1)
                return 0;
            switch (GetTeamSideMode())
            {
                case TeamSideMode.P1_SIDE:
                    return _GetCharacterCount(_characters.Count);
                case TeamSideMode.P2_SIDE:
                    return _GetCharacterCount(_characters.Count);
                case TeamSideMode.BOTH_SIDE:
                    return _GetCharacterCount(_characters.Count) * 2;
                default:
                    return _GetCharacterCount(_characters.Count);
            }
        }

        public int GetMaxRoundTimeRawData() => max_round_time;

        public int GetMaxRoundTime() => max_round_time * 60;

        public int GetMaxRoundState1Time() => 60;

        public int GetMaxRoundState4Time() => 60;

        public string GetMugenCommandLineOptionsBase() => mugen_cmdline_options_base;

        public string GetMugenCommandLineOptions()
        {
            if (current_game_mode == GameMode.QUICK_VS)
            {
                mugen_cmdline_options = mugen_cmdline_options_base;
                mugen_cmdline_options = mugen_cmdline_options + " -p1 \"" + _qvsProfile.GetP1Def() + "\"";
                mugen_cmdline_options = mugen_cmdline_options + " -p1.color " + _qvsProfile.GetP1Color();
                mugen_cmdline_options = mugen_cmdline_options + " -p1.ai " + (_qvsProfile.GetP1AI() ? 1 : 0);
                if (_qvsProfile.GetP1LifeFlag())
                    mugen_cmdline_options = mugen_cmdline_options + " -p1.life " + _qvsProfile.GetP1LifeValue();
                if (_qvsProfile.GetP1PowerFlag())
                    mugen_cmdline_options = mugen_cmdline_options + " -p1.power " + _qvsProfile.GetP1PowerValue();
                if (_qvsProfile.IsP3Enabled())
                {
                    mugen_cmdline_options = mugen_cmdline_options + " -p3 \"" + _qvsProfile.GetP3Def() + "\"";
                    mugen_cmdline_options = mugen_cmdline_options + " -p3.color " + _qvsProfile.GetP3Color();
                    mugen_cmdline_options = mugen_cmdline_options + " -p3.ai " + (_qvsProfile.GetP3AI() ? 1 : 0);
                    if (_qvsProfile.GetP3LifeFlag())
                        mugen_cmdline_options = mugen_cmdline_options + " -p3.life " + _qvsProfile.GetP3LifeValue();
                    if (_qvsProfile.GetP3PowerFlag())
                        mugen_cmdline_options = mugen_cmdline_options + " -p3.power " + _qvsProfile.GetP3PowerValue();
                }
                mugen_cmdline_options = mugen_cmdline_options + " -p2 \"" + _qvsProfile.GetP2Def() + "\"";
                mugen_cmdline_options = mugen_cmdline_options + " -p2.color " + _qvsProfile.GetP2Color();
                mugen_cmdline_options = mugen_cmdline_options + " -p2.ai " + (_qvsProfile.GetP2AI() ? 1 : 0);
                if (_qvsProfile.GetP2LifeFlag())
                    mugen_cmdline_options = mugen_cmdline_options + " -p2.life " + _qvsProfile.GetP2LifeValue();
                if (_qvsProfile.GetP2PowerFlag())
                    mugen_cmdline_options = mugen_cmdline_options + " -p2.power " + _qvsProfile.GetP2PowerValue();
                if (_qvsProfile.IsP4Enabled())
                {
                    mugen_cmdline_options = mugen_cmdline_options + " -p4 \"" + _qvsProfile.GetP4Def() + "\"";
                    mugen_cmdline_options = mugen_cmdline_options + " -p4.color " + _qvsProfile.GetP4Color();
                    mugen_cmdline_options = mugen_cmdline_options + " -p4.ai " + (_qvsProfile.GetP4AI() ? 1 : 0);
                    if (_qvsProfile.GetP4LifeFlag())
                        mugen_cmdline_options = mugen_cmdline_options + " -p4.life " + _qvsProfile.GetP4LifeValue();
                    if (_qvsProfile.GetP4PowerFlag())
                        mugen_cmdline_options = mugen_cmdline_options + " -p4.power " + _qvsProfile.GetP4PowerValue();
                }
                mugen_cmdline_options = mugen_cmdline_options + " -rounds " + _qvsProfile.GetRounds();
                if (_qvsProfile.GetNoMusicFlag())
                    mugen_cmdline_options += " -nomusic";
                if (_qvsProfile.GetNoSoundFlag())
                    mugen_cmdline_options += " -nosound";
                if (_qvsProfile.GetStage() != "")
                    mugen_cmdline_options = mugen_cmdline_options + " -s \"" + _qvsProfile.GetStage() + "\"";
            }
            return mugen_cmdline_options;
        }

        public bool IsSpeedMode() => speed_mode;

        public bool IsSkipMode() => skip_mode;

        public bool IsExperimentalBreakpoints() => experimental_bps;

        public bool IsDebugMode() => debug_mode;

        public bool IsAutoModeAvailable() => auto_mode_available;

        public void SetGameMode(GameMode mode)
        {
            current_game_mode = mode;
            mugen_cmdline_options = mugen_cmdline_options_base;
        }

        public bool IsAutoMode() => current_game_mode == GameMode.AUTO_MODE;

        public bool IsQuickMode() => current_game_mode == GameMode.QUICK_VS;

        public GameMode GetCurrentGameMode() => current_game_mode;

        public GameMode GetDefaultGameMode() => default_game_mode;

        public int GetErrorRetryCount() => error_retry;

        public int GetRoundCount() => rounds_count;

        public int GetDefaultColor() => default_color;

        public string GetCharListRawData() => char_list_raw_data;

        public bool IsStrictRoundMode() => strict_round_mode;

        protected override void InitSubProfiles() => _qvsProfile.InitProfile(profile_folder, cfg_file_name + ".qvs");

        private ParamMode _CheckParamMode(string stBuffer)
        {
            if (stBuffer.Length < 2 || stBuffer[0] != '[' || stBuffer[stBuffer.Length - 1] != ']')
                return ParamMode.NONE;
            if (stBuffer.IndexOf(mode_mugen_key, 0) == 0)
                return ParamMode.MUGEN;
            if (stBuffer.IndexOf(mode_auto_mode_key, 0) == 0)
                return ParamMode.AUTO_MODE;
            return stBuffer.IndexOf(mode_characters_key, 0) == 0 ? ParamMode.CHARACTERS : ParamMode.UNKNOWN;
        }

        private bool CheckParamMode(string stBuffer)
        {
            ParamMode paramMode = _CheckParamMode(stBuffer);
            switch (paramMode)
            {
                case ParamMode.MUGEN:
                case ParamMode.AUTO_MODE:
                case ParamMode.CHARACTERS:
                    _paramMode = paramMode;
                    return true;
                default:
                    return false;
            }
        }

        private void _LoadMugenParams(string stBuffer)
        {
            if (stBuffer.IndexOf("MugenPath", 0) == 0)
                mugen_path = GetValue("MugenPath", stBuffer);
            else if (stBuffer.IndexOf("MugenExe", 0) == 0)
                mugen_exe = GetValue("MugenExe", stBuffer);
            else if (stBuffer.IndexOf("SelectDotDef", 0) == 0)
                mugen_select_cfg = GetValue("SelectDotDef", stBuffer);
            else if (stBuffer.IndexOf("ProfileName", 0) == 0)
                profile_name = GetValue("ProfileName", stBuffer);
            else if (stBuffer.IndexOf("MugenCommandLineOptions", 0) == 0)
            {
                mugen_cmdline_options_base = GetValue("MugenCommandLineOptions", stBuffer);
                mugen_cmdline_options = mugen_cmdline_options_base;
            }
            else if (stBuffer.IndexOf("SpeedUp", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("SpeedUp", stBuffer), out result) || result == 0)
                    return;
                speed_mode = true;
            }
            else if (stBuffer.IndexOf("SkipMode", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("SkipMode", stBuffer), out result) || result == 0)
                    return;
                skip_mode = true;
            }
            else if (stBuffer.IndexOf("ExperimentalBPS", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("ExperimentalBPS", stBuffer), out result) || result == 0)
                    return;
                experimental_bps = true;
            }
            else if (stBuffer.IndexOf("DebugMode", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("DebugMode", stBuffer), out result) || result == 0)
                    return;
                debug_mode = true;
            }
            else
            {
                int result;
                if (stBuffer.IndexOf("DefaultGameMode", 0) != 0 || !int.TryParse(GetValue("DefaultGameMode", stBuffer), out result))
                    return;
                switch (result)
                {
                    case 0:
                        default_game_mode = GameMode.NORMAL;
                        break;
                    case 1:
                        default_game_mode = GameMode.QUICK_VS;
                        break;
                    case 2:
                        default_game_mode = GameMode.AUTO_MODE;
                        break;
                    default:
                        default_game_mode = GameMode.NORMAL;
                        break;
                }
            }
        }

        private void _LoadAutoModeParams(string stBuffer)
        {
            if (stBuffer.IndexOf("AutoMode", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("AutoMode", stBuffer), out result) || result == 0)
                    return;
                auto_mode_available = true;
            }
            else if (stBuffer.IndexOf("ErrorRetry", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("ErrorRetry", stBuffer), out result))
                    return;
                error_retry = result;
            }
            else if (stBuffer.IndexOf("DefaultColor", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("DefaultColor", stBuffer), out result))
                    return;
                default_color = result;
            }
            else if (stBuffer.IndexOf("Rounds", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("Rounds", stBuffer), out result))
                    return;
                rounds_count = result;
            }
            else if (stBuffer.IndexOf("MaxRoundTime", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("MaxRoundTime", stBuffer), out result))
                    return;
                max_round_time = result;
            }
            else if (stBuffer.IndexOf("StrictRoundMode", 0) == 0)
                strict_round_mode = false;
            else if (stBuffer.IndexOf("MatchMode", 0) == 0)
            {
                int result;
                if (!int.TryParse(GetValue("MatchMode", stBuffer), out result))
                    return;
                if (result != 0)
                {
                    if (result == 1)
                        match_mode = MatchMode.ALLvsALL;
                    else
                        match_mode = MatchMode.P1vsALL;
                }
                else
                    match_mode = MatchMode.P1vsALL;
            }
            else
            {
                int result;
                if (stBuffer.IndexOf("TeamSide", 0) != 0 || !int.TryParse(GetValue("TeamSide", stBuffer), out result))
                    return;
                switch (result)
                {
                    case 0:
                        team_side = TeamSideMode.BOTH_SIDE;
                        break;
                    case 1:
                        team_side = TeamSideMode.P1_SIDE;
                        break;
                    case 2:
                        team_side = TeamSideMode.P2_SIDE;
                        break;
                    default:
                        team_side = TeamSideMode.BOTH_SIDE;
                        break;
                }
            }
        }

        private void _LoadCharacters(string stBuffer)
        {
            if (stBuffer == "")
                return;
            _characters.Add(new Character_t()
            {
                name = stBuffer
            });
        }

        public void ResetConf()
        {
            _paramMode = ParamMode.MUGEN;
            _characters.Clear();
            profile_name = null;
            mugen_path = null;
            mugen_exe = null;
            mugen_select_cfg = null;
            mugen_cmdline_options_base = null;
            mugen_cmdline_options = null;
            speed_mode = false;
            skip_mode = false;
            experimental_bps = false;
            debug_mode = false;
            auto_mode_available = false;
            error_retry = 0;
            default_color = 1;
            rounds_count = 1;
            strict_round_mode = false;
            max_round_time = 10;
            char_list_raw_data = null;
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
                    string str = streamReader.ReadLine();
                    if (_paramMode == ParamMode.CHARACTERS)
                    {
                        if (char_list_raw_data == null)
                        {
                            char_list_raw_data = str + Environment.NewLine;
                        }
                        else
                        {
                            MugenProfile mugenProfile = this;
                            mugenProfile.char_list_raw_data = mugenProfile.char_list_raw_data + str + Environment.NewLine;
                        }
                    }
                    string stBuffer = str.Trim();
                    if (stBuffer.IndexOf(";", 0) != 0)
                    {
                        int length = stBuffer.IndexOf(';');
                        if (length > 0)
                            stBuffer = stBuffer.Substring(0, length);
                        if (!CheckParamMode(stBuffer))
                        {
                            switch (_paramMode)
                            {
                                case ParamMode.MUGEN:
                                    _LoadMugenParams(stBuffer);
                                    continue;
                                case ParamMode.AUTO_MODE:
                                    _LoadAutoModeParams(stBuffer);
                                    continue;
                                case ParamMode.CHARACTERS:
                                    _LoadCharacters(stBuffer);
                                    continue;
                                default:
                                    continue;
                            }
                        }
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

        public string GetCharListRawData(string the_file)
        {
            string str1 = "";
            ParamMode paramMode = ParamMode.NONE;
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(the_file, Encoding.Default);
                while (streamReader.Peek() >= 0)
                {
                    string stBuffer = streamReader.ReadLine().Trim();
                    if (stBuffer.IndexOf(";", 0) != 0)
                    {
                        int length1 = stBuffer.IndexOf(';');
                        if (length1 > 0)
                            stBuffer = stBuffer.Substring(0, length1);
                        int length2 = stBuffer.IndexOf(',');
                        if (length2 > 0)
                            stBuffer = stBuffer.Substring(0, length2);
                        switch (_CheckParamMode(stBuffer))
                        {
                            case ParamMode.NONE:
                                if (paramMode == ParamMode.CHARACTERS)
                                {
                                    string str2 = stBuffer;
                                    string lower = stBuffer.ToLower();
                                    if (!lower.Contains("randomselect") && !lower.Contains("blank") && !lower.Contains("empty") && !lower.Contains("none"))
                                    {
                                        str1 = str1 + str2 + Environment.NewLine;
                                        continue;
                                    }
                                    continue;
                                }
                                continue;
                            case ParamMode.CHARACTERS:
                                paramMode = ParamMode.CHARACTERS;
                                continue;
                            default:
                                paramMode = ParamMode.NONE;
                                continue;
                        }
                    }
                }
            }
            catch
            {
                streamReader?.Close();
                return str1;
            }
            streamReader?.Close();
            return str1;
        }

        public enum GameMode
        {
            NORMAL,
            QUICK_VS,
            AUTO_MODE,
        }

        public enum MatchMode
        {
            P1vsALL,
            ALLvsALL,
        }

        public enum TeamSideMode
        {
            P1_SIDE,
            P2_SIDE,
            BOTH_SIDE,
        }

        private class Character_t
        {
            public string name;
        }

        private enum ParamMode
        {
            NONE,
            UNKNOWN,
            MUGEN,
            AUTO_MODE,
            CHARACTERS,
        }
    }
}
