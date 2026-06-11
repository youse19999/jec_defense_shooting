using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityGoogleDrive;
using UnityEngine.WSA;
using UnityEngine.Networking;
using System.IO.Compression;



#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[System.Serializable]
public class ItemData
{
    [JsonProperty("Timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("Column 1")]
    public DateTime Column1 { get; set; }

    [JsonProperty("Column 2")]
    public DateTime Column2 { get; set; }

    [JsonProperty("名前")]
    public string Name { get; set; }

    [JsonProperty("リソース")]
    public string ResourceUrl { get; set; }
}
public class InputDialogWindow : EditorWindow
{
    private string inputText = "";
    private System.Action<string> onComplete;

    public static void ShowDialog(string title, System.Action<string> onClickOK)
    {
        InputDialogWindow window = CreateInstance<InputDialogWindow>();
        window.titleContent = new GUIContent(title);
        window.onComplete = onClickOK;
        window.ShowUtility();
    }

    void OnGUI()
    {
        GUILayout.Label("APIキーの入力をしてください。", EditorStyles.boldLabel);
        inputText = GUILayout.TextField(inputText);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            onComplete?.Invoke(inputText);
            Close();
        }
        if (GUILayout.Button("キャンセル"))
        {
            Close();
        }
        GUILayout.EndHorizontal();
    }
}
public class ResourcesUpdater:MonoBehaviour
{
    async Awaitable Start()
    {
        string jsonString = @"[
               {""Timestamp"":""2026-06-04T02:28:28.000Z"",""Column 1"":""2026-06-03T15:00:00.000Z"",""Column 2"":""1899-12-30T03:05:40.000Z"",""名前"":""斉藤 陽生"",""リソース"":""https://drive.google.com/open?id=1J2vakWFSZwsmPo7VFmSHlD-C2-UFz2se""},
               {""Timestamp"":""2026-06-04T03:12:40.000Z"",""Column 1"":""2026-06-03T15:00:00.000Z"",""Column 2"":""1899-12-29T15:49:40.000Z"",""名前"":"""",""リソース"":""https://drive.google.com/open?id=1Jd7-oueIJxULoQb3G_0f2YDG4LvxTxVi""},
               {""Timestamp"":""2026-06-11T00:40:56.000Z"",""Column 1"":""2026-06-10T15:00:00.000Z"",""Column 2"":""1899-12-30T01:17:40.000Z"",""名前"":""斉藤 陽生"",""リソース"":""https://drive.google.com/open?id=14wiCwPpKmNMedBS4WdOvv7dlu5xEtxAG""}
        ]";

        // デシリアライズを実行
        List<ItemData> items = JsonConvert.DeserializeObject<List<ItemData>>(jsonString);
        List<ItemData> descendingOrder = items.OrderByDescending(x => x.Timestamp).ToList();
        // データの確認用ログList<ItemData> ascendingOrder = items.OrderBy(x => x.Timestamp).ToList();
        if (!File.Exists("version.tmp") || descendingOrder.First().Timestamp.ToString() != File.ReadAllText("version.tmp").ToString())
        {
            InputDialogWindow.ShowDialog("APIキーの入力", async (result) =>
            {

                var id = descendingOrder.First().ResourceUrl.Split("https://drive.google.com/open?id=")[1];
                Debug.Log(id);
                string url = $"https://www.googleapis.com/drive/v3/files/{id}?alt=media&key={result}";

                using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
                {
                    // リクエスト送信
                    await webRequest.SendWebRequest();

                    if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                        webRequest.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError($"エラーが発生しました: {webRequest.error}");
                        Debug.LogError($"レスポンスコード: {webRequest.responseCode}");
                    }
                    else
                    {
                        Debug.Log("ダウンロード成功！");

                        byte[] rawData = webRequest.downloadHandler.data;

                        File.WriteAllText("version.tmp", descendingOrder.First().Timestamp.ToString());

                        File.WriteAllBytes("lastest.zip",rawData);

                        string temp_folder = Path.Combine(UnityEngine.Application.dataPath, "temp");
                        string temp_folder_resource = Path.Combine(UnityEngine.Application.dataPath, "temp/Resources");
                        Debug.Log(temp_folder);
                        string bak_folder = Path.Combine(UnityEngine.Application.dataPath, "bak");

                        if(Directory.Exists(temp_folder))
                        {
                            Directory.CreateDirectory(bak_folder);
                        }

                        ZipFile.ExtractToDirectory("lastest.zip", temp_folder);
                        Debug.Log(Directory.GetFiles(temp_folder).Length);
                        foreach(string path in Directory.GetFiles(temp_folder_resource))
                        {
                            if(File.Exists(path))
                            {
                                File.Copy(path,Path.Combine(bak_folder,Path.GetFileName(path)),true);
                                Debug.Log(path + "に送りました。");
                                File.Copy(path, Path.Combine(UnityEngine.Application.dataPath, Path.GetFileName(path)),true);
                            }
                            else
                            {
                                File.Copy(path, Path.Combine(UnityEngine.Application.dataPath, Path.GetFileName(path)));
                            }
                        }
                        Directory.Delete(temp_folder);
                        Debug.Log("");
                    }
                }
            });
        }
    }
} 
#endif