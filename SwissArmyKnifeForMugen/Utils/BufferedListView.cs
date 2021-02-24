// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.BufferedListView
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Utils
{
  /// <summary>
  /// Seems to be just a double-buffered view for lists such as Player, Explod lists.
  /// </summary>
  internal class BufferedListView : ListView
  {
    protected override bool DoubleBuffered
    {
      get => true;
      set
      {
      }
    }
  }
}
