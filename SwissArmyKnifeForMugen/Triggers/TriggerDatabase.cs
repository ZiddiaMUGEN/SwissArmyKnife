// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TriggerDatabase
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Databases;

namespace SwissArmyKnifeForMugen.Triggers
{
    /// <summary>
    /// Stores all the possible trigger types, their valid operators, the addresses to monitor, and the value types.
    /// </summary>
    public class TriggerDatabase
    {

        /// <summary>
        /// provides the offset (from player root) to monitor for a trigger breakpoint.
        /// </summary>
        /// <param name="mugen">address database to use</param>
        /// <param name="triggerType">type of trigger to check</param>
        /// <param name="isOffsetFromBase">results as true if the trigger is base-offset, false if it is player-offset</param>
        /// <returns></returns>
        public static uint GetTriggerAddrForType(MugenAddrDatabase mugen, TriggerId triggerType, ref bool isOffsetFromBase)
        {
            if (mugen == null)
                return 0;
            isOffsetFromBase = false;
            switch (triggerType)
            {
                case TriggerId.TRIGGER_ALIVE:
                    return mugen.ALIVE_PLAYER_OFFSET;
                case TriggerId.TRIGGER_STATENO:
                    return mugen.STATE_NO_PLAYER_OFFSET;
                case TriggerId.TRIGGER_SYSVAR:
                    return mugen.SYS_VAR_PLAYER_OFFSET;
                case TriggerId.TRIGGER_SYSFVAR:
                    return mugen.SYS_FVAR_PLAYER_OFFSET;
                case TriggerId.TRIGGER_VAR:
                    return mugen.VAR_PLAYER_OFFSET;
                case TriggerId.TRIGGER_FVAR:
                    return mugen.FVAR_PLAYER_OFFSET;
                case TriggerId.TRIGGER_DAMAGE:
                    return mugen.DAMAGE_PLAYER_OFFSET;
                case TriggerId.TRIGGER_NUMHELPER:
                case TriggerId.TRIGGER_NUMHELPER_ID:
                case TriggerId.TRIGGER_NUMPROJ_ID:
                case TriggerId.TRIGGER_NUMEXPLOD_ID:
                case TriggerId.TRIGGER_NUMTARGET:
                case TriggerId.TRIGGER_HASTARGET:
                    isOffsetFromBase = true;
                    return mugen.GAMETIME_BASE_OFFSET;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// returns a ValueType which will be output for a specified trigger type.
        /// </summary>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        public static ValueType GetTriggerValueType(TriggerId triggerType)
        {
            switch (triggerType)
            {
                case TriggerId.TRIGGER_ALIVE:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_STATENO:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_SYSVAR:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_SYSFVAR:
                    return ValueType.VALUE_FLOAT;
                case TriggerId.TRIGGER_VAR:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_FVAR:
                    return ValueType.VALUE_FLOAT;
                case TriggerId.TRIGGER_NUMHELPER:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_NUMHELPER_ID:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_NUMPROJ_ID:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_NUMEXPLOD_ID:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_NUMTARGET:
                    return ValueType.VALUE_INT;
                case TriggerId.TRIGGER_HASTARGET:
                    return ValueType.VALUE_BOOL;
                case TriggerId.TRIGGER_DAMAGE:
                    return ValueType.VALUE_INT;
                default:
                    return ValueType.VALUE_NONE;
            }
        }

        /// <summary>
        /// valid types of triggers.
        /// </summary>
        public enum TriggerId
        {
            TRIGGER_NONE,
            TRIGGER_ALIVE,
            TRIGGER_STATENO,
            TRIGGER_SYSVAR,
            TRIGGER_SYSFVAR,
            TRIGGER_VAR,
            TRIGGER_FVAR,
			TRIGGER_NUMHELPER,
            TRIGGER_NUMHELPER_ID,
            TRIGGER_NUMPROJ_ID,
            TRIGGER_NUMEXPLOD_ID,
            TRIGGER_NUMTARGET,
            TRIGGER_HASTARGET,
            TRIGGER_DAMAGE,
        }

        /// <summary>
        /// valid return values from a trigger.
        /// </summary>
        public enum ValueType
        {
            VALUE_NONE,
            VALUE_ANY,
            VALUE_INT,
            VALUE_FLOAT,
            VALUE_BOOL,
        }

        /// <summary>
        /// stores the current value of a trigger
        /// </summary>
        public class TriggerValue_t
        {
            public ValueType valueType;
            public uint mask;
            private int iValue;
            private float fValue;
            private bool bValue;

            public TriggerValue_t()
            {
                valueType = ValueType.VALUE_NONE;
                iValue = 0;
                fValue = 0.0f;
                bValue = false;
                mask = uint.MaxValue;
            }

            public void reset()
            {
                valueType = ValueType.VALUE_NONE;
                iValue = 0;
                fValue = 0.0f;
                bValue = false;
                mask = uint.MaxValue;
            }

            public int GetInt32Value() => iValue;

            public int GetMaskedInt32Value() => iValue & (int)mask;

            public float GetSingleValue() => fValue;

            public bool GetBoolValue() => bValue;

            public void SetInt32Value(int value) => iValue = value;

            public void SetSingleValue(float value) => fValue = value;

            public void SetBoolValue(bool value) => bValue = value;

            public bool isEqual(TriggerValue_t value)
            {
                if (valueType != value.valueType)
                    return false;
                switch (valueType)
                {
                    case ValueType.VALUE_NONE:
                        return false;
                    case ValueType.VALUE_ANY:
                        return false;
                    case ValueType.VALUE_INT:
                        return iValue == value.iValue;
                    case ValueType.VALUE_FLOAT:
                        return fValue == (double)value.fValue;
                    case ValueType.VALUE_BOOL:
                        return bValue == value.bValue;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// this exists because I was too lazy to remove it completely
        /// </summary>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        internal static bool IsTriggerAvailable(TriggerId triggerType)
        {
            return true;
        }
    }
}
