// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MainConfig
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.IO;
using System.Text;

namespace SwissArmyKnifeForMugen.Configs
{
    /// <summary>
    /// Configuration for the SAK application.
    /// </summary>
    internal class MainConfig : ProfileBase
    {
        private int main_win_x = 5;
        private int main_win_y = 5;
        private const string text_editor_key = "TextEditor";
        private const string show_debug_win_key = "DisplayDebugWin";
        private const string main_win_x_key = "MainWin-X";
        private const string main_win_y_key = "MainWin-Y";
        private const string main_win_w_key = "MainWin-W";
        private const string main_win_h_key = "MainWin-H";
        private const string log_win_x_key = "LogWin-X";
        private const string log_win_y_key = "LogWin-Y";
        private const string log_win_w_key = "LogWin-W";
        private const string log_win_h_key = "LogWin-H";
        private const string mugen_win_x_key = "MugenWin-X";
        private const string mugen_win_y_key = "MugenWin-Y";
        private const string debug_win_x_key = "DebugWin-X";
        private const string debug_win_y_key = "DebugWin-Y";
        private const string debug_win_w_key = "DebugWin-W";
        private const string debug_win_h_key = "DebugWin-H";
        private const string var_win_x_key = "VarWin-X";
        private const string var_win_y_key = "VarWin-Y";
        private const string var_win_w_key = "VarWin-W";
        private const string var_win_h_key = "VarWin-H";
        private bool show_debug_win;
        private string text_editor;
        private int main_win_w;
        private int main_win_h;
        private int log_win_x;
        private int log_win_y;
        private int log_win_w;
        private int log_win_h;
        private int mugen_win_x;
        private int mugen_win_y;
        private int debug_win_x;
        private int debug_win_y;
        private int debug_win_w;
        private int debug_win_h;
        private int var_win_x;
        private int var_win_y;
        private int var_win_w;
        private int var_win_h;

        /// <summary>
        /// Get the internal text editor.
        /// </summary>
        /// <returns></returns>
        public string GetTextEditor() => this.text_editor;

        /// <summary>
        /// Check if the debug window is displayed.
        /// </summary>
        /// <returns></returns>
        public bool DoesShowDebugWin() => this.show_debug_win;

        /// <summary>
        /// X-coord of the main window.
        /// </summary>
        /// <returns></returns>
        public int GetMainWinX() => this.main_win_x;

        /// <summary>
        /// Y-coord of the main window.
        /// </summary>
        /// <returns></returns>
        public int GetMainWinY() => this.main_win_y;

        /// <summary>
        /// Width of the main window.
        /// </summary>
        /// <returns></returns>
        public int GetMainWinW() => this.main_win_w;

        /// <summary>
        /// Height of the main window.
        /// </summary>
        /// <returns></returns>
        public int GetMainWinH() => this.main_win_h;

        /// <summary>
        /// X-coord of the log window.
        /// </summary>
        /// <returns></returns>
        public int GetLogWinX() => this.log_win_x;

        /// <summary>
        /// Y-coord of the log window.
        /// </summary>
        /// <returns></returns>
        public int GetLogWinY() => this.log_win_y;

        /// <summary>
        /// Width of the log window.
        /// </summary>
        /// <returns></returns>
        public int GetLogWinW() => this.log_win_w;

        /// <summary>
        /// Height of the log window.
        /// </summary>
        /// <returns></returns>
        public int GetLogWinH() => this.log_win_h;

        /// <summary>
        /// X-coord of the Mugen window.
        /// </summary>
        /// <returns></returns>
        public int GetMugenWinX() => this.mugen_win_x;

        /// <summary>
        /// Y-coord of the Mugen window.
        /// </summary>
        /// <returns></returns>
        public int GetMugenWinY() => this.mugen_win_y;

        /// <summary>
        /// X-coord of the Debug window.
        /// </summary>
        /// <returns></returns>
        public int GetDebugWinX() => this.debug_win_x;

        /// <summary>
        /// Y-coord of the Debug window.
        /// </summary>
        /// <returns></returns>
        public int GetDebugWinY() => this.debug_win_y;

