// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerCheckTarget
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

namespace SwissArmyKnifeForMugen
{
  public class TriggerCheckTarget
  {
    private TriggerCheckTarget.Player_t _targetPlayer = new TriggerCheckTarget.Player_t();
    private TriggerCheckTarget.Trigger_t _targetTrigger = new TriggerCheckTarget.Trigger_t();
    private TriggerDatabase.TriggerValue_t _triggerValueFrom = new TriggerDatabase.TriggerValue_t();
    private TriggerDatabase.TriggerValue_t _triggerValueTo = new TriggerDatabase.TriggerValue_t();
    private TriggerCheckTarget.CheckCommand _nextCommand;
    private TriggerCheckTarget.CheckMode _currentMode;
    private bool _isDirty;
    private TriggerCheckTarget.ValueOpType _valueOpType;

    public TriggerCheckTarget.CheckCommand GetNextCommand() => this._nextCommand;

    public void SetNextCommand(TriggerCheckTarget.CheckCommand cmd) => this._nextCommand = cmd;

    public TriggerCheckTarget.CheckMode GetCurrentMode() => this._currentMode;

    public void SetCurrentMode(TriggerCheckTarget.CheckMode mode) => this._currentMode = mode;

    public bool IsDirty() => this._isDirty;

    public void SetDirty() => this._isDirty = true;

    public void ResetDirty() => this._isDirty = false;

    public TriggerCheckTarget.Player_t GetTargetPlayer() => this._targetPlayer;

    public void SetTargetPlayer(TriggerCheckTarget.Player_t player) => this._targetPlayer = player;

    public TriggerCheckTarget.Trigger_t GetTargetTrigger() => this._targetTrigger;

    public void SetTargetTrigger(TriggerCheckTarget.Trigger_t trigger) => this._targetTrigger = trigger;

    public TriggerDatabase.TriggerValue_t GetTargetValueFrom() => this._triggerValueFrom;

    public void SetTargetValueFrom(TriggerDatabase.TriggerValue_t value) => this._triggerValueFrom = value;

    public TriggerDatabase.TriggerValue_t GetTargetValueTo() => this._triggerValueTo;

    public void SetTargetValueTo(TriggerDatabase.TriggerValue_t value) => this._triggerValueTo = value;

    public TriggerCheckTarget.ValueOpType GetTargetValueOpType() => this._valueOpType;

    public void SetTargetValueOpType(TriggerCheckTarget.ValueOpType flag) => this._valueOpType = flag;

    public bool IsAvailable() => this._targetPlayer.playerType != TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && this._targetTrigger.triggerType != TriggerDatabase.TriggerId.TRIGGER_NONE && this._triggerValueFrom.valueType != TriggerDatabase.ValueType.VALUE_NONE && (uint) this._triggerValueTo.valueType > 0U;

    public enum CheckCommand
    {
      CHECKCMD_NONE,
      CHECKCMD_STOP,
      CHECKCMD_START,
      CHECKCMD_RESUME,
    }

    public enum CheckMode
    {
      CHECKMODE_STOPPED,
      CHECKMODE_STARTED,
      CHECKMODE_SUSPENDED,
    }

    public class Player_t
    {
      public TriggerCheckTarget.Player_t.PlayerType playerType;
      public int pid;

      public Player_t()
      {
        this.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE;
        this.pid = 0;
      }

      public enum PlayerType
      {
        PLAYER_NONE,
        PLAYER_P1,
        PLAYER_P2,
        PLAYER_P3,
        PLAYER_P4,
        PLAYER_PLAYERID,
        PLAYER_P1_HELPERID,
        PLAYER_P2_HELPERID,
        PLAYER_P3_HELPERID,
        PLAYER_P4_HELPERID,
      }
    }

    public class Trigger_t
    {
      public TriggerDatabase.TriggerId triggerType;
      public int index;

      public Trigger_t()
      {
        this.triggerType = TriggerDatabase.TriggerId.TRIGGER_NONE;
        this.index = 0;
      }
    }

    public enum ValueOpType
    {
      VALUE_OP_NONE,
      VALUE_OP_EQ,
      VALUE_OP_NOT_EQ,
      VALUE_OP_LT,
      VALUE_OP_LE,
      VALUE_OP_GT,
      VALUE_OP_GE,
      VALUE_OP_FROM_TO,
      VALUE_OP_L_FROM_TO,
      VALUE_OP_G_FROM_TO,
      VALUE_OP_LG_FROM_TO,
      VALUE_OP_NOT_FROM_TO,
      VALUE_OP_NOT_L_FROM_TO,
      VALUE_OP_NOT_G_FROM_TO,
      VALUE_OP_NOT_LG_FROM_TO,
    }
  }
}
