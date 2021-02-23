// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MugenAddrDatabase
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

namespace SwissArmyKnifeForMugen
{
    public class MugenAddrDatabase
    {
        public bool USE_NEW_DEBUG_COLOR_ADDR = false;
        public uint[] NEW_DEBUG_COLOR_OFFSETS = null;
        public uint[] NEW_DEBUG_COLOR_SN_OFFSETS = null;

        public uint MUGEN_POINTER_BASE_OFFSET = 4938572;
        public uint PAUSE_ADDR = 4942465;
        public uint CMD_CURRENT_INDEX_ADDR = 4938056;
        public uint CMD_NEXT_INDEX_ADDR = 4938060;
        public uint CMD_KEY_ADDR = 4937032;
        public uint DEMO_BASE_OFFSET = 48012;
        public uint MUGEN_ACTIVE_BASE_OFFSET = 48008;
        public uint SPEED_MODE_BASE_OFFSET = 51508;
        public uint SKIP_MODE_BASE_OFFSET = 46080;
        public uint DEBUG_MODE_BASE_OFFSET = 50464;
        public uint DEBUG_TARGET_BASE_OFFSET = 50456;
        public uint GAMETIME_BASE_OFFSET = 46076;
        public uint SCREEN_X_BASE_OFFSET = 46120;
        public uint SCREEN_Y_BASE_OFFSET = 46132;
        public uint ROUND_STATE_BASE_OFFSET = 48176;
        public uint ROUND_NO_BASE_OFFSET = 48132;
        public uint ROUND_TIME_BASE_OFFSET = 48192;
        public uint ROUND_NO_OF_MATCH_TIME_BASE_OFFSET = 23544;
        public uint TURNS_MODE_BASE_OFFSET = 23548;
        public uint TEAM_WIN_BASE_OFFSET = 48180;
        public uint TEAM_WIN_KO_BASE_OFFSET = 48184;
        public uint TEAM_1_WIN_COUNT_BASE_OFFSET = 48136;
        public uint TEAM_2_WIN_COUNT_BASE_OFFSET = 48140;
        public uint DRAW_GAME_COUNT_BASE_OFFSET = 48148;
        public uint PLAYER_1_BASE_OFFSET = 46932;
        public uint EXPLOD_LIST_BASE_OFFSET = 41700;
        public uint PAL_TIME_BASE_OFFSET = 45440;
        public uint PAL_GRAY_BASE_OFFSET = 45444;
        public uint PAL_R_BASE_OFFSET = 45448;
        public uint PAL_G_BASE_OFFSET = 45452;
        public uint PAL_B_BASE_OFFSET = 45456;
        public uint PAUSE_TIME_BASE_OFFSET = 48084;
        public uint SUPER_PAUSE_TIME_BASER_OFFSET = 48120;
        public uint OFFSET_EXPLOD_LIST_OFFSET = 228;
        public uint ANIM_ADDR_EXPLOD_OFFSET = 128;
        public uint ANIM_INDEX_EXPLOD_OFFSET = 12;
        public uint EXPLOD_ID_EXPLOD_OFFSET = 16;
        public uint EXPLOD_OWNER_ID_EXPLOD_OFFSET = 12;
        public uint PROJ_LIST_PROJ_BASE_OFFSET = 20;
        public uint PROJ_MAX_PROJ_BASE_OFFSET = 40;
        public uint OFFSET_PROJ_LIST_OFFSET = 732;
        public uint EXIST_PROJ_OFFSET = 4;
        public uint PROJ_X_PROJ_OFFSET = 92;
        public uint PROJ_Y_PROJ_OFFSET = 96;
        public uint OFFSET_ANIM_LIST_OFFSET = 16;
        public uint ANIM_NO_ANIM_OFFSET = 12;
        public uint ANIM_INFO_ANIM_OFFSET = 16;
        public uint CLSN1_ADDR_ANIM_INFO_OFFSET = 40;
        public uint CLSN2_ADDR_ANIM_INFO_OFFSET = 44;
        public uint OFFSET_HITOVERRIDE_LIST_OFFSET = 20;
        public uint ATTR_HITOVERRIDE_OFFSET = 4;
        public uint NUMTARGET_TARGET_ENTRY_OFFSET = 8;
        public uint TARGET_LIST_TARGET_ENTRY_OFFSET = 20;
        public uint OFFSET_TARGET_LIST_OFFSET = 32;
        public uint EXIST_PLAYER_OFFSET = 344;
        public uint PLAYER_ID_PLAYER_OFFSET = 4;
        public uint HELPER_ID_PLAYER_OFFSET = 9752;
        public uint PARENT_ID_PLAYER_OFFSET = 9756;
        public uint PROJ_BASE_PLAYER_OFFSET = 540;
        public uint STATE_OWNER_PLAYER_OFFSET = 3056;
        public uint STATE_NO_PLAYER_OFFSET = 3060;
        public uint PREV_STATE_NO_PLAYER_OFFSET = 3064;
        public uint PALNO_PLAYER_OFFSET = 5060;
        public uint ALIVE_PLAYER_OFFSET = 3620;
        public uint LIFE_PLAYER_OFFSET = 352;
        public uint POWER_PLAYER_OFFSET = 376;
        public uint DAMAGE_PLAYER_OFFSET = 4136;
        public uint CTRL_PLAYER_OFFSET = 3596;
        public uint STATE_TYPE_PLAYER_OFFSET = 3584;
        public uint MOVE_TYPE_PLAYER_OFFSET = 3588;
        public uint HIT_PAUSE_TIME_PLAYER_OFFSET = 3608;
        public uint MOVE_CONTACT_PLAYER_OFFSET = 3632;
        public uint MOVE_HIT_PLAYER_OFFSET = 3636;
        public uint ANIM_ADDR_PLAYER_OFFSET = 5052;
        public uint MUTEKI_PLAYER_OFFSET = 4088;
        public uint HITBY_1_PLAYER_OFFSET = 4092;
        public uint HITBY_2_PLAYER_OFFSET = 4096;
        public uint HITOVERRIDE_LIST_PLAYER_OFFSET = 4264;
        public uint TARGET_ENTRY_PLAYER_OFFSET = 544;
        public uint FALL_DAMAGE_PLAYER_OFFSET = 4212;
        public uint FACING_PLAYER_OFFSET = 400;
        public uint POS_X_PLAYER_OFFSET = 412;
        public uint POS_Y_PLAYER_OFFSET = 416;
        public uint VEL_X_PLAYER_OFFSET = 436;
        public uint VEL_Y_PLAYER_OFFSET = 440;
        public uint SYS_VAR_PLAYER_OFFSET = 4048;
        public uint SYS_FVAR_PLAYER_OFFSET = 4068;
        public uint VAR_PLAYER_OFFSET = 3648;
        public uint FVAR_PLAYER_OFFSET = 3888;
        public uint ACTIVE_PLAYER_OFFSET = 47720;
        public uint ASSERT_1_PLAYER_OFFSET = 47992;
        public uint PAUSE_MOVE_TIME_PLAYER_OFFSET = 476;
        public uint SUPER_PAUSE_MOVE_TIME_PLAYER_OFFSET = 480;
        public uint ATTACK_MUL_SET_PLAYER_OFFSET = 392;
        public uint DISPLAY_NAME_PLAYER_INFO_OFFSET = 48;
        public uint ANIM_LIST_REF1_PLAYER_INFO_OFFSET = 972;
        public uint ANIM_LIST_REF3_PLAYER_INFO_OFFSET = 24;
        private static MugenAddrDatabase _default_db;
        private static Mugen10DB _mugen10_db;
        private static Mugen11A4DB _mugen11a4_db;
        private static Mugen11B1DB _mugen11b1_db;
        public uint EXIST_EXPLOD_OFFSET;
        public uint PROJ_ID_PROJ_OFFSET;
        public uint EXIST_HITOVERRIDE_OFFSET;
        public uint ANIM_LIST_REF2_PLAYER_INFO_OFFSET;
        public uint SELF_ASSERT_PLAYER_OFFSET;
        public uint LOCALCOORD_X_PLAYER_INFO_OFFSET;
        public uint LOCALCOORD_Y_PLAYER_INFO_OFFSET;

        public static MugenAddrDatabase GetAddrDatabase(
          MugenWindow.MugenType_t mugen_type)
        {
            switch (mugen_type)
            {
                case MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10:
                    if (MugenAddrDatabase._mugen10_db == null)
                        MugenAddrDatabase._mugen10_db = new Mugen10DB();
                    return (MugenAddrDatabase)MugenAddrDatabase._mugen10_db;
                case MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4:
                    if (MugenAddrDatabase._mugen11a4_db == null)
                        MugenAddrDatabase._mugen11a4_db = new Mugen11A4DB();
                    return (MugenAddrDatabase)MugenAddrDatabase._mugen11a4_db;
                case MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11B1:
                    if (MugenAddrDatabase._mugen11b1_db == null)
                        MugenAddrDatabase._mugen11b1_db = new Mugen11B1DB();
                    return (MugenAddrDatabase)MugenAddrDatabase._mugen11b1_db;
                default:
                    if (MugenAddrDatabase._default_db == null)
                        MugenAddrDatabase._default_db = new MugenAddrDatabase();
                    return MugenAddrDatabase._default_db;
            }
        }
    }
}
