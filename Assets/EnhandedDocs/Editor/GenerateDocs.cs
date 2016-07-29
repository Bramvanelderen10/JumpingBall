using UnityEngine;
using System.Collections;
using UnityEditor;
using HtmlAgilityPack;
using System.IO;
using System;

public class EnhancedDocs : Editor {
	[MenuItem("Help/Enhanced Unity Manual", false, 0)]
	public static void ShowManual() {
		if (!EnsureGenerated()) return;
		System.Diagnostics.Process.Start(@"EnhancedDocs/en/Manual/index.html".Replace(char.Parse("/"), char.Parse("\\")));
	}

	[MenuItem("Help/Enhanced Scripting Reference", false, 0)]
	public static void ShowScriptReference() {
		if (!EnsureGenerated()) return;
		System.Diagnostics.Process.Start(@"EnhancedDocs/en/ScriptReference/index.html".Replace(char.Parse("/"), char.Parse("\\")));
	}

	[MenuItem("Help/Delete generated EnhancedDocs", false, 0)]
	public static void DeleteDocs() {
		FileUtil.DeleteFileOrDirectory("EnhancedDocs");
		EditorUtility.DisplayDialog("Delete generated EnhancedDocs", "Generated EnhancedDocs has been deleted", "Ok");
	}

	static bool EnsureGenerated() {
		if (File.Exists("EnhancedDocs/EnhancedDocsVersion.txt") && File.ReadAllText("EnhancedDocs/EnhancedDocsVersion.txt") == Application.unityVersion) return true;
		if (EditorUtility.DisplayDialog("Generate EnhancedDocs", "Enhanced documentation needs to be generated before you can view it. This may take up to 15 minutes.", "Ok", "Cancel")) {
			Generate();
			File.WriteAllText("EnhancedDocs/EnhancedDocsVersion.txt", Application.unityVersion);
			return true;
		}
		return false;
	}

	static void Generate() {
		EditorUtility.DisplayProgressBar("Copying original documentation", "", 0.5f);

		var path = EditorApplication.applicationPath.Split(char.Parse("/"));
		path[path.Length - 1] = "Data/Documentation";
		FileUtil.ReplaceDirectory(String.Join("/", path), "EnhancedDocs");


		string root = "EnhancedDocs/en/ScriptReference";
		var files = Directory.GetFiles(root);
		var progress = 0;
		foreach (var fileName in files) {
			EditorUtility.DisplayProgressBar("Generating Enhanced documentation", fileName, (float)progress / files.Length);

			if (!fileName.EndsWith(".html")) continue;

			try {
				var d = new HtmlDocument();
				d.Load(fileName);
				foreach (var e in d.DocumentNode.SelectNodes("//tr/td/a")) {
					var d2 = new HtmlDocument();
					d2.Load(root + "/" + e.GetAttributeValue("href", ""));
					foreach (var e2 in d2.DocumentNode.SelectNodes("//div[@class='signature']")) {
						foreach (var e3 in e2.SelectNodes("div")) {
							if (e3.InnerHtml.StartsWith("public ")) {
								e3.InnerHtml = e3.InnerHtml.Substring("public ".Length);
							}
							if (e3.InnerHtml.StartsWith("static")) {
								e3.InnerHtml = e3.InnerHtml.Substring("static".Length);
							}
						};
						e2.SetAttributeValue("style", "margin:0 0 0 0;");
						var n = HtmlTextNode.CreateNode("<tr><td></td><td>" + e2.OuterHtml + "</td></tr>");
						e.ParentNode.ParentNode.ParentNode.InsertAfter(n, e.ParentNode.ParentNode);
					}
				}
				d.Save(fileName);
			} catch {

			}
			progress++;
		}

		EditorUtility.ClearProgressBar();
	}
}
