// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.Mugen11A4DB
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

namespace SwissArmyKnifeForMugen
{
    public class Mugen11A4DB : MugenAddrDatabase
    {
        public Mugen11A4DB()
        {
            this.USE_NEW_DEBUG_COLOR_ADDR = true;
            this.NEW_DEBUG_COLOR_OFFSETS = new uint[] { 0x419A95, 0x4198F8, 0x419962, 0x419B90 };
            this.NEW_DEBUG_COLOR_SN_OFFSETS = new uint[] { 0x419C78, 0x419C84, 0x419C89 };

            this.MUGEN_POINTER_BASE_OFFSET = 5259496U;
            this.CMD_CURRENT_INDEX_ADDR = 5257328U;
            this.CMD_NEXT_INDEX_ADDR = 5257332U;
            this.CMD_KEY_ADDR = 5256304U;
            this.DEMO_BASE_OFFSET = 48008U;
            this.MUGEN_ACTIVE_BASE_OFFSET = 48004U;
            this.SPEED_MODE_BASE_OFFSET = 78060U;
            this.SKIP_MODE_BASE_OFFSET = 73372U;
            this.DEBUG_MODE_BASE_OFFSET = 78028U;
            this.DEBUG_TARGET_BASE_OFFSET = 78020U;
            this.PAUSE_ADDR = 73392U;
            this.SCREEN_X_BASE_OFFSET = 75944U;
            this.SCREEN_Y_BASE_OFFSET = 75948U;
            this.PAL_TIME_BASE_OFFSET = 45880U;
            this.PAL_GRAY_BASE_OFFSET = 45884U;
            this.PAL_R_BASE_OFFSET = 45888U;
            this.PAL_G_BASE_OFFSET = 45892U;
            this.PAL_B_BASE_OFFSET = 45896U;
            this.GAMETIME_BASE_OFFSET = 73368U;
            this.ROUND_STATE_BASE_OFFSET = 75604U;
            this.ROUND_NO_BASE_OFFSET = 75560U;
            this.ROUND_TIME_BASE_OFFSET = 75620U;
            this.ROUND_NO_OF_MATCH_TIME_BASE_OFFSET = 2584U;
            this.TURNS_MODE_BASE_OFFSET = 2588U;
            this.TEAM_WIN_BASE_OFFSET = 75608U;
            this.TEAM_WIN_KO_BASE_OFFSET = 75612U;
            this.TEAM_1_WIN_COUNT_BASE_OFFSET = 75564U;
            this.TEAM_2_WIN_COUNT_BASE_OFFSET = 75568U;
            this.DRAW_GAME_COUNT_BASE_OFFSET = 75576U;
            this.PAUSE_TIME_BASE_OFFSET = 75512U;
            this.SUPER_PAUSE_TIME_BASER_OFFSET = 75548U;
            this.ACTIVE_PLAYER_OFFSET = 75148U;
            this.ASSERT_1_PLAYER_OFFSET = 75420U;
            this.PLAYER_1_BASE_OFFSET = 74360U;
            this.EXPLOD_LIST_BASE_OFFSET = 68640U;
            this.HITOVERRIDE_LIST_PLAYER_OFFSET = 4480U;
            this.OFFSET_HITOVERRIDE_LIST_OFFSET = 20U;
            this.EXIST_HITOVERRIDE_OFFSET = 0U;
            this.ATTR_HITOVERRIDE_OFFSET = 4U;
            this.TARGET_ENTRY_PLAYER_OFFSET = 712U;
            this.NUMTARGET_TARGET_ENTRY_OFFSET = 8U;
            this.TARGET_LIST_TARGET_ENTRY_OFFSET = 24U;
            this.OFFSET_TARGET_LIST_OFFSET = 28U;
            this.EXIST_PLAYER_OFFSET = 432U;
            this.PLAYER_ID_PLAYER_OFFSET = 4U;
            this.HELPER_ID_PLAYER_OFFSET = 5700U;
            this.PARENT_ID_PLAYER_OFFSET = 5704U;
            this.PROJ_BASE_PLAYER_OFFSET = 708U;
            this.STATE_OWNER_PLAYER_OFFSET = 3272U;
            this.STATE_NO_PLAYER_OFFSET = 3276U;
            this.PREV_STATE_NO_PLAYER_OFFSET = 3280U;
            this.PALNO_PLAYER_OFFSET = 5436U;
            this.ALIVE_PLAYER_OFFSET = 3840U;
            this.LIFE_PLAYER_OFFSET = 440U;
            this.POWER_PLAYER_OFFSET = 464U;
            this.DAMAGE_PLAYER_OFFSET = 4356U;
            this.CTRL_PLAYER_OFFSET = 3812U;
            this.STATE_TYPE_PLAYER_OFFSET = 3800U;
            this.MOVE_TYPE_PLAYER_OFFSET = 3804U;
            this.HIT_PAUSE_TIME_PLAYER_OFFSET = 3828U;
            this.MOVE_CONTACT_PLAYER_OFFSET = 3852U;
            this.MOVE_HIT_PLAYER_OFFSET = 3856U;
            this.MUTEKI_PLAYER_OFFSET = 4308U;
            this.HITBY_1_PLAYER_OFFSET = 4312U;
            this.HITBY_2_PLAYER_OFFSET = 4316U;
            this.FALL_DAMAGE_PLAYER_OFFSET = 4428U;
            this.FACING_PLAYER_OFFSET = 488U;
            this.POS_X_PLAYER_OFFSET = 504U;
            this.POS_Y_PLAYER_OFFSET = 512U;
            this.VEL_X_PLAYER_OFFSET = 584U;
            this.VEL_Y_PLAYER_OFFSET = 592U;
            this.SYS_VAR_PLAYER_OFFSET = 4268U;
            this.SYS_FVAR_PLAYER_OFFSET = 4288U;
            this.VAR_PLAYER_OFFSET = 3868U;
            this.FVAR_PLAYER_OFFSET = 4108U;
            this.ATTACK_MUL_SET_PLAYER_OFFSET = 480U;
            this.DISPLAY_NAME_PLAYER_INFO_OFFSET = 48U;
            this.ANIM_LIST_REF1_PLAYER_INFO_OFFSET = 1076U;
            this.ANIM_LIST_REF2_PLAYER_INFO_OFFSET = 0U;
            this.ANIM_LIST_REF3_PLAYER_INFO_OFFSET = 28U;
            this.OFFSET_EXPLOD_LIST_OFFSET = 616U;
            this.ANIM_ADDR_EXPLOD_OFFSET = 480U;
            this.ANIM_INDEX_EXPLOD_OFFSET = 12U;
            this.EXIST_EXPLOD_OFFSET = 0U;
            this.EXPLOD_ID_EXPLOD_OFFSET = 16U;
            this.EXPLOD_OWNER_ID_EXPLOD_OFFSET = 12U;
            this.OFFSET_PROJ_LIST_OFFSET = 1288U;
            this.PROJ_LIST_PROJ_BASE_OFFSET = 24U;
            this.PROJ_MAX_PROJ_BASE_OFFSET = 12U;
            this.EXIST_PROJ_OFFSET = 4U;
            this.PROJ_ID_PROJ_OFFSET = 0U;
            this.PROJ_X_PROJ_OFFSET = 164U;
            this.PROJ_Y_PROJ_OFFSET = 168U;
            this.OFFSET_ANIM_LIST_OFFSET = 16U;
            this.ANIM_NO_ANIM_OFFSET = 12U;
            this.ANIM_ADDR_PLAYER_OFFSET = 5428U;
            this.ANIM_INFO_ANIM_OFFSET = 16U;
            this.CLSN1_ADDR_ANIM_INFO_OFFSET = 132U;
            this.CLSN2_ADDR_ANIM_INFO_OFFSET = 136U;
            this.PAUSE_MOVE_TIME_PLAYER_OFFSET = 552U;
            this.SUPER_PAUSE_MOVE_TIME_PLAYER_OFFSET = 556U;
            this.SELF_ASSERT_PLAYER_OFFSET = 684U;
            this.LOCALCOORD_X_PLAYER_INFO_OFFSET = 144U;
            this.LOCALCOORD_Y_PLAYER_INFO_OFFSET = 152U;
        }
    }
}
