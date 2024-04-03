Module Program
	<Runtime.Versioning.SupportedOSPlatform("windows")>
	Sub Main(args As String())
		slmgr("/skms kms1.shanghaitech.edu.cn")
		Dim 版本ID As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("EditionID")
		Select Case 版本ID
			Case "Professional"
				slmgr("/ipk W269N-WFGWX-YVC9B-4J6C9-T83GX")
			Case "ServerDatacenter"
				slmgr("/ipk 2KNJJ-33Y9H-2GXGX-KMQWH-G6H67")
			Case Else
				Console.WriteLine($"不支持的EditionID：{版本ID}")
				Console.ReadKey(True)
		End Select
		slmgr("/ato")
		Console.WriteLine("现在可以关闭此窗口")
		Console.ReadKey(True)
	End Sub

	Sub slmgr(参数 As String)
		Static 启动信息 As New ProcessStartInfo("cscript") With {.CreateNoWindow = True, .UseShellExecute = False, .RedirectStandardOutput = True, .RedirectStandardError = True}
		启动信息.Arguments = $"//Nologo C:\Windows\System32\slmgr.vbs {参数}"
		Dim 进程 As Process = Process.Start(启动信息)
		AddHandler 进程.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs) Console.WriteLine(e.Data)
		AddHandler 进程.ErrorDataReceived, Sub(sender As Object, e As DataReceivedEventArgs) Console.WriteLine(e.Data)
		进程.BeginOutputReadLine()
		进程.BeginErrorReadLine()
		进程.WaitForExit()
	End Sub
End Module
