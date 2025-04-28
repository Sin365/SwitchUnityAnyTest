//using nn.account;
using nn.account;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static AxiHttp;

public class MainTest : MonoBehaviour
{
	public Button btn1; public Text text1;
	public Button btn2; public Text text2;
	public Button btn3; public Text text3;
	public Button btn4; public Text text4;

	public Button btn5; public Text text5;
	public Button btn6; public Text text6;
	public Button btn7; public Text text7;
	public Button btn8; public Text text8;


	public Button btn9; public Text text9;
	public Button btn10; public Text text10;
	public Button btn11; public Text text11;
	public Button btn12; public Text text12;

	public Button btn13; public Text text13;
	public Button btn14; public Text text14;
	public Button btn15; public Text text15;
	public Button btn16; public Text text16;
	public Button btn17; public Text text17;
	public Button btn18; public Text text18;
	public Button btn19; public Text text19;
	public Button btn20; public Text text20;
	public Button btn21; public Text text21;

	public Button btnHttpTest;
	public Button tcpTest;
	public Button btnNetLibTest;
	public Button btnNewDownloadTest;
	public Button btnNewDownloadTestAsync;
	public Button btnDNS;
	public Button btnKeyboard;

	//public string dataPath => Application.dataPath;
	//public string streamingAssetsPath => Application.streamingAssetsPath;
	public string temporaryCachePath => Application.temporaryCachePath;
	//public string persistentDataPath => Application.persistentDataPath;
	public string dataAxibugPath => "sd:/Axibug";
	public string dataAxibugPath_save => "save:/Axibug";
	public string dataAxibugPath_sdmc => "sd:/Axibug";

	public string persistentDataPath = "";
	public string testfile => "/axibugtest.txt";
	public string testdir => "/axibugtestdir";

