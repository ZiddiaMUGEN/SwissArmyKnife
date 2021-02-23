// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.Program
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.Threading;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Mutex mutex = new Mutex(true, "SwissArmyKnifeForMugen");
      if (mutex != null && mutex.WaitOne(100))
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run((Form) MainForm.MainObj());
        mutex.ReleaseMutex();
      }
      else
      {
        int num = (int) MessageBox.Show("Running two instances is not supported.", "Swiss Army Knife");
      }
    }
  }
}