        /// <summary>
        /// Width of the Debug window.
        /// </summary>
        /// <returns></returns>
        public int GetDebugWinW() => this.debug_win_w;

        /// <summary>
        /// Height of the Debug window.
        /// </summary>
        /// <returns></returns>
        public int GetDebugWinH() => this.debug_win_h;

        /// <summary>
        /// X-coord of the Variable window.
        /// </summary>
        /// <returns></returns>
        public int GetVarWinX() => this.var_win_x;

        /// <summary>
        /// Y-coord of the Variable window.
        /// </summary>
        /// <returns></returns>
        public int GetVarWinY() => this.var_win_y;

        /// <summary>
        /// Width of the Variable window.
        /// </summary>
        /// <returns></returns>
        public int GetVarWinW() => this.var_win_w;

        /// <summary>
        /// Height of the Variable window.
        /// </summary>
        /// <returns></returns>
        public int GetVarWinH() => this.var_win_h;

        /// <summary>
        /// Set main window position.
        /// </summary>
        /// <param name="main_x"></param>
        /// <param name="main_y"></param>
        /// <param name="main_w"></param>
        /// <param name="main_h"></param>
        public void SetMainWindowPos(int main_x, int main_y, int main_w, int main_h)
        {
            this.main_win_x = main_x;
            this.main_win_y = main_y;
            this.main_win_w = main_w;
            this.main_win_h = main_h;
        }

        /// <summary>
        /// Set log window position.
        /// </summary>
        /// <param name="log_x"></param>
        /// <param name="log_y"></param>
        /// <param name="log_w"></param>
        /// <param name="log_h"></param>
        public void SetLogWindowPos(int log_x, int log_y, int log_w, int log_h)
        {
            this.log_win_x = log_x;
            this.log_win_y = log_y;
            this.log_win_w = log_w;
            this.log_win_h = log_h;
        }

        /// <summary>
        /// Set MUGEN window position.
        /// </summary>
        /// <param name="mugen_x"></param>
        /// <param name="mugen_y"></param>
        public void SetMugenWindowPos(int mugen_x, int mugen_y)
        {
            this.mugen_win_x = mugen_x;
            this.mugen_win_y = mugen_y;
        }

        /// <summary>
        /// Set debug window position.
        /// </summary>
        /// <param name="debug_x"></param>
        /// <param name="debug_y"></param>
        /// <param name="debug_w"></param>
        /// <param name="debug_h"></param>
        public void SetDebugWindowPos(int debug_x, int debug_y, int debug_w, int debug_h)
        {
            this.debug_win_x = debug_x;
            this.debug_win_y = debug_y;
            this.debug_win_w = debug_w;
            this.debug_win_h = debug_h;
        }

        public void SetVarWindowPos(int var_x, int var_y, int var_w, int var_h)
        {
            this.var_win_x = var_x;
            this.var_win_y = var_y;
            this.var_win_w = var_w;
            this.var_win_h = var_h;
        }