	void Start()
	{
		//string a = Application.persistentDataPath;
		//Debug.Log($"persistentDataPath -> {a}");

		text1.text = $"scan->persistentDataPath";
		btn1.onClick.AddListener(() =>
		{
			string a = Application.persistentDataPath;
			Debug.Log($"persistentDataPath -> {a}");
		});

		text2.text = $"scan->Application.dataPath";
		btn2.onClick.AddListener(() =>
		{
			string a = Application.dataPath;
			Debug.Log($"Application.dataPath -> {a}");
		});

		text3.text = $"scan->Application.streamingAssetsPath";
		btn3.onClick.AddListener(() =>
		{
			string a = Application.streamingAssetsPath;
			Debug.Log($"Application.streamingAssetsPath -> {a}");
		});


		text4.text = $"scan->Application.temporaryCachePath";
		btn4.onClick.AddListener(() =>
		{
			string a = Application.temporaryCachePath;
			Debug.Log($"Application.temporaryCachePath -> {a}");
		});


		//btn1.onClick.AddListener(() => TraverseDirectory(dataPath)); text1.text = $"scan->{dataPath}";
		//btn2.onClick.AddListener(() => TraverseDirectory(streamingAssetsPath)); text2.text = $"scan->{streamingAssetsPath}";
		//btn3.onClick.AddListener(() => TraverseDirectory(temporaryCachePath)); text3.text = $"scan->{temporaryCachePath}";
		//btn4.onClick.AddListener(() => NP_SaveToFile_save()); text4.text = $"try->NP_SaveToFile_save";

		btn5.onClick.AddListener(() => AxiNS.instance.Init()); text5.text = $"AxiNS->Init";
		btn6.onClick.AddListener(() => AxiNS.instance.mount.MountSDForDebug()); text6.text = $"AxiNS->MountForDebug";
		btn7.onClick.AddListener(() => AxiNS.instance.mount.MountSD()); text7.text = $"AxiNS->MountSD";
		//btn7.onClick.AddListener(() => AxiNS.instance.user.GetUserID(out var _)); text7.text = $"AxiNS.instance.user.GetUserID";
		//btn5.onClick.AddListener(()
		//=> CreateFile(dataPath)); text5.text = $"createfile->{dataPath}";
		//btn6.onClick.AddListener(() => CreateFile(streamingAssetsPath)); text6.text = $"createfile->{streamingAssetsPath}";
		//btn7.onClick.AddListener(() => CreateFile(temporaryCachePath)); text7.text = $"createfile->{temporaryCachePath}";
		//btn8.onClick.AddListener(() => CreateFile(dataAxibugPath)); text8.text = $"createfile->{dataAxibugPath}";

		//btn9.onClick.AddListener(() => NS_CreateFile(dataAxibugPath)); text9.text = $"NS_CreateFile->{dataAxibugPath}";
		//btn10.onClick.AddListener(() => NS_CreateDir(dataAxibugPath)); text10.text = $"NS_CreateDir->{dataAxibugPath}";

		btn8.onClick.AddListener(() => NS_CreateDir(dataAxibugPath_save)); text8.text = $"NS_CreateDir->{dataAxibugPath_save}";
		btn9.onClick.AddListener(() => NS_CreateFile(dataAxibugPath_save)); text9.text = $"NS_CreateFile->{dataAxibugPath_save}";

		btn10.onClick.AddListener(() => NS_CreateDir(dataAxibugPath_sdmc)); text10.text = $"NS_CreateDir->{dataAxibugPath_sdmc}";
		btn11.onClick.AddListener(() => NS_CreateFile(dataAxibugPath_sdmc)); text11.text = $"NS_CreateFile->{dataAxibugPath_sdmc}";

		btn12.onClick.AddListener(() => AxiNS.instance.io.FileToSaveWithCreate("save:/axibug.txt", new byte[] { 1, 2, 3, 4 })); text12.text = $"AxiNS.instance.io.SaveFile({"save:/axibug.txt"}";
		btn13.onClick.AddListener(() => AxiNS.instance.io.FileToSaveWithCreate("save:/axibug/axibug.txt", new byte[] { 1, 2, 3, 4 })); text13.text = $"AxiNS.instance.io.SaveFile({"save:/axibug/axibug.txt"}";
		btn14.onClick.AddListener(() => AxiNS.instance.io.LoadSwitchDataFile("save:/axibug/axibug.txt", out var _)); text14.text = $"AxiNS.instance.io.LoadSwitchDataFile({"save:/axibug/axibug.txt"}";
		btn15.onClick.AddListener(() => AxiNS.instance.io.FileToSaveWithCreate("sd:/axibug.txt", new byte[] { 1, 2, 3, 4 })); text15.text = $"AxiNS.instance.io.SaveFile({"sd:/axibug.txt"}";
		btn16.onClick.AddListener(() => AxiNS.instance.io.FileToSaveWithCreate("save:/axibug/bigfile.txt", new byte[1024 * 1024 * 32])); text16.text = $"AxiNS.instance.io.SaveFile({"save:/axibug/bigfile.txt"}";
		btn17.onClick.AddListener(() =>
		{
			bool result = AxiNS.instance.io.GetDirectoryEntrys("save:/",nn.fs.OpenDirectoryMode.All, out var elist);
			if (!result)
				UnityEngine.Debug.Log($"result =>{result}");
			else
			{
				UnityEngine.Debug.Log($"====EntrysList====");
				foreach (var e in elist)
					UnityEngine.Debug.Log(e);
			}
		}
		); text17.text = $"AxiNS.instance.io.GetDirectoryEntrys({"save:/"},nn.fs.OpenDirectoryMode.All,out var elist)";

		btn18.onClick.AddListener(() =>
		{
			bool result = AxiNS.instance.io.GetDirectoryEntrysFullRecursion("save:/",out var elist);
			if (!result)
				UnityEngine.Debug.Log($"result =>{result}");
			else
			{
				UnityEngine.Debug.Log($"==== FullRecursion Entrys List====");
				foreach (var e in elist)
					UnityEngine.Debug.Log(e);
			}
		}
		); text18.text = $"AxiNS.instance.io.GetDirectoryEntrysFullRecursion({"save:/"},out var elist)";

		btn19.onClick.AddListener(() =>
		{
			bool result = AxiNS.instance.io.GetDirectoryEntrys("save:/axibug/", nn.fs.OpenDirectoryMode.All, out var elist);
			if (!result)
				UnityEngine.Debug.Log($"result =>{result}");
			else
			{
				UnityEngine.Debug.Log($"====EntrysList====");
				foreach (var e in elist)
					UnityEngine.Debug.Log(e);
			}
		}
		); text19.text = $"AxiNS.instance.io.GetDirectoryEntrys({"save:/axibug/"},nn.fs.OpenDirectoryMode.All,out var elist)";

		btn20.onClick.AddListener(() =>
		{
			bool result = AxiNS.instance.io.GetDirectoryDirs("save:/",  out var elist);
			if (!result)
				UnityEngine.Debug.Log($"result =>{result}");
			else
			{
				UnityEngine.Debug.Log($"====EntrysList====");
				foreach (var e in elist)
					UnityEngine.Debug.Log(e);
			}
		}
		); text20.text = $"AxiNS.instance.io.GetDirectoryDirs({"save:/"},out var elist)";

		btn21.onClick.AddListener(() =>
		{
			bool result = AxiNS.instance.io.GetDirectoryFiles("save:/", out var elist);
			if (!result)
				UnityEngine.Debug.Log($"result =>{result}");
			else
			{
				UnityEngine.Debug.Log($"====EntrysList====");
				foreach (var e in elist)
					UnityEngine.Debug.Log(e);
			}
		}
		); text21.text = $"AxiNS.instance.io.GetDirectoryFiles({"save:/"},out var elist)";


		//btn11.onClick.AddListener(() => CreateDir(temporaryCachePath)); text11.text = $"CreateDir->{temporaryCachePath}";
		//btn12.onClick.AddListener(() => CreateDir(dataAxibugPath)); text12.text = $"CreateDir->{dataAxibugPath}";
		btnKeyboard?.onClick.AddListener(TestKeyBoard);

		btnHttpTest.onClick.AddListener(() => { StartCoroutine(HttpTest()); });
		tcpTest.onClick.AddListener(() => TCPTest());
		btnNetLibTest?.onClick.AddListener(NetLibTest);
		btnNewDownloadTest.onClick.AddListener(() => NewHttpDownLoadTest());
		btnNewDownloadTestAsync.onClick.AddListener(() => { StartCoroutine(NewHttpDownLoadTestAsync()); });
		btnDNS.onClick.AddListener(DNSTest);
	}


