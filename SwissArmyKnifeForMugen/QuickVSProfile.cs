// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.QuickVSProfile
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwissArmyKnifeForMugen
{
  internal class QuickVSProfile : ProfileBase
  {
    private string[] _p1History = new string[20]
    {
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      ""
    };
    private string[] _p2History = new string[20]
    {
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      ""
    };
    private string[] _p3History = new string[20]
    {
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      ""
    };
    private string[] _p4History = new string[20]
    {
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      "",
      ""
    };
    private int _rounds = 2;
    private string _p1def = "(No setting)";
    private string _p2def = "(No setting)";
    private int _p1color = 1;
    private int _p2color = 1;
    private bool _p1ai = true;
    private bool _p2ai = true;
    private int _p1lifeValue = 1000;
    private int _p2lifeValue = 1000;
    private string _p3def = "(No setting)";
    private string _p4def = "(No setting)";
    private int _p3color = 1;
    private int _p4color = 1;
    private bool _p3ai = true;
    private bool _p4ai = true;
    private int _p3lifeValue = 1000;
    private int _p4lifeValue = 1000;
    private string _stage = "";
    private string mode_vs_mode_key = "[VSMode]";
    private string mode_p1_history_key = "[P1History]";
    private string mode_p2_history_key = "[P2History]";
    private string mode_p3_history_key = "[P3History]";
    private string mode_p4_history_key = "[P4History]";
    private QuickVSProfile.ParamMode _paramMode = QuickVSProfile.ParamMode.VSMode;
    private string p1_color_key = "P1Color";
    private string p1_ai_key = "P1Ai";
    private string p1_life_flag_key = "P1LifeFlag";
    private string p1_life_value_key = "P1LifeValue";
    private string p1_power_flag_key = "P1PowerFlag";
    private string p1_power_value_key = "P1PowerValue";
    private string p2_color_key = "P2Color";
    private string p2_ai_key = "P2Ai";
    private string p2_life_flag_key = "P2LifeFlag";
    private string p2_life_value_key = "P2LifeValue";
    private string p2_power_flag_key = "P2PowerFlag";
    private string p2_power_value_key = "P2PowerValue";
    private string p3_color_key = "P3Color";
    private string p3_ai_key = "P3Ai";
    private string p3_life_flag_key = "P3LifeFlag";
    private string p3_life_value_key = "P3LifeValue";
    private string p3_power_flag_key = "P3PowerFlag";
    private string p3_power_value_key = "P3PowerValue";
    private string p4_color_key = "P4Color";
    private string p4_ai_key = "P4Ai";
    private string p4_life_flag_key = "P4LifeFlag";
    private string p4_life_value_key = "P4LifeValue";
    private string p4_power_flag_key = "P4PowerFlag";
    private string p4_power_value_key = "P4PowerValue";
    private string win_rounds_key = "WinRounds";
    private bool _p1lifeFlag;
    private bool _p2lifeFlag;
    private bool _p1powerFlag;
    private int _p1powerValue;
    private bool _p2powerFlag;
    private int _p2powerValue;
    private bool _p3enable;
    private bool _p4enable;
    private bool _p3lifeFlag;
    private bool _p4lifeFlag;
    private bool _p3powerFlag;
    private int _p3powerValue;
    private bool _p4powerFlag;
    private int _p4powerValue;
    private bool _noMusic;
    private bool _noSound;

    public int GetRounds() => this._rounds;

    public void SetRounds(int rounds) => this._rounds = rounds;

    public string GetP1Def()
    {
      if (this._p1History[0] != null && this._p1History[0] != "")
        this._p1def = this._p1History[0];
      return this._p1def;
    }

    public void SetP1Def(string newDef)
    {
      this._p1def = newDef;
      bool flag = false;
      int index1;
      for (index1 = 0; index1 < this._p1History.Length; ++index1)
      {
        if (this._p1def.CompareTo(this._p1History[index1]) == 0)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index2 = index1 - 1; index2 >= 0; --index2)
          this._p1History[index2 + 1] = this._p1History[index2];
      }
      else
      {
        for (int index2 = this._p1History.Length - 2; index2 >= 0; --index2)
          this._p1History[index2 + 1] = this._p1History[index2];
      }
      this._p1History[0] = this._p1def;
    }

    public string GetP2Def()
    {
      if (this._p2History[0] != null && this._p2History[0] != "")
        this._p2def = this._p2History[0];
      return this._p2def;
    }

    public void SetP2Def(string newDef)
    {
      this._p2def = newDef;
      bool flag = false;
      int index1;
      for (index1 = 0; index1 < this._p2History.Length; ++index1)
      {
        if (this._p2def.CompareTo(this._p2History[index1]) == 0)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index2 = index1 - 1; index2 >= 0; --index2)
          this._p2History[index2 + 1] = this._p2History[index2];
      }
      else
      {
        for (int index2 = this._p2History.Length - 2; index2 >= 0; --index2)
          this._p2History[index2 + 1] = this._p2History[index2];
      }
      this._p2History[0] = this._p2def;
    }

    public bool IsP3Enabled() => this._p3enable;

    public string GetP3Def()
    {
      if (!this._p3enable)
        return "(No setting)";
      if (this._p3History[0] != null && this._p3History[0] != "")
        this._p3def = this._p3History[0];
      return this._p3def;
    }

    public void SetP3Def(string newDef)
    {
      switch (newDef)
      {
        case "":
          this._p3enable = false;
          break;
        default:
          this._p3enable = true;
          this._p3def = newDef;
          bool flag = false;
          int index1;
          for (index1 = 0; index1 < this._p3History.Length; ++index1)
          {
            if (this._p3def.CompareTo(this._p3History[index1]) == 0)
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            for (int index2 = index1 - 1; index2 >= 0; --index2)
              this._p3History[index2 + 1] = this._p3History[index2];
          }
          else
          {
            for (int index2 = this._p3History.Length - 2; index2 >= 0; --index2)
              this._p3History[index2 + 1] = this._p3History[index2];
          }
          this._p3History[0] = this._p3def;
          break;
      }
    }

    public bool IsP4Enabled() => this._p4enable;

    public string GetP4Def()
    {
      if (!this._p4enable)
        return "(No setting)";
      if (this._p4History[0] != null && this._p4History[0] != "")
        this._p4def = this._p4History[0];
      return this._p4def;
    }

    public void SetP4Def(string newDef)
    {
      switch (newDef)
      {
        case "":
          this._p4enable = false;
          break;
        default:
          this._p4def = newDef;
          this._p4enable = true;
          bool flag = false;
          int index1;
          for (index1 = 0; index1 < this._p4History.Length; ++index1)
          {
            if (this._p4def.CompareTo(this._p4History[index1]) == 0)
            {
              flag = true;
              break;
            }
          }
          if (flag)
          {
            for (int index2 = index1 - 1; index2 >= 0; --index2)
              this._p4History[index2 + 1] = this._p4History[index2];
          }
          else
          {
            for (int index2 = this._p4History.Length - 2; index2 >= 0; --index2)
              this._p4History[index2 + 1] = this._p4History[index2];
          }
          this._p4History[0] = this._p4def;
          break;
      }
    }

    public string[] GetP1History() => ((IEnumerable<string>) this._p1History).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();

    public string[] GetP2History() => ((IEnumerable<string>) this._p2History).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();

    public string[] GetP3History() => ((IEnumerable<string>) this._p3History).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();

    public string[] GetP4History() => ((IEnumerable<string>) this._p4History).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();

    public int GetP1Color() => this._p1color;

    public void SetP1Color(int color) => this._p1color = color;

    public int GetP2Color() => this._p2color;

    public void SetP2Color(int color) => this._p2color = color;

    public int GetP3Color() => this._p3color;

    public void SetP3Color(int color) => this._p3color = color;

    public int GetP4Color() => this._p4color;

    public void SetP4Color(int color) => this._p4color = color;

    public bool GetP1AI() => this._p1ai;

    public void SetP1AI(bool ai) => this._p1ai = ai;

    public bool GetP2AI() => this._p2ai;

    public void SetP2AI(bool ai) => this._p2ai = ai;

    public bool GetP3AI() => this._p3ai;

    public void SetP3AI(bool ai) => this._p3ai = ai;

    public bool GetP4AI() => this._p4ai;

    public void SetP4AI(bool ai) => this._p4ai = ai;

    public bool GetP1LifeFlag() => this._p1lifeFlag;

    public void SetP1LifeFlag(bool flag) => this._p1lifeFlag = flag;

    public int GetP1LifeValue() => this._p1lifeValue;

    public void SetP1LifeValue(int value) => this._p1lifeValue = value;

    public bool GetP2LifeFlag() => this._p2lifeFlag;

    public void SetP2LifeFlag(bool flag) => this._p2lifeFlag = flag;

    public int GetP2LifeValue() => this._p2lifeValue;

    public void SetP2LifeValue(int value) => this._p2lifeValue = value;

    public bool GetP3LifeFlag() => this._p3lifeFlag;

    public void SetP3LifeFlag(bool flag) => this._p3lifeFlag = flag;

    public int GetP3LifeValue() => this._p3lifeValue;

    public void SetP3LifeValue(int value) => this._p3lifeValue = value;

    public bool GetP4LifeFlag() => this._p4lifeFlag;

    public void SetP4LifeFlag(bool flag) => this._p4lifeFlag = flag;

    public int GetP4LifeValue() => this._p4lifeValue;

    public void SetP4LifeValue(int value) => this._p4lifeValue = value;

    public bool GetP1PowerFlag() => this._p1powerFlag;

    public void SetP1PowerFlag(bool flag) => this._p1powerFlag = flag;

    public int GetP1PowerValue() => this._p1powerValue;

    public void SetP1PowerValue(int value) => this._p1powerValue = value;

    public bool GetP2PowerFlag() => this._p2powerFlag;

    public void SetP2PowerFlag(bool flag) => this._p2powerFlag = flag;

    public int GetP2PowerValue() => this._p2powerValue;

    public void SetP2PowerValue(int value) => this._p2powerValue = value;

    public bool GetP3PowerFlag() => this._p3powerFlag;

    public void SetP3PowerFlag(bool flag) => this._p3powerFlag = flag;

    public int GetP3PowerValue() => this._p3powerValue;

    public void SetP3PowerValue(int value) => this._p3powerValue = value;

    public bool GetP4PowerFlag() => this._p4powerFlag;

    public void SetP4PowerFlag(bool flag) => this._p4powerFlag = flag;

    public int GetP4PowerValue() => this._p4powerValue;

    public void SetP4PowerValue(int value) => this._p4powerValue = value;

    public bool GetNoMusicFlag() => this._noMusic;

    public void SetNoMusicFlag(bool flag) => this._noMusic = flag;

    public bool GetNoSoundFlag() => this._noSound;

    public void SetNoSoundFlag(bool flag) => this._noSound = flag;

    public string GetStage() => this._stage;

    public void SetStage(string stage) => this._stage = stage;

    private void _LoadVSModeParams(string stBuffer)
    {
      if (stBuffer.IndexOf(this.p1_color_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_color_key, stBuffer), out result) || result <= 0 || result >= 13)
          return;
        this._p1color = result;
      }
      else if (stBuffer.IndexOf(this.p2_color_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_color_key, stBuffer), out result) || result <= 0 || result >= 13)
          return;
        this._p2color = result;
      }
      else if (stBuffer.IndexOf(this.p3_color_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_color_key, stBuffer), out result) || result <= 0 || result >= 13)
          return;
        this._p3color = result;
      }
      else if (stBuffer.IndexOf(this.p4_color_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_color_key, stBuffer), out result) || result <= 0 || result >= 13)
          return;
        this._p4color = result;
      }
      else if (stBuffer.IndexOf(this.p1_ai_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_ai_key, stBuffer), out result) || result == 0)
          return;
        this._p1ai = true;
      }
      else if (stBuffer.IndexOf(this.p2_ai_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_ai_key, stBuffer), out result) || result == 0)
          return;
        this._p2ai = true;
      }
      else if (stBuffer.IndexOf(this.p3_ai_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_ai_key, stBuffer), out result) || result == 0)
          return;
        this._p3ai = true;
      }
      else if (stBuffer.IndexOf(this.p4_ai_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_ai_key, stBuffer), out result) || result == 0)
          return;
        this._p4ai = true;
      }
      else if (stBuffer.IndexOf(this.p1_life_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_life_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p1lifeFlag = true;
      }
      else if (stBuffer.IndexOf(this.p1_life_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_life_value_key, stBuffer), out result) || result < 0)
          return;
        this._p1lifeValue = result;
      }
      else if (stBuffer.IndexOf(this.p1_power_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_power_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p1powerFlag = true;
      }
      else if (stBuffer.IndexOf(this.p1_power_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p1_power_value_key, stBuffer), out result) || result < 0)
          return;
        this._p1powerValue = result;
      }
      else if (stBuffer.IndexOf(this.p2_life_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_life_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p2lifeFlag = true;
      }
      else if (stBuffer.IndexOf(this.p2_life_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_life_value_key, stBuffer), out result) || result < 0)
          return;
        this._p2lifeValue = result;
      }
      else if (stBuffer.IndexOf(this.p2_power_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_power_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p2powerFlag = true;
      }
      else if (stBuffer.IndexOf(this.p2_power_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p2_power_value_key, stBuffer), out result) || result < 0)
          return;
        this._p2powerValue = result;
      }
      else if (stBuffer.IndexOf(this.p3_life_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_life_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p3lifeFlag = true;
      }
      else if (stBuffer.IndexOf(this.p3_life_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_life_value_key, stBuffer), out result) || result < 0)
          return;
        this._p3lifeValue = result;
      }
      else if (stBuffer.IndexOf(this.p3_power_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_power_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p3powerFlag = true;
      }
      else if (stBuffer.IndexOf(this.p3_power_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p3_power_value_key, stBuffer), out result) || result < 0)
          return;
        this._p3powerValue = result;
      }
      else if (stBuffer.IndexOf(this.p4_life_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_life_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p4lifeFlag = true;
      }
      else if (stBuffer.IndexOf(this.p4_life_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_life_value_key, stBuffer), out result) || result < 0)
          return;
        this._p4lifeValue = result;
      }
      else if (stBuffer.IndexOf(this.p4_power_flag_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_power_flag_key, stBuffer), out result) || result == 0)
          return;
        this._p4powerFlag = true;
      }
      else if (stBuffer.IndexOf(this.p4_power_value_key, 0) == 0)
      {
        int result;
        if (!int.TryParse(this.GetValue(this.p4_power_value_key, stBuffer), out result) || result < 0)
          return;
        this._p4powerValue = result;
      }
      else
      {
        int result;
        if (stBuffer.IndexOf(this.win_rounds_key, 0) != 0 || !int.TryParse(this.GetValue(this.win_rounds_key, stBuffer), out result) || (result <= 0 || result >= 100))
          return;
        this._rounds = result;
      }
    }

    private void _LoadP1History(string stBuffer)
    {
      if (stBuffer == "")
        return;
      string str = stBuffer;
      for (int index = this._p1History.Length - 2; index >= 0; --index)
        this._p1History[index + 1] = this._p1History[index];
      this._p1History[0] = str;
    }

    private void _LoadP2History(string stBuffer)
    {
      if (stBuffer == "")
        return;
      string str = stBuffer;
      for (int index = this._p2History.Length - 2; index >= 0; --index)
        this._p2History[index + 1] = this._p2History[index];
      this._p2History[0] = str;
    }

    private void _LoadP3History(string stBuffer)
    {
      if (stBuffer == "")
        return;
      string str = stBuffer;
      for (int index = this._p3History.Length - 2; index >= 0; --index)
        this._p3History[index + 1] = this._p3History[index];
      this._p3History[0] = str;
    }

    private void _LoadP4History(string stBuffer)
    {
      if (stBuffer == "")
        return;
      string str = stBuffer;
      for (int index = this._p4History.Length - 2; index >= 0; --index)
        this._p4History[index + 1] = this._p4History[index];
      this._p4History[0] = str;
    }

    public void ResetConf()
    {
      string[] strArray1 = new string[20]
      {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
      };
      string[] strArray2 = new string[20]
      {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
      };
      string[] strArray3 = new string[20]
      {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
      };
      string[] strArray4 = new string[20]
      {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
      };
      this._p1History = strArray1;
      this._p2History = strArray2;
      this._p3History = strArray3;
      this._p4History = strArray4;
      this._rounds = 2;
      this._p1def = "(No setting)";
      this._p2def = "(No setting)";
      this._p3def = "(No setting)";
      this._p4def = "(No setting)";
      this._p1color = 1;
      this._p2color = 1;
      this._p3color = 1;
      this._p4color = 1;
      this._p1ai = true;
      this._p2ai = true;
      this._p3ai = true;
      this._p4ai = true;
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
          string stBuffer = streamReader.ReadLine().Trim();
          if (stBuffer.IndexOf(";", 0) != 0)
          {
            int length = stBuffer.IndexOf(';');
            if (length > 0)
              stBuffer = stBuffer.Substring(0, length);
            if (!this.CheckParamMode(stBuffer))
            {
              switch (this._paramMode)
              {
                case QuickVSProfile.ParamMode.VSMode:
                  this._LoadVSModeParams(stBuffer);
                  continue;
                case QuickVSProfile.ParamMode.P1History:
                  this._LoadP1History(stBuffer);
                  continue;
                case QuickVSProfile.ParamMode.P2History:
                  this._LoadP2History(stBuffer);
                  continue;
                case QuickVSProfile.ParamMode.P3History:
                  this._LoadP3History(stBuffer);
                  continue;
                case QuickVSProfile.ParamMode.P4History:
                  this._LoadP4History(stBuffer);
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

    private QuickVSProfile.ParamMode _CheckParamMode(string stBuffer)
    {
      if (stBuffer.Length < 2 || stBuffer[0] != '[' || stBuffer[stBuffer.Length - 1] != ']')
        return QuickVSProfile.ParamMode.NONE;
      if (stBuffer.IndexOf(this.mode_vs_mode_key, 0) == 0)
        return QuickVSProfile.ParamMode.VSMode;
      if (stBuffer.IndexOf(this.mode_p1_history_key, 0) == 0)
        return QuickVSProfile.ParamMode.P1History;
      if (stBuffer.IndexOf(this.mode_p2_history_key, 0) == 0)
        return QuickVSProfile.ParamMode.P2History;
      if (stBuffer.IndexOf(this.mode_p3_history_key, 0) == 0)
        return QuickVSProfile.ParamMode.P3History;
      return stBuffer.IndexOf(this.mode_p4_history_key, 0) == 0 ? QuickVSProfile.ParamMode.P4History : QuickVSProfile.ParamMode.NONE;
    }

    private bool CheckParamMode(string stBuffer)
    {
      QuickVSProfile.ParamMode paramMode = this._CheckParamMode(stBuffer);
      switch (paramMode)
      {
        case QuickVSProfile.ParamMode.VSMode:
        case QuickVSProfile.ParamMode.P1History:
        case QuickVSProfile.ParamMode.P2History:
        case QuickVSProfile.ParamMode.P3History:
        case QuickVSProfile.ParamMode.P4History:
          this._paramMode = paramMode;
          return true;
        default:
          return false;
      }
    }

    public bool SaveConfigData()
    {
      string profilePath = this.GetProfilePath();
      Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
      string str1 = ";" + Environment.NewLine + "; ファイル名:" + this.GetProfileFile() + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + "[VSMode]" + Environment.NewLine + "P1Color = " + (object) this._p1color + Environment.NewLine + "P1Ai = " + (this._p1ai ? (object) "1" : (object) "0") + Environment.NewLine + "P1LifeFlag = " + (this._p1lifeFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P1LifeValue = " + (object) this._p1lifeValue + Environment.NewLine + "P1PowerFlag = " + (this._p1powerFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P1PowerValue = " + (object) this._p1powerValue + Environment.NewLine + "P2Color = " + (object) this._p2color + Environment.NewLine + "P2Ai = " + (this._p2ai ? (object) "1" : (object) "0") + Environment.NewLine + "P2LifeFlag = " + (this._p2lifeFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P2LifeValue = " + (object) this._p2lifeValue + Environment.NewLine + "P2PowerFlag = " + (this._p2powerFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P2PowerValue = " + (object) this._p2powerValue + Environment.NewLine + "P3Color = " + (object) this._p3color + Environment.NewLine + "P3Ai = " + (this._p3ai ? (object) "1" : (object) "0") + Environment.NewLine + "P3LifeFlag = " + (this._p3lifeFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P3LifeValue = " + (object) this._p3lifeValue + Environment.NewLine + "P3PowerFlag = " + (this._p3powerFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P3PowerValue = " + (object) this._p3powerValue + Environment.NewLine + "P4Color = " + (object) this._p4color + Environment.NewLine + "P4Ai = " + (this._p4ai ? (object) "1" : (object) "0") + Environment.NewLine + "P4LifeFlag = " + (this._p4lifeFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P4LifeValue = " + (object) this._p4lifeValue + Environment.NewLine + "P4PowerFlag = " + (this._p4powerFlag ? (object) "1" : (object) "0") + Environment.NewLine + "P4PowerValue = " + (object) this._p4powerValue + Environment.NewLine + Environment.NewLine + "WinRounds = " + (object) this._rounds + Environment.NewLine + Environment.NewLine + "[P1History]" + Environment.NewLine;
      for (int index = this._p1History.Length - 1; index >= 0; --index)
        str1 = str1 + this._p1History[index] + Environment.NewLine;
      string str2 = str1 + Environment.NewLine + "[P2History]" + Environment.NewLine;
      for (int index = this._p2History.Length - 1; index >= 0; --index)
        str2 = str2 + this._p2History[index] + Environment.NewLine;
      string str3 = str2 + Environment.NewLine + "[P3History]" + Environment.NewLine;
      for (int index = this._p3History.Length - 1; index >= 0; --index)
        str3 = str3 + this._p3History[index] + Environment.NewLine;
      string contents = str3 + Environment.NewLine + "[P4History]" + Environment.NewLine;
      for (int index = this._p4History.Length - 1; index >= 0; --index)
        contents = contents + this._p4History[index] + Environment.NewLine;
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

    private enum ParamMode
    {
      NONE,
      VSMode,
      P1History,
      P2History,
      P3History,
      P4History,
    }
  }
}
