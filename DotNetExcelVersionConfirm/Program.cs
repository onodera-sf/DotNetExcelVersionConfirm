var version = GetInstallExcelVersion();

Console.WriteLine($"インストールされている Excel の内部バージョンは {version} です。");


/// <summary>
/// インストールされている Excel の内部バージョンを取得します。
/// </summary>
/// <returns>インストールされている Excel の内部バージョン。正常に取得できなかった場合は 0 を返します。</returns>
static decimal GetInstallExcelVersion()
{
  // Excel.Application の Type を取得
  var type = Type.GetTypeFromProgID("Excel.Application");
  if (type == null)
  {
    return 0;
  }

  object? application = null;
  try
  {
    // Excel.Application のインスタンスを生成します
    application = Activator.CreateInstance(type);
    if (application == null)
    {
      // 未インストールの場合
      return 0;
    }

    // バージョンを取得
    var ver = application.GetType().InvokeMember("Version", System.Reflection.BindingFlags.GetProperty, null, application, null);
    if (ver == null)
    {
      return 0;
    }

    // 文字列を数値に変換します
    decimal version;
    if (!decimal.TryParse(ver.ToString(), out version))
    {
      return 0;
    }
    return version;
  }
  finally
  {
    // 生成した Excel のリソースを解放します
    if (application != null)
    {
      System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
    }
  }
}