	private nn.fs.FileHandle fileHandle = new nn.fs.FileHandle();
	void NS_CreateFile(string path)
	{
		string topath = path + testfile;

		nn.Result result = nn.fs.File.Create(topath, 1);
		//result.abortUnlessSuccess();
		if (!result.IsSuccess())
		{
			UnityEngine.Debug.LogError($"失败: =>nn.fs.File.Create({topath}, 1) ,result=>{result.GetErrorInfo()}");
			return;
		}
		UnityEngine.Debug.Log($"成功: =>nn.fs.File.Create({topath}, 1)");
	}
	void NS_CreateDir(string path)
	{
		var result = nn.fs.Directory.Create(path);
		//result.abortUnlessSuccess();
		if (!result.IsSuccess())
		{
			UnityEngine.Debug.LogError($"失败: =>nn.fs.Directory({path}), result=>{result.GetErrorInfo()}");
			return;
		}
		UnityEngine.Debug.Log($"成功: =>nn.fs.Directory({path})");
	}

	private static Uid userId; // user ID for the user account on the Nintendo Switch


	//private static Uid GetCurrentUserId()
	//{
	//	Uid pOut = default;
	//	UserHandle handle = new UserHandle();
	//	nn.Result result = nn.account.Account.GetUserId(ref pOut, handle);
	//	if (!result.IsSuccess())
	//	{
	//		throw new Exception("GetCurrentUserId -> result.IsSuccess() == false");
	//	}
	//	return pOut;
	//}
	// Update is called once per frame
	void Update()
	{

	}

	void TraverseDirectory(string folderPath)
	{
		Debug.Log($"Scan ->" + folderPath);
		// 检查路径是否存在
		if (!System.IO.Directory.Exists(folderPath))
		{
			Debug.Log($"{folderPath}");
			return;
		}

		// 遍历文件夹
		DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
		FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();

		foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
		{
			if (fileSystemInfo is DirectoryInfo)
			{
				// 递归遍历子文件夹
				DirectoryInfo subDirectoryInfo = (DirectoryInfo)fileSystemInfo;
				TraverseDirectory(subDirectoryInfo.FullName);
			}
			else
			{
				// 处理文件
				FileInfo fileInfo = (FileInfo)fileSystemInfo;
				Debug.Log(fileInfo.FullName);
			}
		}
	}

