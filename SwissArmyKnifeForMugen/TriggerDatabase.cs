// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerDatabase
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

namespace SwissArmyKnifeForMugen
{
  public class TriggerDatabase
  {
    public static TriggerDatabase.ValueType[] TriggerType = new TriggerDatabase.ValueType[7]
    {
      TriggerDatabase.ValueType.VALUE_NONE,
      TriggerDatabase.ValueType.VALUE_INT,
      TriggerDatabase.ValueType.VALUE_INT,
      TriggerDatabase.ValueType.VALUE_INT,
      TriggerDatabase.ValueType.VALUE_FLOAT,
      TriggerDatabase.ValueType.VALUE_INT,
      TriggerDatabase.ValueType.VALUE_FLOAT
    };
    private static uint CONST_TRIGGER_LENGTH = 7;

    public static uint GetTriggerAddrForType(MugenAddrDatabase mugen, int triggerType)
    {
      if (mugen == null)
        return 0;
      switch (triggerType)
      {
        case 1:
          return mugen.ALIVE_PLAYER_OFFSET;
        case 2:
          return mugen.STATE_NO_PLAYER_OFFSET;
        case 3:
          return mugen.SYS_VAR_PLAYER_OFFSET;
        case 4:
          return mugen.SYS_FVAR_PLAYER_OFFSET;
        case 5:
          return mugen.VAR_PLAYER_OFFSET;
        case 6:
          return mugen.FVAR_PLAYER_OFFSET;
        default:
          return 0;
      }
    }

    public static bool IsTriggerAvailable(int triggerId) => triggerId >= 0 && (long) triggerId < (long) TriggerDatabase.CONST_TRIGGER_LENGTH;

    public enum TriggerId
    {
      TRIGGER_NONE,
      TRIGGER_ALIVE,
      TRIGGER_STATENO,
      TRIGGER_SYSVAR,
      TRIGGER_SYSFVAR,
      TRIGGER_VAR,
      TRIGGER_FVAR,
    }

    public enum ValueType
    {
      VALUE_NONE,
      VALUE_ANY,
      VALUE_INT,
      VALUE_FLOAT,
    }

    public class TriggerValue_t
    {
      public TriggerDatabase.ValueType valueType;
      public uint mask;
      private int iValue;
      private float fValue;

      public TriggerValue_t()
      {
        this.valueType = TriggerDatabase.ValueType.VALUE_NONE;
        this.iValue = 0;
        this.fValue = 0.0f;
        this.mask = uint.MaxValue;
      }

      public void reset()
      {
        this.valueType = TriggerDatabase.ValueType.VALUE_NONE;
        this.iValue = 0;
        this.fValue = 0.0f;
        this.mask = uint.MaxValue;
      }

      public int GetInt32Value() => this.iValue;

      public int GetMaskedInt32Value() => this.iValue & (int) this.mask;

      public float GetSingleValue() => this.fValue;

      public void SetInt32Value(int value) => this.iValue = value;

      public void SetSingleValue(float value) => this.fValue = value;

      public bool isEqual(TriggerDatabase.TriggerValue_t value)
      {
        if (this.valueType != value.valueType)
          return false;
        switch (this.valueType)
        {
          case TriggerDatabase.ValueType.VALUE_NONE:
            return false;
          case TriggerDatabase.ValueType.VALUE_ANY:
            return false;
          case TriggerDatabase.ValueType.VALUE_INT:
            return this.iValue == value.iValue;
          case TriggerDatabase.ValueType.VALUE_FLOAT:
            return (double) this.fValue == (double) value.fValue;
          default:
            return false;
        }
      }
    }
  }
}
