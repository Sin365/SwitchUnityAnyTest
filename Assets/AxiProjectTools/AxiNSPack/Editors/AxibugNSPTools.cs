#if UNITY_EDITOR_WIN
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace AxibugEmuOnline.Editors
{
	public class AxibugNSPTools : Editor
    {
        static string switch_keys = Path.GetFullPath(Path.Combine(Application.dataPath, "AxiProjectTools/AxiNSPack/switch_keys"));
        static string hacpack_root = Path.GetFullPath(Path.Combine(Application.dataPath, "AxiProjectTools/AxiNSPack/hacpack"));
        static Dictionary<string, string> tools = new Dictionary<string, string>();
        static string prodKeysPath;

        [MenuItem("Axibug��ֲ����/Switch/AxibugNSPTools/RepackNSP(�����¹���NPS��")]
        static void RepackNSP()
        {
            if (!CheckEnvironmentVariable())
                return;

            string path = EditorUtility.OpenFilePanel(
                title: "ѡ�� .nsp �ļ�",
                directory: Path.Combine(Application.dataPath, ".."), // Ĭ��·��Ϊ��Ŀ Assets Ŀ¼
                extension: "nsp" // �����ļ�����Ϊ .nsp
            );

            if (string.IsNullOrEmpty(path))
                return;

            RepackNSP(path);
        }

        [MenuItem("Axibug��ֲ����/Switch/AxibugNSPTools/Build With RepackNSP(���NSP�����¹���NPS��")]
        public static void BuildWithRepackNSP()
        {
            if (!CheckEnvironmentVariable())
                return;

            if (!EditorUtility.DisplayDialog("���", $"ȷ�ϴ��NSP?", "����", "ȡ��"))
                return;

            var levels = new List<string>();
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    levels.Add(scene.path);
            }

            var buildOpt = EditorUserBuildSettings.development ? BuildOptions.Development : BuildOptions.None;
            if (EditorUserBuildSettings.buildWithDeepProfilingSupport)
                buildOpt |= BuildOptions.EnableDeepProfilingSupport;
            if (EditorUserBuildSettings.allowDebugging)
                buildOpt |= BuildOptions.AllowDebugging;

            //��ѡ����NSP��
            EditorUserBuildSettings.switchCreateRomFile = true;

#if UNITY_2018_1_OR_NEWER && !UNITY_6000_0_OR_NEWER
            string titleid = PlayerSettings.Switch.applicationID;
#else
            string titleid = "null";
#endif
            string targetName = $"{Application.productName}_{titleid}.nsp";

			string _locationPathName = $"Output/NSPBuild/{targetName}";
            var options = new BuildPlayerOptions
            {
                scenes = levels.ToArray(),
                locationPathName = _locationPathName,
                target = BuildTarget.Switch,
                options = buildOpt
            };

            try
            {
                BuildReport report = BuildPipeline.BuildPlayer(options);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AxibugNSPTools] Unity Build NSP ����:{ex.ToString()}");
                return;
            }

            string NSPFullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", _locationPathName));
            RepackNSP(NSPFullPath);
        }

        static bool CheckEnvironmentVariable()
        {
            // ��ȡ������������Ҫ��ӻ���������飩
            string sdkRoot = Environment.GetEnvironmentVariable("NINTENDO_SDK_ROOT");
            if (string.IsNullOrEmpty(sdkRoot))
            {
                Debug.LogError($"[AxibugNSPTools]������ȷ���û�������:NINTENDO_SDK_ROOT,(�������ã���֤���ú󳹵�����Unity Hub��Unity)");
                return false;
            }

            #region ��ȡprod.keys�ļ�·��
            prodKeysPath = Path.Combine(
                switch_keys,
                "prod.keys"
            );

            if (!File.Exists(prodKeysPath))
            {
                Debug.LogError($"[AxibugNSPTools]δ�ҵ� prod.keys! ����׼���ļ���path:{prodKeysPath}");
                return false;
            }
            #endregion

            return true;
        }

        static void RepackNSP(string nspFile)
        {
            #region ��ʼ������·��
            // ��ȡ������������Ҫ��ӻ���������飩
            string sdkRoot = Environment.GetEnvironmentVariable("NINTENDO_SDK_ROOT");
            tools["authoringTool"] = Path.Combine(sdkRoot, "Tools/CommandLineTools/AuthoringTool/AuthoringTool.exe");
            tools["hacPack"] = Path.Combine(hacpack_root, "hacpack");
            #endregion

            #region ����NSP�ļ�·��
            string nspFilePath = nspFile;
            string nspFileName = Path.GetFileName(nspFilePath);
            string nspParentDir = Path.GetDirectoryName(nspFilePath);
            #endregion

            #region ��ȡTitle ID
            string titleID = ExtractTitleID(nspFilePath);
            if (string.IsNullOrEmpty(titleID))
            {
                Debug.LogError($"[AxibugNSPTools]NSP�ļ���һ���֣��������TitleID");
                return;
            }
            Debug.Log($"[AxibugNSPTools]Using Title ID: {titleID}");
            #endregion

            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"������ʱĿ¼", 0);
            #region ������ʱĿ¼
            CleanDirectory(Path.Combine(nspParentDir, "repacker_extract"));
            CleanDirectory(Path.Combine(Path.GetTempPath(), "NCA"));
            CleanDirectory(Path.Combine(nspParentDir, "hacpack_backup"));
            #endregion

            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"���NSP�ļ�", 0.2f);
            #region ���NSP�ļ�
            string extractPath = Path.Combine(nspParentDir, "repacker_extract");
            ExecuteCommand($"{tools["authoringTool"]} extract -o \"{extractPath}\" \"{nspFilePath}\"", nspParentDir);

            string controlPath = null;
            string programPath = null;
            FindNACPAndNPDPaths(extractPath, ref controlPath, ref programPath);
            if (controlPath == null || programPath == null)
            {
                Debug.LogError("[AxibugNSPTools] Critical directory structure not found!");
                return;
            }
            #endregion

            #region �ؽ�NCA/NSP
            string tmpPath = Path.Combine(Path.GetTempPath(), "NCA");
            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"�ؽ� Program NCA", 0.3f);
            string programNCA = BuildProgramNCA(tmpPath, titleID, programPath, nspParentDir);
            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"�ؽ� Control NCA", 0.4f);
            string controlNCA = BuildControlNCA(tmpPath, titleID, controlPath, nspParentDir);
            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"�ؽ� Meta NCA", 0.5f);
            BuildMetaNCA(tmpPath, titleID, programNCA, controlNCA, nspParentDir);
            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"�ؽ�NSP", 0.6f);
            string outputNSP = BuildFinalNSP(nspFilePath, nspParentDir, tmpPath, titleID, nspParentDir);
            EditorUtility.DisplayProgressBar("AxibugNSPTools", $"�ؽ�NSP", 0.9f);
            Debug.Log($"[AxibugNSPTools]Repacking completed: {outputNSP}");

			#endregion

			EditorUtility.DisplayProgressBar("AxibugNSPTools", $"������ʱĿ¼", 1);
			#region ������ʱĿ¼
			CleanDirectory(Path.Combine(nspParentDir, "repacker_extract"));
			CleanDirectory(Path.Combine(Path.GetTempPath(), "NCA"));
			CleanDirectory(Path.Combine(nspParentDir, "hacpack_backup"));
			#endregion
			System.Diagnostics.Process.Start("explorer", "/select,\"" + outputNSP.Trim() + "\"");
			EditorUtility.ClearProgressBar();
			

		}



        #region ��������
        static string GetUserInput()
        {
            Console.Write("Enter the NSP filepath: ");
            return Console.ReadLine();
        }
        static string ExtractTitleID(string path)
        {
            var match = Regex.Match(path, @"0100[\dA-Fa-f]{12}");
            return match.Success ? match.Value : null;
        }

        static void CleanDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                while (Directory.Exists(path)) ; // �ȴ�ɾ�����
            }
        }

        static void FindNACPAndNPDPaths(string basePath, ref string controlPath, ref string programPath)
        {
            foreach (var dir in Directory.GetDirectories(basePath))
            {
                if (File.Exists(Path.Combine(dir, "fs0/control.nacp")))
                    controlPath = dir;
                if (File.Exists(Path.Combine(dir, "fs0/main.npdm")))
                    programPath = dir;
            }
        }

        static string ExecuteCommand(string command, string workdir)
        {
            Debug.Log($"����cmd=>{command}");
            var process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,  // ���Ӵ������ض���
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,  // ��ȷָ������
                    StandardErrorEncoding = Encoding.UTF8,
                    WorkingDirectory = workdir
                }
            };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            // ʹ���¼�������򲶻�ʵʱ���
            process.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    outputBuilder.AppendLine(args.Data);
                    Debug.Log($"[AxibugNSPTools]{args.Data}");
                }
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    errorBuilder.AppendLine(args.Data);
                    if (args.Data.Contains("[WARN]"))
						Debug.LogWarning($"[AxibugNSPTools]{args.Data}");
					else
                        Debug.LogError($"[AxibugNSPTools]{args.Data}");
                }
            };

            process.Start();

            // ��ʼ�첽��ȡ���
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // �ȴ������˳�����ʱ���ѹرգ�
            process.WaitForExit();

            // ��������Ϣ���ӵ������
            if (errorBuilder.Length > 0)
            {
                outputBuilder.AppendLine("\nError Output:");
                outputBuilder.Append(errorBuilder);
            }

            return outputBuilder.ToString();
        }
        #endregion

        #region NCA�����߼�
        static string BuildProgramNCA(string tmpPath, string titleID, string programDir, string workdir)
        {
            string args = $"-k \"{prodKeysPath}\" -o \"{tmpPath}\" --titleid {titleID} " +
                          $"--type nca --ncatype program --exefsdir \"{programDir}/fs0\" " +
                          $"--romfsdir \"{programDir}/fs1\" --logodir \"{programDir}/fs2\"";

            string output = ExecuteCommand($"{tools["hacPack"]} {args}", workdir);
            return ParseNCAOutput(output, "Program");
        }

        static string BuildControlNCA(string tmpPath, string titleID, string controlDir, string workdir)
        {
            string args = $"-k \"{prodKeysPath}\" -o \"{tmpPath}\" --titleid {titleID} " +
                          $"--type nca --ncatype control --romfsdir \"{controlDir}/fs0\"";

            string output = ExecuteCommand($"{tools["hacPack"]} {args}", workdir);

            return ParseNCAOutput(output, "Control");
        }

        static void BuildMetaNCA(string tmpPath, string titleID, string programNCA, string controlNCA, string workdir)
        {
            string args = $"-k \"{prodKeysPath}\" -o \"{tmpPath}\" --titleid {titleID} " +
                          $"--type nca --ncatype meta --titletype application " +
                          $"--programnca \"{programNCA}\" --controlnca \"{controlNCA}\"";

            ExecuteCommand($"{tools["hacPack"]} {args}", workdir);
        }

        static string BuildFinalNSP(string origPath, string parentDir, string tmpPath, string titleID, string workdir)
        {
            string outputPath = origPath.Replace(".nsp", "_repacked.nsp");
            if (File.Exists(outputPath)) File.Delete(outputPath);

            string args = $"-k \"{prodKeysPath}\" -o \"{parentDir}\" --titleid {titleID} " +
                          $"--type nsp --ncadir \"{tmpPath}\"";

            ExecuteCommand($"{tools["hacPack"]} {args}", workdir);
            File.Move(Path.Combine(parentDir, $"{titleID}.nsp"), outputPath);
            return outputPath;
        }

        static string ParseNCAOutput(string output, string type)
        {
            var line = output.Split('\n')
                .FirstOrDefault(l => l.Contains($"Created {type} NCA:"));
            //return line?.Split(':').Last().Trim();
            return line?.Substring(line.IndexOf("NCA:") + "NCA:".Length).Trim();

        }
        #endregion
    }
}
#endif