	void CreateDir(string folderPath)
	{
		string topath = folderPath + testdir;
		Debug.Log($"CreateDir->{topath}");
		Debug.Log($"Directory.Exists->{System.IO.Directory.Exists(topath)}");
		try
		{
			System.IO.Directory.CreateDirectory(topath);
			Debug.Log($"CreateDir OK!->{topath}");
		}
		catch (Exception ex)
		{
			Debug.Log($"CreateDir Cant!->{topath} {ex.ToString()}");
		}
	}

	void CreateFile(string folderPath)
	{
		string topath = folderPath + testfile;
		Debug.Log($"CreateFile->{topath}");
		Debug.Log($"File.Exists->{System.IO.File.Exists(topath)}");
		try
		{
			System.IO.File.WriteAllText(topath, "axibugtest12345");
			Debug.Log($"WriteAllText OK!->{topath}");
		}
		catch (Exception ex)
		{
			Debug.Log($"WriteAllText Cant!->{topath} {ex.ToString()}");
		}
	}

	void ReadFile(string folderPath)
	{
		string topath = folderPath + testfile;
		Debug.Log($"ReadFile->{topath}");
		Debug.Log($"File.Exists->{System.IO.File.Exists(topath)}");
		try
		{
			Debug.Log($"ReadFile OK!->{System.IO.File.ReadAllText(topath)}");
		}
		catch (Exception ex)
		{
			Debug.Log($"ReadFile Cant!->{topath} {ex.ToString()}");
		}
	}


	IEnumerator HttpTest()
	{
		UnityWebRequestAsyncOperation reqaasync = null;
		UnityWebRequest req = null;
		try
		{
			req = UnityWebRequest.Get("http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0");
			reqaasync = req.SendWebRequest();
		}
		catch (Exception ex)
		{
			Debug.Log($"req err->{ex.ToString()}");
		}
		yield return reqaasync;
		Debug.Log($"req.responseCode->{req.responseCode}");
		Debug.Log($"req.downloadHandler.text->{req.downloadHandler.text}");
	}

	async void HttpCSharpTest()
	{
		//var client = new HttpClient();
		//{
		//	try
		//	{
		//		// 指定要请求的URL  
		//		string url = "http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0";

		//		Debug.Log($"HttpCSharpTest->{url}");
		//		// 发送GET请求  
		//		HttpResponseMessage response = await client.GetAsync(url);

		//		// 确保HTTP成功状态值  
		//		response.EnsureSuccessStatusCode();

		//		// 读取响应内容  
		//		string responseBody = await response.Content.ReadAsStringAsync();
		//		Debug.Log($"responseBody->{responseBody}");
		//	}
		//	catch (HttpRequestException e)
		//	{
		//		Debug.Log($"\nException Caught!");
		//		Debug.Log($"Message :{e.Message.ToString()} ");
		//	}
		//}
	}
	async void HttpCSharpTest2()
	{
		//WebRequest web = WebRequest.Create(@"http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0");//声明WebRequest对象
		//MessageBox.Show("ContentLength请求数据的内容长度：" + web.ContentLength);
		//MessageBox.Show("ContentType内容类型：" + web.ContentType);
		//MessageBox.Show("Credentials网络凭证：" + web.Credentials);
		//MessageBox.Show("Method协议方法：" + web.Method);
		//MessageBox.Show("RequestUri：" + web.RequestUri);
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://139.186.160.243/api/CheckStandInfo?platform=1&version=1.0.0.0"));
		try
		{
			WebResponse response = await request.GetResponseAsync();
			using (var stream = response.GetResponseStream())
			using (var reader = new System.IO.StreamReader(stream))
			{
				string content = await reader.ReadToEndAsync();
				Debug.Log($"返回Body->{content}");
			}
		}
		catch (WebException ex)
		{
			// 处理网络异常  
			Debug.Log($"An error occurred: {ex.Message}");
			if (ex.Response != null)
			{
				// 读取错误响应（如404、500等HTTP状态码）  
				using (var errorStream = ex.Response.GetResponseStream())
				using (var reader = new System.IO.StreamReader(errorStream))
				{
					string errorContent = await reader.ReadToEndAsync();
					Debug.Log($"Error content: {errorContent}");
				}
			}
		}
	}


	async void DNSTest()
	{
		Debug.Log($"DNSTest");
		Debug.Log($"strIPRet->{AxiHttp.GetDnsIP("emu.axibug.com").ToString()}");
	}


