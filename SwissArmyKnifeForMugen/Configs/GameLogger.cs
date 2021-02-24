// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.GameLogger
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Displays;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Configs
{
    /// <summary>
    /// Logs info about matches that run in SAK.
    /// </summary>
    internal class GameLogger
    {
        private Hashtable charDataBase = new Hashtable();
        private static GameLogger selfObj;

        private GameLogger()
        {
        }

        public static GameLogger MainObj()
        {
            if (GameLogger.selfObj == null)
                GameLogger.selfObj = new GameLogger();
            return GameLogger.selfObj;
        }

        /// <summary>
        /// Resets all internally-stored character logging data.
        /// </summary>
        public void ResetCharData() => this.charDataBase.Clear();

        /// <summary>
        /// Updates internally-stored character logging data.
        /// </summary>
        /// <param name="charName">Character name</param>
        /// <param name="palno">Palette</param>
        /// <param name="data">Additional data to add</param>
        public void UpdateCharData(string charName, int palno, GameLogger.CharData_t data)
        {
            if (charName == null)
                return;
            string str = charName + " (" + palno.ToString() + "p)";
            if (!this.charDataBase.ContainsKey((object)str))
                this.charDataBase.Add((object)str, (object)data);
            else
                ((GameLogger.CharData_t)this.charDataBase[(object)str]).Add(data);
        }

        public void UpdateCharDataEx(string charName, GameLogger.CharData_t data)
        {
            if (charName == null)
                return;
            if (!this.charDataBase.ContainsKey((object)charName))
                this.charDataBase.Add((object)charName, (object)data);
            else
                ((GameLogger.CharData_t)this.charDataBase[(object)charName]).Add(data);
        }

        /// <summary>
        /// Logs info about a given character.
        /// </summary>
        /// <param name="name">Character name</param>
        /// <param name="charData">Data to be logged</param>
        /// <param name="displayFlag">Whether to display or just log to file</param>
        private void _DisplayCharData(string name, GameLogger.CharData_t charData, bool displayFlag)
        {
            LogManager logManager = LogManager.MainObj();
            if (logManager == null)
                return;
            if (displayFlag)
            {
                logManager.appendLog("");
                logManager.appendLog("◆" + name + "'s battle record ---");
                logManager.appendLog("Total matches = " + (object)charData.GetTotalGames());
                logManager.appendLog("Wins=" + charData.winGames.ToString() + ",");
                logManager.append("Losses=" + charData.loseGames.ToString() + ",");
                logManager.append("Draws=" + charData.drawGames.ToString() + ",");
                logManager.append("Errors=" + charData.errorGames.ToString() + ",");
                logManager.append("Cancelled matches=" + charData.canceledGames.ToString());
                logManager.appendLog("Total number of rounds = " + (object)charData.GetTotalRounds());
                logManager.appendLog("Wins=" + charData.winRounds.ToString());
                logManager.append("(KO=" + charData.winKORounds.ToString() + "),");
                logManager.append("Losses=" + charData.loseRounds.ToString());
                logManager.append("(KO=" + charData.loseKORounds.ToString() + "),");
                logManager.append("Draws=" + charData.drawRounds.ToString() + ",");
                logManager.append("Errors=" + charData.errorRounds.ToString() + ",");
                logManager.append("Cancelled matches=" + charData.canceledRounds.ToString());
            }
            else
            {
                logManager._appendLog("");
                logManager._appendLog("◆" + name + "'s battle record ---");
                logManager._appendLog("Total matches = " + (object)charData.GetTotalGames());
                logManager._appendLog("Wins=" + charData.winGames.ToString() + ",");
                logManager._append("Losses=" + charData.loseGames.ToString() + ",");
                logManager._append("Draws=" + charData.drawGames.ToString() + ",");
                logManager._append("Errorsー=" + charData.errorGames.ToString() + ",");
                logManager._append("Cancelled matches=" + charData.canceledGames.ToString());
                logManager._appendLog("Total number of rounds = " + (object)charData.GetTotalRounds());
                logManager._appendLog("Wins=" + charData.winRounds.ToString());
                logManager._append("(KO=" + charData.winKORounds.ToString() + "),");
                logManager._append("Losses=" + charData.loseRounds.ToString());
                logManager._append("(KO=" + charData.loseKORounds.ToString() + "),");
                logManager._append("Draws=" + charData.drawRounds.ToString() + ",");
                logManager._append("Errors=" + charData.errorRounds.ToString() + ",");
                logManager._append("Cancelled matches=" + charData.canceledRounds.ToString());
            }
        }

        public void DumpCharData(string charName, int palno)
        {
            if (charName == null)
                return;
            string name = charName + " (" + palno.ToString() + "p)";
            if (!this.charDataBase.ContainsKey((object)name))
                return;
            GameLogger.CharData_t charData = (GameLogger.CharData_t)this.charDataBase[(object)name];
            this._DisplayCharData(name, charData, true);
        }

        public void DumpCharDataEx(string charName)
        {
            if (charName == null || !this.charDataBase.ContainsKey((object)charName))
                return;
            GameLogger.CharData_t charData = (GameLogger.CharData_t)this.charDataBase[(object)charName];
            this._DisplayCharData(charName, charData, true);
        }

        public void DisplayAll(bool displayFlag)
        {
            LogManager logManager = LogManager.MainObj();
            if (logManager == null)
                return;
            if (displayFlag)
            {
                logManager.appendLog("");
                logManager.appendLog("--- Battle record log ---");
            }
            else
            {
                logManager._appendLog("");
                logManager._appendLog("--- Battle record log ---");
            }
            List<string> stringList = new List<string>();
            foreach (DictionaryEntry dictionaryEntry in this.charDataBase)
                stringList.Add((string)dictionaryEntry.Key);
            stringList.Sort();
            foreach (string name in stringList)
                this._DisplayCharData(name, (GameLogger.CharData_t)this.charDataBase[(object)name], displayFlag);
        }

        public void SaveAll(string fileName)
        {
            switch (fileName)
            {
                case "":
                    break;
                default:
                    LogManager logManager = LogManager.MainObj();
                    if (logManager == null)
                        break;
                    string contents = "--- Battle record log ---" + Environment.NewLine + Environment.NewLine;
                    List<string> stringList = new List<string>();
                    foreach (DictionaryEntry dictionaryEntry in this.charDataBase)
                        stringList.Add((string)dictionaryEntry.Key);
                    stringList.Sort();
                    foreach (string str in stringList)
                    {
                        GameLogger.CharData_t charDataT = (GameLogger.CharData_t)this.charDataBase[(object)str];
                        contents = contents + "◆" + str + "'s battle record ---";
                        contents += Environment.NewLine;
                        contents = contents + "Total matches = " + (object)charDataT.GetTotalGames();
                        contents += Environment.NewLine;
                        contents = contents + "Wins=" + charDataT.winGames.ToString() + ",";
                        contents = contents + "Losses=" + charDataT.loseGames.ToString() + ",";
                        contents = contents + "Draws=" + charDataT.drawGames.ToString() + ",";
                        contents = contents + "Error=" + charDataT.errorGames.ToString() + ",";
                        contents = contents + "Cancelled matches=" + charDataT.canceledGames.ToString();
                        contents += Environment.NewLine;
                        contents = contents + "Total number of rounds = " + (object)charDataT.GetTotalRounds();
                        contents += Environment.NewLine;
                        contents = contents + "Wins=" + charDataT.winRounds.ToString();
                        contents = contents + "(KO=" + charDataT.winKORounds.ToString() + "),";
                        contents = contents + "Losses=" + charDataT.loseRounds.ToString();
                        contents = contents + "(KO=" + charDataT.loseKORounds.ToString() + "),";
                        contents = contents + "Draws=" + charDataT.drawRounds.ToString() + ",";
                        contents = contents + "Errors=" + charDataT.errorRounds.ToString() + ",";
                        contents = contents + "Cancelled matches=" + charDataT.canceledRounds.ToString();
                        contents += Environment.NewLine;
                        contents += Environment.NewLine;
                    }
                    Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
                    try
                    {
                        File.AppendAllText(fileName, contents, encoding);
                        logManager.appendLog("The battle history log has been saved to the " + fileName + " file");
                        break;
                    }
                    catch
                    {
                        int num = (int)MessageBox.Show("Could not save the battle history to the " + fileName + " log file", "Swiss Army Knife");
                        break;
                    }
            }
        }

        public void CopyAll(string fileName)
        {
            switch (fileName)
            {
                case "":
                    break;
                default:
                    LogManager logManager = LogManager.MainObj();
                    if (logManager == null)
                        break;
                    string contents = "Name, Total maches, Wins, Losses, Draws, Errors, Cancelled maches, Total rounds, Wins, KO wins, Losses, KO losses, Draws, Errors, Cancelled rounds" + Environment.NewLine;
                    List<string> stringList = new List<string>();
                    foreach (DictionaryEntry dictionaryEntry in this.charDataBase)
                        stringList.Add((string)dictionaryEntry.Key);
                    stringList.Sort();
                    foreach (string str in stringList)
                    {
                        GameLogger.CharData_t charDataT = (GameLogger.CharData_t)this.charDataBase[(object)str];
                        contents = contents + str + ", ";
                        contents = contents + (object)charDataT.GetTotalGames() + ", ";
                        contents = contents + (object)charDataT.winGames + ", ";
                        contents = contents + (object)charDataT.loseGames + ", ";
                        contents = contents + (object)charDataT.drawGames + ", ";
                        contents = contents + (object)charDataT.errorGames + ", ";
                        contents = contents + (object)charDataT.canceledGames + ", ";
                        contents = contents + (object)charDataT.GetTotalRounds() + ", ";
                        contents = contents + (object)charDataT.winRounds + ", ";
                        contents = contents + (object)charDataT.winKORounds + ", ";
                        contents = contents + (object)charDataT.loseRounds + ", ";
                        contents = contents + (object)charDataT.loseKORounds + ", ";
                        contents = contents + (object)charDataT.drawRounds + ", ";
                        contents = contents + (object)charDataT.errorRounds + ", ";
                        contents += (string)(object)charDataT.canceledRounds;
                        contents += Environment.NewLine;
                    }
                    Encoding encoding = Encoding.GetEncoding("SHIFT_JIS");
                    try
                    {
                        File.AppendAllText(fileName, contents, encoding);
                        logManager.appendLog("The battle history log has been saved to the " + fileName + " file");
                        break;
                    }
                    catch
                    {
                        int num = (int)MessageBox.Show("Could not save the battle history to the " + fileName + " log file", "Swiss Army Knife");
                        break;
                    }
            }
        }

        public int GetPlayersCount() => this.charDataBase != null ? this.charDataBase.Count : 0;

        /// <summary>
        /// Structure storing match data for a character.
        /// </summary>
        public class CharData_t
        {
            public int winRounds;
            public int winKORounds;
            public int loseRounds;
            public int loseKORounds;
            public int drawRounds;
            public int canceledRounds;
            public int errorRounds;
            public int winGames;
            public int loseGames;
            public int drawGames;
            public int canceledGames;
            public int errorGames;

            public void Add(GameLogger.CharData_t data)
            {
                this.winRounds += data.winRounds;
                this.winKORounds += data.winKORounds;
                this.loseRounds += data.loseRounds;
                this.loseKORounds += data.loseKORounds;
                this.drawRounds += data.drawRounds;
                this.canceledRounds += data.canceledRounds;
                this.errorRounds += data.errorRounds;
                this.winGames += data.winGames;
                this.loseGames += data.loseGames;
                this.drawGames += data.drawGames;
                this.canceledGames += data.canceledGames;
                this.errorGames += data.errorGames;
            }

            public int GetTotalRounds() => this.winRounds + this.loseRounds + this.drawRounds + this.canceledRounds + this.errorRounds;

            public int GetTotalGames() => this.winGames + this.loseGames + this.drawGames + this.canceledGames + this.errorGames;
        }
    }
}
