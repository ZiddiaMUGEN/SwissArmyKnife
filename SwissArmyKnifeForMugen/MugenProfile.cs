// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MugenProfile
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen
{
  internal class MugenProfile : ProfileBase
  {
    private int default_color = 1;
    private int rounds_count = 1;
    private int max_round_time = 10;
    private QuickVSProfile _qvsProfile = new QuickVSProfile();
    private int _temp_game_count = -1;
    private List<MugenProfile.Character_t> _characters = new List<MugenProfile.Character_t>();
    private string mode_mugen_key = "[Mugen]";
    private string mode_auto_mode_key = "[AutoMode]";
    private string mode_characters_key = "[Characters]";
    private MugenProfile.ParamMode _paramMode = MugenProfile.ParamMode.MUGEN;
    private const string profile_name_key = "ProfileName";
    private const string mugen_path_key = "MugenPath";
    private const string mugen_exe_key = "MugenExe";
    private const string mugen_select_cfg_key = "SelectDotDef";
    private const string mugen_cmdline_option_key = "MugenCommandLineOptions";
    private const string speed_mode_key = "SpeedUp";
    private const string skip_mode_key = "SkipMode";
    private const string exp_bps_key = "ExperimentalBPS";
    private const string debug_mode_key = "DebugMode";
    private const string auto_mode_key = "AutoMode";
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
    private MugenProfile.MatchMode match_mode;
    private MugenProfile.TeamSideMode team_side;
    private string char_list_raw_data;
    private MugenProfile.GameMode default_game_mode;
    private MugenProfile.GameMode current_game_mode;
    private bool _countIncremented;

    public QuickVSProfile GetQuickVSProfile() => this._qvsProfile;

    public int GetTempGameCount()
    {
      lock (this)
        return this._temp_game_count;
    }

    public void DecrementTempGameCount()
    {
      lock (this)
        this._temp_game_count = this._temp_game_count > 0 ? this._temp_game_count - 1 : 0;
    }

    public void IncrementTempGameCount()
    {
      lock (this)
      {
        if (this._temp_game_count < 0)
          this._temp_game_count = 1;
        else
          ++this._temp_game_count;
        this._countIncremented = true;
      }
    }

    public bool CheckIncremented(bool reset)
    {
      bool flag = false;
      lock (this)
      {
        flag = this._countIncremented;
        if (reset)
          this._countIncremented = false;
      }
      return flag;
    }

    public void SetIncremented()
    {
      lock (this)
        this._countIncremented = true;
    }

    public void SetTempGameCount(int count)
    {
      lock (this)
        this._temp_game_count = count;
    }

    public void SetProfileNo(int num)
    {
      if (this._profileNo != 0)
        return;
      this._profileNo = num;
    }

    public int GetProfileNo() => this._profileNo;

    public string GetProfileName() => this.profile_name != null ? this.profile_name : "";

    public string GetMugenPath()
    {
      if (this.mugen_path == null && this.mugen_exe != null && Path.IsPathRooted(this.mugen_exe))
        this.mugen_path = Path.GetDirectoryName(this.mugen_exe);
      return this.mugen_path;
    }

    public string GetMugenExePath()
    {
      if (this.mugen_exe == null || this.mugen_exe == "")
        return (string) null;
      if (Path.IsPathRooted(this.mugen_exe))
        return this.mugen_exe;
      return this.GetMugenPath() != null ? Path.Combine(this.GetMugenPath(), this.mugen_exe) : (string) null;
    }

    public string GetMugenSelectCfgPath()
    {
      if (this.mugen_select_cfg == null || this.mugen_select_cfg == "")
        return (string) null;
      if (Path.IsPathRooted(this.mugen_select_cfg))
        return this.mugen_select_cfg;
      return this.GetMugenPath() != null ? Path.Combine(this.GetMugenPath(), this.mugen_select_cfg) : (string) null;
    }

    private void _GetNextP1NameEx(int charNo, out string p1Name, out string p3Name)
    {
      p1Name = (string) null;
      p3Name = (string) null;
      if (this._characters.Count < 1 || this._characters.Count <= charNo)
        return;
      MugenProfile.Character_t character = this._characters[charNo];
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
      p2Name = (string) null;
      p4Name = (string) null;
      if (this._characters.Count <= index)
        return;
      MugenProfile.Character_t character = this._characters[index];
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
      name = (string) null;
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
            s = this.default_color.ToString();
          }
          int result;
          if (int.TryParse(s, out result))
          {
            colorNo = result;
            break;
          }
          colorNo = this.default_color;
          break;
      }
    }

    public MugenProfile.MatchMode GetMatchMode() => this.match_mode;

    public MugenProfile.TeamSideMode GetTeamSideMode() => this.team_side;

    private void _CalcCharIndecis(int charNo, out int charNo1, out int charNo2)
    {
      if (this.match_mode == MugenProfile.MatchMode.ALLvsALL)
      {
        int num1 = charNo;
        if (this.team_side == MugenProfile.TeamSideMode.BOTH_SIDE)
          num1 = charNo / 2;
        int num2 = num1;
        int num3;
        for (num3 = 0; num3 < this._characters.Count && num2 > this._characters.Count - 1 - num3; ++num3)
          num2 -= this._characters.Count - 1 - num3;
        charNo1 = num3;
        charNo2 = num2 + num3 + 1;
        if (charNo2 <= this._characters.Count - 1)
          return;
        ++charNo1;
        charNo2 = charNo1 + 1;
      }
      else
      {
        charNo1 = 0;
        if (this.team_side == MugenProfile.TeamSideMode.BOTH_SIDE)
          charNo2 = charNo / 2 + 1;
        else
          charNo2 = charNo + 1;
      }
    }

    public bool WasLastFight()
    {
      int charNo = this._temp_game_count;
      if (charNo < 0)
        charNo = 0;
      int charNo1 = 0;
      int charNo2 = 0;
      this._CalcCharIndecis(charNo, out charNo1, out charNo2);
      string p1Name;
      this._GetNextP1NameEx(charNo1, out p1Name, out string _);
      string p2Name;
      this._GetNextP2NameEx(charNo2, out p2Name, out string _);
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
          str2 = str2 + " -p1.color " + (object) p1color;
        str1 = str2 + " -p1.ai 1";
      }
      if (p3name != null && p3name != "")
      {
        string path = p3name;
        if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
          p3name = path + ".def";
        string str2 = str1 + " -p3 \"" + p3name + "\"";
        if (p3color >= 1 && p3color <= 12)
          str2 = str2 + " -p3.color " + (object) p3color;
        str1 = str2 + " -p3.ai 1";
      }
      if (p2name != null && p2name != "")
      {
        string path = p2name;
        if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
          p2name = path + ".def";
        string str2 = str1 + " -p2 \"" + p2name + "\"";
        if (p2color >= 1 && p2color <= 12)
          str2 = str2 + " -p2.color " + (object) p2color;
        str1 = str2 + " -p2.ai 1";
      }
      if (p4name != null && p4name != "")
      {
        string path = p4name;
        if (Path.GetExtension(path) != ".def" && Path.GetFileName(path) != path)
          p4name = path + ".def";
        string str2 = str1 + " -p4 \"" + p4name + "\"";
        if (p4color >= 1 && p4color <= 12)
          str2 = str2 + " -p4.color " + (object) p4color;
        str1 = str2 + " -p4.ai 1";
      }
      return str1;
    }

    public bool PrepareNextMatch(int charNo)
    {
      if (charNo < 0)
        charNo = 0;
      if (this._temp_game_count < 0)
        this._temp_game_count = 0;
      int charNo1 = 0;
      int charNo2 = 0;
      this._CalcCharIndecis(charNo, out charNo1, out charNo2);
      string p1Name = (string) null;
      string p2Name = (string) null;
      string p3Name = (string) null;
      string p4Name = (string) null;
      this._GetNextP1NameEx(charNo1, out p1Name, out p3Name);
      this._GetNextP2NameEx(charNo2, out p2Name, out p4Name);
      if (p1Name == null || p2Name == null)
        return false;
      string name1 = (string) null;
      int colorNo1 = 0;
      this._ExtractName(p1Name, out name1, out colorNo1);
      string name2 = (string) null;
      int colorNo2 = 0;
      this._ExtractName(p2Name, out name2, out colorNo2);
      string name3 = (string) null;
      int colorNo3 = 0;
      this._ExtractName(p3Name, out name3, out colorNo3);
      string name4 = (string) null;
      int colorNo4 = 0;
      this._ExtractName(p4Name, out name4, out colorNo4);
      this.mugen_cmdline_options = this.mugen_cmdline_options_base;
      switch (this.GetTeamSideMode())
      {
        case MugenProfile.TeamSideMode.P1_SIDE:
          this.mugen_cmdline_options += this._PrepareNextMatch(name1, colorNo1, name2, colorNo2, name3, colorNo3, name4, colorNo4);
          break;
        case MugenProfile.TeamSideMode.P2_SIDE:
          this.mugen_cmdline_options += this._PrepareNextMatch(name2, colorNo2, name1, colorNo1, name4, colorNo4, name3, colorNo3);
          break;
        case MugenProfile.TeamSideMode.BOTH_SIDE:
          this.mugen_cmdline_options = charNo != charNo / 2 * 2 ? this.mugen_cmdline_options + this._PrepareNextMatch(name2, colorNo2, name1, colorNo1, name4, colorNo4, name3, colorNo3) : this.mugen_cmdline_options + this._PrepareNextMatch(name1, colorNo1, name2, colorNo2, name3, colorNo3, name4, colorNo4);
          break;
      }
      this.mugen_cmdline_options = !this.strict_round_mode ? this.mugen_cmdline_options + " -rounds " + (object) this.rounds_count : this.mugen_cmdline_options + " -rounds " + (object) (this.rounds_count + 1);
      return true;
    }

    private int _GetCharacterCount(int nCount)
    {
      switch (this.GetMatchMode())
      {
        case MugenProfile.MatchMode.P1vsALL:
          return nCount - 1;
        case MugenProfile.MatchMode.ALLvsALL:
          return nCount * (nCount - 1) / 2;
        default:
          return nCount - 1;
      }
    }

    public int GetCharacterCount()
    {
      if (this._characters == null || this._characters.Count < 1)
        return 0;
      switch (this.GetTeamSideMode())
      {
        case MugenProfile.TeamSideMode.P1_SIDE:
          return this._GetCharacterCount(this._characters.Count);
        case MugenProfile.TeamSideMode.P2_SIDE:
          return this._GetCharacterCount(this._characters.Count);
        case MugenProfile.TeamSideMode.BOTH_SIDE:
          return this._GetCharacterCount(this._characters.Count) * 2;
        default:
          return this._GetCharacterCount(this._characters.Count);
      }
    }

    public int GetMaxRoundTimeRawData() => this.max_round_time;

    public int GetMaxRoundTime() => this.max_round_time * 60;

    public int GetMaxRoundState1Time() => 60;

    public int GetMaxRoundState4Time() => 60;

    public string GetMugenCommandLineOptionsBase() => this.mugen_cmdline_options_base;

    public string GetMugenCommandLineOptions()
    {
      if (this.current_game_mode == MugenProfile.GameMode.QUICK_VS)
      {
        this.mugen_cmdline_options = this.mugen_cmdline_options_base;
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p1 \"" + this._qvsProfile.GetP1Def() + "\"";
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p1.color " + (object) this._qvsProfile.GetP1Color();
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p1.ai " + (object) (this._qvsProfile.GetP1AI() ? 1 : 0);
        if (this._qvsProfile.GetP1LifeFlag())
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p1.life " + (object) this._qvsProfile.GetP1LifeValue();
        if (this._qvsProfile.GetP1PowerFlag())
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p1.power " + (object) this._qvsProfile.GetP1PowerValue();
        if (this._qvsProfile.IsP3Enabled())
        {
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p3 \"" + this._qvsProfile.GetP3Def() + "\"";
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p3.color " + (object) this._qvsProfile.GetP3Color();
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p3.ai " + (object) (this._qvsProfile.GetP3AI() ? 1 : 0);
          if (this._qvsProfile.GetP3LifeFlag())
            this.mugen_cmdline_options = this.mugen_cmdline_options + " -p3.life " + (object) this._qvsProfile.GetP3LifeValue();
          if (this._qvsProfile.GetP3PowerFlag())
            this.mugen_cmdline_options = this.mugen_cmdline_options + " -p3.power " + (object) this._qvsProfile.GetP3PowerValue();
        }
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p2 \"" + this._qvsProfile.GetP2Def() + "\"";
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p2.color " + (object) this._qvsProfile.GetP2Color();
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -p2.ai " + (object) (this._qvsProfile.GetP2AI() ? 1 : 0);
        if (this._qvsProfile.GetP2LifeFlag())
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p2.life " + (object) this._qvsProfile.GetP2LifeValue();
        if (this._qvsProfile.GetP2PowerFlag())
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p2.power " + (object) this._qvsProfile.GetP2PowerValue();
        if (this._qvsProfile.IsP4Enabled())
        {
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p4 \"" + this._qvsProfile.GetP4Def() + "\"";
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p4.color " + (object) this._qvsProfile.GetP4Color();
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -p4.ai " + (object) (this._qvsProfile.GetP4AI() ? 1 : 0);
          if (this._qvsProfile.GetP4LifeFlag())
            this.mugen_cmdline_options = this.mugen_cmdline_options + " -p4.life " + (object) this._qvsProfile.GetP4LifeValue();
          if (this._qvsProfile.GetP4PowerFlag())
            this.mugen_cmdline_options = this.mugen_cmdline_options + " -p4.power " + (object) this._qvsProfile.GetP4PowerValue();
        }
        this.mugen_cmdline_options = this.mugen_cmdline_options + " -rounds " + (object) this._qvsProfile.GetRounds();
        if (this._qvsProfile.GetNoMusicFlag())
          this.mugen_cmdline_options += " -nomusic";
        if (this._qvsProfile.GetNoSoundFlag())
          this.mugen_cmdline_options += " -nosound";
        if (this._qvsProfile.GetStage() != "")
          this.mugen_cmdline_options = this.mugen_cmdline_options + " -s \"" + this._qvsProfile.GetStage() + "\"";
      }
      return this.mugen_cmdline_options;
    }

    public bool IsSpeedMode() => this.speed_mode;

    public bool IsSkipMode() => this.skip_mode;

    public bool IsExperimentalBreakpoints() => this.experimental_bps;

    public bool IsDebugMode() => this.debug_mode;

    public bool IsAutoModeAvailable() => this.auto_mode_available;

    public void SetGameMode(MugenProfile.GameMode mode)
    {
      this.current_game_mode = mode;
      this.mugen_cmdline_options = this.mugen_cmdline_options_base;
    }

    public bool IsAutoMode() => this.current_game_mode == MugenProfile.GameMode.AUTO_MODE;

    public bool IsQuickMode() => this.current_game_mode == MugenProfile.GameMode.QUICK_VS;

    public MugenProfile.GameMode GetCurrentGameMode() => this.current_game_mode;

    public MugenProfile.GameMode GetDefaultGameMode() => this.default_game_mode;

    public int GetErrorRetryCount() => this.error_retry;

    public int GetRoundCount() => this.rounds_count;

    public int GetDefaultColor() => this.default_color;

    public string GetCharListRawData() => this.char_list_raw_data;

    public bool IsStrictRoundMode() => this.strict_round_mode;

    protected override void InitSubProfiles() => this._qvsProfile.InitProfile(this.profile_folder, this.cfg_file_name + ".qvs");

    private MugenProfile.ParamMode _CheckParamMode(string stBuffer)
    {
      if (stBuffer.Length < 2 || stBuffer[0] != '[' || stBuffer[stBuffer.Length - 1] != ']')
        return MugenProfile.ParamMode.NONE;
      if (stBuffer.IndexOf(this.mode_mugen_key, 0) == 0)
        return MugenProfile.ParamMode.MUGEN;
      if (stBuffer.IndexOf(this.mode_auto_mode_key, 0) == 0)
        return MugenProfile.ParamMode.AUTO_MODE;
      return stBuffer.IndexOf(this.mode_characters_key, 0) == 0 ? MugenProfile.ParamMode.CHARACTERS : MugenProfile.ParamMode.UNKNOWN;
    }

    private bool CheckParamMode(string stBuffer)
    {
      MugenProfile.ParamMode paramMode = this._CheckParamMode(stBuffer);
      switch (paramMode)
      {
        case MugenProfile.ParamMode.MUGEN:
        case MugenProfile.ParamMode.AUTO_MODE:
        case MugenProfile.ParamMode.CHARACTERS:
          this._paramMode = paramMode;
          return true;
        default:
          return false;
      }
    }

    private void _LoadMugenParams(string stBuffer)
    {
      if (stBuffer.IndexOf("MugenPath", 0) == 0)
        this.mugen_path = this.GetValue("MugenPath", stBuffer);
      else if (stBuffer.IndexOf("MugenExe", 0) == 0)
        this.mugen_exe = this.GetValue("MugenExe", stBuffer);
      else if (stBuffer.IndexOf("SelectDotDef", 0) == 0)
        this.mugen_select_cfg = this.GetValue("SelectDotDef", stBuffer);
      else if (stBuffer.IndexOf("ProfileName", 0) == 0)
        this.profile_name = this.GetValue("ProfileName", stBuffer);
      else if (stBuffer.IndexOf("MugenCommandLineOptions", 0) == 0)
      {
        this.mugen_cmdline_options_base = this.GetValue("MugenCommandLineOptions", stBuffer);
        this.mugen_cmdline_options = this.mugen_cmdline_options_base;
      }
      else if (stBuffer.IndexOf("SpeedUp", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("SpeedUp", stBuffer), out result) || result == 0)
          return;
        this.speed_mode = true;
      }
      else if (stBuffer.IndexOf("SkipMode", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("SkipMode", stBuffer), out result) || result == 0)
          return;
        this.skip_mode = true;
      }
      else if (stBuffer.IndexOf("ExperimentalBPS", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("ExperimentalBPS", stBuffer), out result) || result == 0)
          return;
        this.experimental_bps = true;
      }
      else if (stBuffer.IndexOf("DebugMode", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("DebugMode", stBuffer), out result) || result == 0)
          return;
        this.debug_mode = true;
      }
      else
      {
        int result;
        if (stBuffer.IndexOf("DefaultGameMode", 0) != 0 || !int.TryParse(this.GetValue("DefaultGameMode", stBuffer), out result))
          return;
        switch (result)
        {
          case 0:
            this.default_game_mode = MugenProfile.GameMode.NORMAL;
            break;
          case 1:
            this.default_game_mode = MugenProfile.GameMode.QUICK_VS;
            break;
          case 2:
            this.default_game_mode = MugenProfile.GameMode.AUTO_MODE;
            break;
          default:
            this.default_game_mode = MugenProfile.GameMode.NORMAL;
            break;
        }
      }
    }

    private void _LoadAutoModeParams(string stBuffer)
    {
      if (stBuffer.IndexOf("AutoMode", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("AutoMode", stBuffer), out result) || result == 0)
          return;
        this.auto_mode_available = true;
      }
      else if (stBuffer.IndexOf("ErrorRetry", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("ErrorRetry", stBuffer), out result))
          return;
        this.error_retry = result;
      }
      else if (stBuffer.IndexOf("DefaultColor", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("DefaultColor", stBuffer), out result))
          return;
        this.default_color = result;
      }
      else if (stBuffer.IndexOf("Rounds", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("Rounds", stBuffer), out result))
          return;
        this.rounds_count = result;
      }
      else if (stBuffer.IndexOf("MaxRoundTime", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("MaxRoundTime", stBuffer), out result))
          return;
        this.max_round_time = result;
      }
      else if (stBuffer.IndexOf("StrictRoundMode", 0) == 0)
        this.strict_round_mode = false;
      else if (stBuffer.IndexOf("MatchMode", 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue("MatchMode", stBuffer), out result))
          return;
        if (result != 0)
        {
          if (result == 1)
            this.match_mode = MugenProfile.MatchMode.ALLvsALL;
          else
            this.match_mode = MugenProfile.MatchMode.P1vsALL;
        }
        else
          this.match_mode = MugenProfile.MatchMode.P1vsALL;
      }
      else
      {
        int result;
        if (stBuffer.IndexOf("TeamSide", 0) != 0 || !int.TryParse(this.GetValue("TeamSide", stBuffer), out result))
          return;
        switch (result)
        {
          case 0:
            this.team_side = MugenProfile.TeamSideMode.BOTH_SIDE;
            break;
          case 1:
            this.team_side = MugenProfile.TeamSideMode.P1_SIDE;
            break;
          case 2:
            this.team_side = MugenProfile.TeamSideMode.P2_SIDE;
            break;
          default:
            this.team_side = MugenProfile.TeamSideMode.BOTH_SIDE;
            break;
        }
      }
    }

    private void _LoadCharacters(string stBuffer)
    {
      if (stBuffer == "")
        return;
      this._characters.Add(new MugenProfile.Character_t()
      {
        name = stBuffer
      });
    }

    public void ResetConf()
    {
      this._paramMode = MugenProfile.ParamMode.MUGEN;
      this._characters.Clear();
      this.profile_name = (string) null;
      this.mugen_path = (string) null;
      this.mugen_exe = (string) null;
      this.mugen_select_cfg = (string) null;
      this.mugen_cmdline_options_base = (string) null;
      this.mugen_cmdline_options = (string) null;
      this.speed_mode = false;
      this.skip_mode = false;
      this.experimental_bps = false;
      this.debug_mode = false;
      this.auto_mode_available = false;
      this.error_retry = 0;
      this.default_color = 1;
      this.rounds_count = 1;
      this.strict_round_mode = false;
      this.max_round_time = 10;
      this.char_list_raw_data = (string) null;
    }

    protected override bool LoadCfgFile(string the_file)
    {
      this.ResetConf();
      StreamReader streamReader = (StreamReader) null;
      try
      {
        streamReader = new StreamReader(the_file, Encoding.Default);
        while (streamReader.Peek() >= 0)
        {
          string str = streamReader.ReadLine();
          if (this._paramMode == MugenProfile.ParamMode.CHARACTERS)
          {
            if (this.char_list_raw_data == null)
            {
              this.char_list_raw_data = str + Environment.NewLine;
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
            if (!this.CheckParamMode(stBuffer))
            {
              switch (this._paramMode)
              {
                case MugenProfile.ParamMode.MUGEN:
                  this._LoadMugenParams(stBuffer);
                  continue;
                case MugenProfile.ParamMode.AUTO_MODE:
                  this._LoadAutoModeParams(stBuffer);
                  continue;
                case MugenProfile.ParamMode.CHARACTERS:
                  this._LoadCharacters(stBuffer);
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
      MugenProfile.ParamMode paramMode = MugenProfile.ParamMode.NONE;
      StreamReader streamReader = (StreamReader) null;
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
            switch (this._CheckParamMode(stBuffer))
            {
              case MugenProfile.ParamMode.NONE:
                if (paramMode == MugenProfile.ParamMode.CHARACTERS)
                {
                  string str2 = stBuffer;
                  string lower = stBuffer.ToLower();
                  if (!lower.Contains("randomselect") && !lower.Contains("blank") && (!lower.Contains("empty") && !lower.Contains("none")))
                  {
                    str1 = str1 + str2 + Environment.NewLine;
                    continue;
                  }
                  continue;
                }
                continue;
              case MugenProfile.ParamMode.CHARACTERS:
                paramMode = MugenProfile.ParamMode.CHARACTERS;
                continue;
              default:
                paramMode = MugenProfile.ParamMode.NONE;
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