	void NewHttpTest()
	{
		AxiRespInfo respInfo = AxiHttp.AxiRequest("http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0");
		ShowAxiRespInfo(respInfo);
	}

	IEnumerator NewHttpTestAsync()
	{
		WaitAxiRequest waitAsync = AxiRequestAsync("http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0");
		yield return waitAsync;
		AxiRespInfo respInfo = waitAsync.mReqAsync;
		ShowAxiRespInfo(respInfo);
	}
	void NewHttpDownLoadTest()
	{
		AxiRespInfo respInfo = AxiHttp.AxiDownload("http://emu.axibug.com/images/fcrom/Downtown%20-%20Nekketsu%20Monogatari%20(J).JPG");
		ShowAxiRespInfo(respInfo);
	}

	IEnumerator NewHttpDownLoadTestAsync()
	{
		Debug.Log($"==== NewHttpDownLoadTestAsync 1 ====");
		AxiRespInfo respInfo = AxiHttp.AxiDownloadAsync("http://emu.axibug.com/FileZilla_Server-cn-0_9_60_2.exe");
		Debug.Log($"==== NewHttpDownLoadTestAsync 2 ====");
		while (!respInfo.isDone)
		{
			//yield return new WaitForSeconds(0.3f);
			yield return null;
			Debug.Log($"下载进度：{respInfo.DownLoadPr} ->{respInfo.loadedLenght}/{respInfo.NeedloadedLenght}");
		}
		ShowAxiRespInfo(respInfo);
	}

	void ShowAxiRespInfo(AxiRespInfo respInfo)
	{
		Debug.Log($"");
		Debug.Log($"==== request ====");
		Debug.Log($"url =>{respInfo.url}");
		Debug.Log($"Raw =>{respInfo.requestRaw}");
		Debug.Log($"code =>{respInfo.code}");
		Debug.Log($"respInfo.bTimeOut =>{respInfo.bTimeOut}");
		Debug.Log($"");
		Debug.Log($"==== response ====");
		Debug.Log($"==== header ====");
		Debug.Log($"header =>{respInfo.header}");
		Debug.Log($"HeadersCount =>{respInfo.headers.Count}");
		foreach (var kv in respInfo.headers)
			Debug.Log($"{kv.Key} => {kv.Value}");
		Debug.Log($"");
		Debug.Log($"==== body ====");
		Debug.Log($"body_text =>{respInfo.body}");
		Debug.Log($"body_text.Length =>{respInfo.body.Length}");
		Debug.Log($"bodyRaw.Length =>{respInfo.bodyRaw?.Length}");
		Debug.Log($"");
		Debug.Log($"==== download ====");
		Debug.Log($"downloadMode =>{respInfo.downloadMode}");
		Debug.Log($"respInfo.fileName =>{respInfo.fileName}");
		Debug.Log($"respInfo.NeedloadedLenght =>{respInfo.NeedloadedLenght}");
		Debug.Log($"respInfo.loadedLenght =>{respInfo.loadedLenght}");
		if (respInfo.downloadMode == AxiDownLoadMode.DownLoadBytes)
		{
			if (respInfo.bTimeOut)
			{
				Debug.Log($"DownLoad Timeout!");
				return;
			}
			string downloadSavePath;
			if (Application.platform == RuntimePlatform.PSP2)
			{
				downloadSavePath = dataAxibugPath + "/" + respInfo.fileName;
			}
			else
			{
				downloadSavePath = persistentDataPath + "/" + respInfo.fileName;
			}
			try
			{
				System.IO.File.WriteAllBytes(downloadSavePath, respInfo.bodyRaw);
				Debug.Log($"DownLoad OK");
			}
			catch (Exception ex)
			{
				Debug.Log($"DownLoad Err {ex.ToString()}");
			}
		}
	}


	void NetLibTest()
	{
		Init("main.axibug.com", 1000);
	}


