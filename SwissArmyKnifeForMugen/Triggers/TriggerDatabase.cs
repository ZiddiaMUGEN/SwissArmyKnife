﻿// Decompiled with JetBrains decompiler
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
        /// <returns></returns>
        public static uint GetTriggerAddrForType(MugenAddrDatabase mugen, TriggerId triggerType)
        {
            if (mugen == null)
                return 0;
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

            public TriggerValue_t()
            {
                valueType = ValueType.VALUE_NONE;
                iValue = 0;
                fValue = 0.0f;
                mask = uint.MaxValue;
            }

            public void reset()
            {
                valueType = ValueType.VALUE_NONE;
                iValue = 0;
                fValue = 0.0f;
                mask = uint.MaxValue;
            }

            public int GetInt32Value() => iValue;

            public int GetMaskedInt32Value() => iValue & (int)mask;

            public float GetSingleValue() => fValue;

            public void SetInt32Value(int value) => iValue = value;

            public void SetSingleValue(float value) => fValue = value;

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