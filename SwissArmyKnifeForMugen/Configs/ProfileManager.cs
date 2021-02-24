// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ProfileManager
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Triggers;
using System.IO;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Configs
{
    /// <summary>
    /// Manager for all profiles (e.g. configuration profile, MUGEN launch profile).
    /// </summary>
    internal class ProfileManager
    {
        private string config_folder_base = ".";
        private MainConfig config = new MainConfig();
        private MugenProfile[] profiles = new MugenProfile[8];
        private TriggerCheckProfile _triggerProfile = new TriggerCheckProfile();
        private const string config_file_name = "Config.cfg";
        private const string profile_folder = "Profiles";
        private const string profile_file_name_base = "profile_";
        private const int max_profile_count = 8;
        private static ProfileManager selfObj;
        private int current_profile_no;

        private ProfileManager()
        {
            if (!Directory.Exists(MainForm.GetFullPath("Profiles")))
            {
                try
                {
                    Directory.CreateDirectory(MainForm.GetFullPath("Profiles"));
                }
                catch
                {
                    int num = (int)MessageBox.Show("Error: Could not create a new profile folder.", "Swiss Army Knife");
                }
            }
            config_folder_base = MainForm.ExeFolder();
            config.InitProfile(config_folder_base, "Config.cfg");
            _triggerProfile.InitProfile("Profiles", "Config.cfg.trigger");
            for (int num = 1; num <= 8; ++num)
            {
                profiles[num - 1] = new MugenProfile();
                profiles[num - 1].InitProfile("Profiles", "profile_" + num.ToString() + ".cfg");
                profiles[num - 1].SetProfileNo(num);
            }
        }

        public static ProfileManager MainObj()
        {
            if (selfObj == null)
                selfObj = new ProfileManager();
            return selfObj;
        }

        public MainConfig GetMainConfig() => config;

        public TriggerCheckProfile GetTriggerCheckProfile() => _triggerProfile;

        public MugenProfile GetCurrentProfile() => current_profile_no >= 1 && current_profile_no <= 8 ? GetProfile(current_profile_no) : null;

        public int SetCurrentProfile(int new_profile_no)
        {
            int currentProfileNo = current_profile_no;
            current_profile_no = new_profile_no;
            return currentProfileNo;
        }

        public bool IsValidProfileNo(int profile_no) => profile_no >= 1 && profile_no <= 8;

        public MugenProfile GetProfile(int profile_no) => profile_no < 1 || profile_no > 8 ? GetCurrentProfile() : profiles[profile_no - 1];

        public int GetProfileCount() => 8;
    }
}