	public bool Init(string IP, int port, bool isHadDetailedLog = true, bool bBindReuseAddress = false, int bBindport = 0)
	{
		Debug.Log("==>1");

		//bDetailedLog = isHadDetailedLog;
		//RevIndex = MaxRevIndexNum;
		//SendIndex = MaxSendIndexNum;



		//Socket client;
		//client = new Socket(SocketType.Stream, ProtocolType.Tcp);

		Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		Debug.Log("==>2");
		if (bBindReuseAddress)
		{
			Debug.Log("==>2_1");
			client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			Debug.Log("==>2_2");
			IPEndPoint ipe = new IPEndPoint(IPAddress.Any, Convert.ToInt32(bBindport));
			Debug.Log("==>2_3");
			client.Bind(ipe);
			Debug.Log("==>2_4");
		}
		Debug.Log("==>3");
		//LastConnectIP = IP;
		//LastConnectPort = port;
		return Connect(client, IP, port);
	}
	bool Connect(Socket client, string IP, int port)
	{
		Debug.Log("Connect==>1");
		//带回调的
		try
		{
			Debug.Log("连接到远程IP " + IP + ":" + port);

			client.Connect(IP, port);
			Debug.Log("Connect==>2 OK");
			//Thread thread = new Thread(Recive);
			//thread.IsBackground = true;
			//thread.Start(client);
			int localport = ((IPEndPoint)client.LocalEndPoint).Port;

			Debug.Log("Connect==>3");
			//Debug.Log("Connect==>3 OK");
			//if (bDetailedLog)
			//	LogOut($"连接成功!连接到远程IP->{IP}:{port} | 本地端口->{localport}");
			//else
			//	LogOut("连接成功!");

			//if (_heartTimer == null)
			//{
			//	_heartTimer = new System.Timers.Timer();
			//}
			//_heartTimer.Interval = TimerInterval;
			//_heartTimer.Elapsed += CheckUpdatetimer_Elapsed;
			//_heartTimer.AutoReset = true;
			//_heartTimer.Enabled = true;

			//if (bDetailedLog)
			//	LogOut("开启心跳包检测");

			//OnConnected?.Invoke(true);

			client.Close();
			Debug.Log("Connect==>close");
			return true;
		}
		catch (Exception ex)
		{
			//if (bDetailedLog)
			//	LogOut("连接失败：" + ex.ToString());
			//else
			//	LogOut("连接失败");

			//OnConnected?.Invoke(false);
			return false;
		}
	}


	/*
	 * string url = "http://emu.axibug.com/api/CheckStandInfo?platform=1&version=1.0.0.0";

		string strURI = url;
		string strHost = "";
		string strIP = "";
		string strPort = "";
		string strRelativePath = "";
		bool bSSL = false;
		bool foward_302 = true;

		if (!OnlySocketHttp.OnlySocketHttp.ParseURI(strURI, ref bSSL, ref strHost, ref strIP, ref strPort, ref strRelativePath))
		{
			UnityEngine.Debug.Log("ParseURI False");
			return string.Empty;
		}

		int port;
		int.TryParse(strPort, out port);

		var ip = Dns.GetHostEntry(strHost).AddressList[0];
		var ipEndPoint = new IPEndPoint(ip, port);

		using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
		{
			socket.Connect(ipEndPoint);

			string request = $"GET {strRelativePath} HTTP/1.1\r\nHost: {strHost}\r\nConnection: Close\r\n\r\n";
			byte[] buffer = Encoding.ASCII.GetBytes(request);
			socket.Send(buffer);

			// 读取响应  
			using (var stream = new NetworkStream(socket))
			using (var reader = new StreamReader(stream))
			{
				string response = reader.ReadToEnd();
				UnityEngine.Debug.Log($"response->{response}");

				// 这里可以添加更复杂的逻辑来解析HTTP响应并下载文件  
				// 示例中仅打印了响应  
			}
		}
	 */

	void TCPTest()
	{
		string strHost = "139.186.160.243";
		string strPort = "10492";
		Socket sClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
		Debug.Log($"EndPoint server->{strHost}:strPort");
		EndPoint server = new IPEndPoint(IPAddress.Parse(strHost), System.Convert.ToInt32(strPort));
		Debug.Log($"BeginConnect");
		sClient.BeginConnect(server, new AsyncCallback(ConnectCallback), sClient);
	}


	private void ConnectCallback(IAsyncResult ar)
	{
		Debug.Log($"ConnectCallback");
		try
		{
			Socket client = (Socket)ar.AsyncState;
			client.EndConnect(ar);
		}
		catch (Exception e)
		{
			//WSLog.LogError(e.Message);
		}
		finally
		{
			//connectDone.Set();
		}
	}

	void TestKeyBoard()
	{
		Debug.Log($" isSupported -> {TouchScreenKeyboard.isSupported}");
		TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
	}
}
