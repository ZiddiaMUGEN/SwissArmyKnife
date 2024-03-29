﻿// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ProfileBase
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen.Configs
{
    /// <summary>
    /// Base for a Profile (which stores settings)
    /// </summary>
    internal class ProfileBase
    {
        protected string profile_folder;
        protected string cfg_file_name;

        public void InitProfile(string folder, string filename)
        {
            profile_folder = folder;
            cfg_file_name = filename;
            LoadCfgFile(Path.Combine(folder, cfg_file_name));
            InitSubProfiles();
        }

        protected virtual void InitSubProfiles()
        {
        }

        public void ReLoad() => LoadCfgFile(Path.Combine(profile_folder, cfg_file_name));

        public string GetProfileFolder() => profile_folder;

        public string GetProfileFile() => cfg_file_name;

        public string GetProfilePath() => Path.Combine(profile_folder, cfg_file_name);

        public bool DoesBackupExist() => File.Exists(Path.Combine(profile_folder, cfg_file_name + ".bak"));

        public bool CopyConfig(ProfileBase source_profile)
        {
            if (source_profile == null)
                return false;
            string profilePath = source_profile.GetProfilePath();
            if (profilePath == null)
                return false;
            string destFileName = Path.Combine(profile_folder, cfg_file_name);
            try
            {
                File.Copy(profilePath, destFileName, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool MakeBackup()
        {
            string sourceFileName = Path.Combine(profile_folder, cfg_file_name);
            string destFileName = Path.Combine(profile_folder, cfg_file_name + ".bak");
            try
            {
                File.Copy(sourceFileName, destFileName, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool RestoreBackup()
        {
            string str = Path.Combine(profile_folder, cfg_file_name + ".bak");
            string destFileName = Path.Combine(profile_folder, cfg_file_name);
            if (!File.Exists(str))
                return false;
            try
            {
                File.Copy(str, destFileName, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool SaveConfigText(string configText)
        {
            string profilePath = GetProfilePath();
            Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
            try
            {
                File.WriteAllText(profilePath, configText, encoding);
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected string GetValue(string key, string line)
        {
            string str = line.Substring(key.Length).Trim();
            if (str[0] == '=')
            {
                str = str.Substring(1).Trim();
                if (str.Length > 2 && str[0] == '"' && str[str.Length - 1] == '"')
                    str = str.Substring(1, str.Length - 2);
            }
            return str;
        }

        protected virtual bool LoadCfgFile(string the_file) => false;
    }
}
