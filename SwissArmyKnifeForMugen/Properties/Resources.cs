// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.Properties.Resources
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SwissArmyKnifeForMugen.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (SwissArmyKnifeForMugen.Properties.Resources.resourceMan == null)
          SwissArmyKnifeForMugen.Properties.Resources.resourceMan = new ResourceManager("SwissArmyKnifeForMugen.Properties.Resources", typeof (SwissArmyKnifeForMugen.Properties.Resources).Assembly);
        return SwissArmyKnifeForMugen.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => SwissArmyKnifeForMugen.Properties.Resources.resourceCulture;
      set => SwissArmyKnifeForMugen.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap arror1 => (Bitmap) SwissArmyKnifeForMugen.Properties.Resources.ResourceManager.GetObject(nameof (arror1), SwissArmyKnifeForMugen.Properties.Resources.resourceCulture);

    internal static Bitmap background => (Bitmap) SwissArmyKnifeForMugen.Properties.Resources.ResourceManager.GetObject(nameof (background), SwissArmyKnifeForMugen.Properties.Resources.resourceCulture);

    internal Resources()
    {
    }
  }
}
