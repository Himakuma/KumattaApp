using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class KumattaAppBuild : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    [PostProcessBuild(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS)
        {
            IOSPostProcessBuild(path);
        }
    }

    private static void IOSPostProcessBuild(string path)
    {
        var projectFilePath = PBXProject.GetPBXProjectPath(path);
        var project = new PBXProject();
        project.ReadFromFile(projectFilePath);

        var targetGuid = project.GetUnityFrameworkTargetGuid();
        project.AddFrameworkToProject(targetGuid, "libz.tbd", false);

        project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
        project.WriteToFile(projectFilePath);
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.iOS)
        {
            IOSOnPostprocessBuild(report);
        }
    }

    public void IOSOnPostprocessBuild(BuildReport report)
    {
        string plistPath = Path.Combine(report.summary.outputPath, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        plist.root.SetString("NSLocalNetworkUsageDescription", "テストの為");
        plist.WriteToFile(plistPath);
    }
}
