// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerCheckTarget
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

namespace SwissArmyKnifeForMugen.Triggers
{
    /// <summary>
    /// Represents the target of a trigger, storing the target Player, trigger type, and values.
    /// </summary>
    public class TriggerCheckTarget
    {
        /// <summary>
        /// target player for a trigger
        /// </summary>
        private Player_t _targetPlayer = new Player_t();
        /// <summary>
        /// target trigger type
        /// </summary>
        private Trigger_t _targetTrigger = new Trigger_t();
        /// <summary>
        /// start of value range for value-based trigger
        /// </summary>
        private TriggerDatabase.TriggerValue_t _triggerValueFrom = new TriggerDatabase.TriggerValue_t();
        /// <summary>
        /// end of value range for value-based trigger
        /// </summary>
        private TriggerDatabase.TriggerValue_t _triggerValueTo = new TriggerDatabase.TriggerValue_t();
        /// <summary>
        /// next command to execute for this trigger
        /// </summary>
        private CheckCommand _nextCommand;
        /// <summary>
        /// current trigger checking mode
        /// </summary>
        private CheckMode _currentMode;
        private bool _isDirty;
        /// <summary>
        /// type of operator used for this trigger
        /// </summary>
        private ValueOpType _valueOpType;

        public CheckCommand GetNextCommand() => _nextCommand;

        public void SetNextCommand(CheckCommand cmd) => _nextCommand = cmd;

        public CheckMode GetCurrentMode() => _currentMode;

        public void SetCurrentMode(CheckMode mode) => _currentMode = mode;

        public bool IsDirty() => _isDirty;

        public void SetDirty() => _isDirty = true;

        public void ResetDirty() => _isDirty = false;

        public Player_t GetTargetPlayer() => _targetPlayer;

        public void SetTargetPlayer(Player_t player) => _targetPlayer = player;

        public Trigger_t GetTargetTrigger() => _targetTrigger;

        public void SetTargetTrigger(Trigger_t trigger) => _targetTrigger = trigger;

        public TriggerDatabase.TriggerValue_t GetTargetValueFrom() => _triggerValueFrom;

        public void SetTargetValueFrom(TriggerDatabase.TriggerValue_t value) => _triggerValueFrom = value;

        public TriggerDatabase.TriggerValue_t GetTargetValueTo() => _triggerValueTo;

        public void SetTargetValueTo(TriggerDatabase.TriggerValue_t value) => _triggerValueTo = value;

        public ValueOpType GetTargetValueOpType() => _valueOpType;

        public void SetTargetValueOpType(ValueOpType flag) => _valueOpType = flag;

        /// <summary>
        /// indicates whether a trigger is ready to be activated.
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable() => _targetPlayer.playerType != Player_t.PlayerType.PLAYER_NONE && _targetTrigger.triggerType != TriggerDatabase.TriggerId.TRIGGER_NONE && _triggerValueFrom.valueType != TriggerDatabase.ValueType.VALUE_NONE && (uint)_triggerValueTo.valueType > 0U;

        /// <summary>
        /// represents possible commands that can be issued to this trigger
        /// </summary>
        public enum CheckCommand
        {
            CHECKCMD_NONE,
            CHECKCMD_STOP,
            CHECKCMD_START,
            CHECKCMD_RESUME,
        }

        /// <summary>
        /// represents modes this trigger can be in
        /// </summary>
        public enum CheckMode
        {
            CHECKMODE_STOPPED,
            CHECKMODE_STARTED,
            CHECKMODE_SUSPENDED,
        }

        /// <summary>
        /// represents a player in Mugen that a trigger can be applied to
        /// </summary>
        public class Player_t
        {
            /// <summary>
            /// type of player (slot, playerID, helperID, etc)
            /// </summary>
            public PlayerType playerType;
            /// <summary>
            /// player ID of the player represented by this object
            /// </summary>
            public int pid;

            public Player_t()
            {
                playerType = PlayerType.PLAYER_NONE;
                pid = 0;
            }

            /// <summary>
            /// represents different ways to store a reference player data
            /// </summary>
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

        /// <summary>
        /// represents a trigger with an index and a trigger type
        /// </summary>
        public class Trigger_t
        {
            /// <summary>
            /// type of trigger which is being run, from the database
            /// </summary>
            public TriggerDatabase.TriggerId triggerType;
            /// <summary>
            /// index of the trigger
            /// </summary>
            public int index;

            public Trigger_t()
            {
                triggerType = TriggerDatabase.TriggerId.TRIGGER_NONE;
                index = 0;
            }
        }

        /// <summary>
        /// possible operators for comparisons in the trigger
        /// <br/>any FROM_TO operators have a range of valid values
        /// </summary>
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
