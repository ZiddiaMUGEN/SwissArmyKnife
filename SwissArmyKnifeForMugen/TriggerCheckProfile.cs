// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerCheckProfile
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen
{
  internal class TriggerCheckProfile : ProfileBase
  {
    private string _playerString = "(Not set)";
    private string _triggerString = "(Not set)";
    private string _valueString = "(Not set)";
    private string player_key = "TargetPlayer";
    private string trigger_key = "TargetTrigger";
    private string value_key = "TargetValue";

    public string GetPlayerString() => this._playerString;

    public void SetPlayerString(string s) => this._playerString = s;

    public string GetTriggerString() => this._triggerString;

    public void SetTriggerString(string s) => this._triggerString = s;

    public string GetValueString() => this._valueString;

    public void SetValueString(string s) => this._valueString = s;

    public void ResetConf()
    {
      this._playerString = "(Not set)";
      this._triggerString = "(Not set)";
      this._valueString = "(Not set)";
    }

    private void _LoadParams(string stBuffer)
    {
      if (stBuffer.IndexOf(this.player_key, 0) == 0)
        this._playerString = this.GetValue(this.player_key, stBuffer);
      else if (stBuffer.IndexOf(this.trigger_key, 0) == 0)
      {
        this._triggerString = this.GetValue(this.trigger_key, stBuffer);
      }
      else
      {
        if (stBuffer.IndexOf(this.value_key, 0) != 0)
          return;
        this._valueString = this.GetValue(this.value_key, stBuffer);
      }
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
            this._LoadParams(stBuffer);
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
      string profilePath = this.GetProfilePath();
      Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
      string contents = ";" + Environment.NewLine + "; Filename:" + this.GetProfileFile() + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + "[TriggerChecker]" + Environment.NewLine + "TargetPlayer = " + this._playerString + Environment.NewLine + "TargetTrigger = " + this._triggerString + Environment.NewLine + "TargetValue = " + this._valueString + Environment.NewLine;
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