        /// <summary>
        /// Loads settings and window positions from the config file.
        /// </summary>
        /// <param name="the_file"></param>
        /// <returns></returns>
        protected override bool LoadCfgFile(string the_file)
        {
            this.show_debug_win = false;
            this.text_editor = (string)null;
            StreamReader streamReader = (StreamReader)null;
            try
            {
                streamReader = new StreamReader(the_file, Encoding.Default);
                while (streamReader.Peek() >= 0)
                {
                    string line = streamReader.ReadLine().Trim();
                    if (line.IndexOf(";", 0) != 0)
                    {
                        int length = line.IndexOf(';');
                        if (length > 0)
                            line = line.Substring(0, length);
                        if (line.IndexOf("TextEditor", 0) == 0)
                            this.text_editor = this.GetValue("TextEditor", line);
                        else if (line.IndexOf("DisplayDebugWin", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("DisplayDebugWin", line), out result) && result != 0)
                                this.show_debug_win = true;
                        }
                        else if (line.IndexOf("MainWin-X", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MainWin-X", line), out result))
                                this.main_win_x = result;
                        }
                        else if (line.IndexOf("MainWin-Y", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MainWin-Y", line), out result))
                                this.main_win_y = result;
                        }
                        else if (line.IndexOf("MainWin-W", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MainWin-W", line), out result))
                                this.main_win_w = result;
                        }
                        else if (line.IndexOf("MainWin-H", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MainWin-H", line), out result))
                                this.main_win_h = result;
                        }
                        else if (line.IndexOf("LogWin-X", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("LogWin-X", line), out result))
                                this.log_win_x = result;
                        }
                        else if (line.IndexOf("LogWin-Y", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("LogWin-Y", line), out result))
                                this.log_win_y = result;
                        }
                        else if (line.IndexOf("LogWin-W", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("LogWin-W", line), out result))
                                this.log_win_w = result;
                        }
                        else if (line.IndexOf("LogWin-H", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("LogWin-H", line), out result))
                                this.log_win_h = result;
                        }
                        else if (line.IndexOf("MugenWin-X", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MugenWin-X", line), out result))
                                this.mugen_win_x = result;
                        }
                        else if (line.IndexOf("MugenWin-Y", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("MugenWin-Y", line), out result))
                                this.mugen_win_y = result;
                        }
                        else if (line.IndexOf("DebugWin-X", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("DebugWin-X", line), out result))
                                this.debug_win_x = result;
                        }
                        else if (line.IndexOf("DebugWin-Y", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("DebugWin-Y", line), out result))
                                this.debug_win_y = result;
                        }
                        else if (line.IndexOf("DebugWin-W", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("DebugWin-W", line), out result))
                                this.debug_win_w = result;
                        }
                        else if (line.IndexOf("DebugWin-H", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("DebugWin-H", line), out result))
                                this.debug_win_h = result;
                        }
                        else if (line.IndexOf("VarWin-X", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("VarWin-X", line), out result))
                                this.var_win_x = result;
                        }
                        else if (line.IndexOf("VarWin-Y", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("VarWin-Y", line), out result))
                                this.var_win_y = result;
                        }
                        else if (line.IndexOf("VarWin-W", 0) == 0)
                        {
                            int result;
                            if (int.TryParse(this.GetValue("VarWin-W", line), out result))
                                this.var_win_w = result;
                        }
                        else
                        {
                            int result;
                            if (line.IndexOf("VarWin-H", 0) == 0 && int.TryParse(this.GetValue("VarWin-H", line), out result))
                                this.var_win_h = result;
                        }
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

        public bool SaveConfigData() => this.SaveConfigText("TextEditor = " + this.text_editor + Environment.NewLine + "DisplayDebugWin = " + (this.show_debug_win ? (object)"1" : (object)"0") + Environment.NewLine + Environment.NewLine + "MainWin-X = " + (object)this.main_win_x + Environment.NewLine + "MainWin-Y = " + (object)this.main_win_y + Environment.NewLine + "MainWin-W = " + (object)this.main_win_w + Environment.NewLine + "MainWin-H = " + (object)this.main_win_h + Environment.NewLine + Environment.NewLine + "LogWin-X = " + (object)this.log_win_x + Environment.NewLine + "LogWin-Y = " + (object)this.log_win_y + Environment.NewLine + "LogWin-W = " + (object)this.log_win_w + Environment.NewLine + "LogWin-H = " + (object)this.log_win_h + Environment.NewLine + Environment.NewLine + "MugenWin-X = " + (object)this.mugen_win_x + Environment.NewLine + "MugenWin-Y = " + (object)this.mugen_win_y + Environment.NewLine + Environment.NewLine + "DebugWin-X = " + (object)this.debug_win_x + Environment.NewLine + "DebugWin-Y = " + (object)this.debug_win_y + Environment.NewLine + "DebugWin-W = " + (object)this.debug_win_w + Environment.NewLine + "DebugWin-H = " + (object)this.debug_win_h + Environment.NewLine + Environment.NewLine + "VarWin-X = " + (object)this.var_win_x + Environment.NewLine + "VarWin-Y = " + (object)this.var_win_y + Environment.NewLine + "VarWin-W = " + (object)this.var_win_w + Environment.NewLine + "Varin-H = " + (object)this.var_win_h + Environment.NewLine + Environment.NewLine);
    }
}
