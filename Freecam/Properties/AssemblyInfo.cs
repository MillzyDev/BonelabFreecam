using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Freecam;
using MelonLoader;
using BuildInfo = Freecam.BuildInfo;

[assembly: AssemblyTitle(BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyProduct(BuildInfo.Name)]
[assembly: AssemblyCopyright("Copyright (c) 2023 Frederick (Millzy) Mills")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion(BuildInfo.Version + ".*")]
[assembly: AssemblyFileVersion(BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]

[assembly: MelonInfo(typeof(Mod), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author)]

[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID(BuildInfo.Id)]

[assembly: VerifyLoaderVersion("0.5.7")]
[assembly: HarmonyDontPatchAll]