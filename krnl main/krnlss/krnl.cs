

// krnlss.krnl
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using Controls;
using injection;
using krnlss;
using krnlss.Properties;
using Microsoft.Win32;
using ScintillaNET;
using SirhurtUI.Controls;

public class krnl : Form
{
	public class BrowserMenuRenderer : ToolStripProfessionalRenderer
	{
		public BrowserMenuRenderer()
			: base(new BrowserColors())
		{
		}
	}

	public class BrowserColors : ProfessionalColorTable
	{
		public override Color ToolStripDropDownBackground => Color.FromArgb(40, 40, 40);

		public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 40);

		public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 40);

		public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 40);

		public override Color MenuBorder => Color.FromArgb(45, 45, 45);

		public override Color MenuItemBorder => Color.FromArgb(45, 45, 45);

		public override Color MenuItemSelected => Color.FromArgb(45, 45, 45);

		public override Color MenuStripGradientBegin => Color.FromArgb(40, 40, 40);

		public override Color MenuStripGradientEnd => Color.FromArgb(45, 45, 45);

		public override Color MenuItemSelectedGradientBegin => Color.FromArgb(40, 40, 40);

		public override Color MenuItemSelectedGradientEnd => Color.FromArgb(40, 40, 40);

		public override Color MenuItemPressedGradientBegin => Color.FromArgb(40, 40, 40);

		public override Color MenuItemPressedGradientEnd => Color.FromArgb(40, 40, 40);
	}

	private enum EdgeEnum
	{
		None,
		Right,
		Left,
		Top,
		Bottom,
		TopLeft,
		BottomRight
	}

	public const int WM_NCLBUTTONDOWN = 161;

	public const int HT_CAPTION = 2;

	private dynamic ScriptPath = Settings.Default.ScriptPath;

	public TabPanelControl tpc = new TabPanelControl();

	public bool changed;

	private IContainer components;

	private Panel panel1;

	private Label label1;

	private PictureBox pictureBox1;

	private ToolStripMenuItem clearToolStripMenuItem;

	private ToolStripMenuItem openIntoToolStripMenuItem;

	private ToolStripMenuItem saveToolStripMenuItem;

	public ContextMenuStrip TabContextMenu;

	public CustomTabControl customTabControl1;

	private TabPage tabPage1;

	private TreeView ScriptView;

	private BunifuFlatButton bunifuFlatButton1;

	private BunifuFlatButton bunifuFlatButton2;

	private BunifuFlatButton bunifuFlatButton3;

	private BunifuFlatButton bunifuFlatButton4;

	public BunifuFlatButton bunifuFlatButton5;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem executeToolStripMenuItem;

	private ToolStripMenuItem loadIntoEditorToolStripMenuItem;

	private ToolStripMenuItem deleteFileToolStripMenuItem;

	private ToolStripMenuItem changePathToolStripMenuItem;

	private ToolStripMenuItem reloadToolStripMenuItem;

	private BunifuFlatButton bunifuFlatButton6;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem fileToolStripMenuItem;

	private ToolStripMenuItem injectToolStripMenuItem;

	private ToolStripMenuItem aboutToolStripMenuItem;

	private ToolStripMenuItem gamesToolStripMenuItem;

	private ToolStripMenuItem hotScriptsToolStripMenuItem;

	private ToolStripMenuItem openGuiToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem1;

	private Button button2;

	private Button button1;

	private ToolStripMenuItem killRobloxToolStripMenuItem;

	private ToolStripMenuItem remoteSpyToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem toolStripMenuItem3;

	private System.Windows.Forms.Timer timer1;

	private ToolStripMenuItem toolStripMenuItem4;

	private ToolTip toolTip1;

	private ToolStripMenuItem toolStripMenuItem5;

	private ToolStripMenuItem toolStripMenuItem6;

	private ToolStripMenuItem toolStripMenuItem7;

	private Panel panel2;

	private Panel panel3;

	private ErrorProvider errorProvider1;

	private ToolStripMenuItem unnamedESPToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem8;

	private ToolStripMenuItem renameToolStripMenuItem;

	private bool mMouseDown;

	private const int SW_HIDE = 0;

	private const int SW_SHOW = 5;

	public static int injectedPID = 0;

	public static RegistryKey SOFTWARE = Registry.CurrentUser.OpenSubKey("SOFTWARE", writable: true);

	public static bool launcherDetected = false;

	public static double timeout = 6.0;

	private EdgeEnum mEdge;

	private bool isonEdge;

	private int mWidth = 20;

	private bool heightUnchanged = true;

	private bool widthUnchanged = true;

	private ToolStripMenuItem cMDXToolStripMenuItem;

	private bool Anim_ATF_break;

	[DllImport("user32.dll", SetLastError = true)]
	internal static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

	[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

	[DllImport("user32.dll")]
	public static extern bool ReleaseCapture();

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetConsoleWindow();

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	public void PopulateTree(dynamic dir, TreeNode node)
	{
		try
		{
			dynamic val = new DirectoryInfo(dir);
			foreach (dynamic directory in val.GetDirectories())
			{
				dynamic val2 = new TreeNode(directory.Name);
				if (((dynamic)(node == null)) ? true : false)
				{
					ScriptView.Nodes.Add(val2);
				}
				else
				{
					node.Nodes.Add(val2);
				}
				this.PopulateTree(directory.FullName, val2);
			}
			foreach (dynamic file in val.GetFiles())
			{
				dynamic val3 = new TreeNode(file.Name);
				if ((dynamic)node != null)
				{
					node.Nodes.Add(val3);
				}
				else
				{
					ScriptView.Nodes.Add(val3);
				}
			}
		}
		catch
		{
		}
	}

	private void ScriptLoading()
	{
		try
		{
			dynamic val = Directory.Exists(Settings.Default.ScriptPath);
			if ((!val))
			{
				Directory.CreateDirectory(Settings.Default.ScriptPath);
			}
		}
		catch
		{
		}
		PopulateTree(Settings.Default.ScriptPath, null);
	}

	public krnl()
	{
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		InitializeComponent();
		panel3.Width = base.Width;
		CustomTabControl.Form1 = this;
		customTabControl1.ShowClosingButton = true;
	}

	private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Exception ex = (Exception)e.ExceptionObject;
		File.WriteAllText("error.txt", string.Join("\n", "Message: " + ex.Message, "StackTrace: " + ex.StackTrace, "Source: " + ex.Source, "TargetSite: " + ex.TargetSite, "HResult: " + ex.HResult, "HelpLink: " + ex.HelpLink, "Values: [ " + string.Join("\n", ex.Data.Values) + " ]"));
		MessageBox.Show("Send `error.txt` to krnl server", "Caught an oopsies!");
		DialogResult dialogResult = MessageBox.Show("Click `Yes` if you want to get an invite to krnl discord server.", "Krnl Prompt", MessageBoxButtons.YesNo);
		if ((dynamic)dialogResult == DialogResult.Yes)
		{
			Process.Start("https://krnl.ca/invite.php");
		}
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		for (int i = 0; i < hotScriptsToolStripMenuItem.DropDownItems.Count; i++)
		{
			ToolStripItem toolStripItem = hotScriptsToolStripMenuItem.DropDownItems[i];
			if (toolStripItem.Text == "Owl Hub" || toolStripItem.Text == "Galaxy Hub")
			{
				toolStripItem.Visible = false;
				toolStripItem.Enabled = false;
			}
		}
		if (!Directory.Exists("bin/tabs"))
		{
			Directory.CreateDirectory("bin/tabs");
		}
		if (Directory.GetFiles("bin/tabs").Length != 0)
		{
			for (int j = 0; j < Directory.GetFiles("bin/tabs").Length / 2; j++)
			{
				customTabControl1.addnewtab();
				try
				{
					using (FileStream stream = new FileStream($"bin/tabs/{j}_name.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
						customTabControl1.TabPages[customTabControl1.TabPages.Count - 2].Text = streamReader.ReadToEnd();
						streamReader.Close();
					}
					using FileStream stream2 = new FileStream($"bin/tabs/{j}_source.lua", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					using StreamReader streamReader2 = new StreamReader(stream2, Encoding.UTF8);
					customTabControl1.GetWorkingTextEditor().Text = streamReader2.ReadToEnd();
					streamReader2.Close();
				}
				catch
				{
				}
			}
		}
		else
		{
			customTabControl1.addnewtab();
		}
		if ((dynamic)(!Directory.Exists(Settings.Default.ScriptPath)))
		{
			Settings.Default.ScriptPath = Environment.CurrentDirectory + "\\scripts";
		}
		menuStrip1.Renderer = new ToolStripProfessionalRenderer(new BrowserColors());
		ScriptLoading();
		Anim_ATF_break = true;
		anim_AwaitingTaskFinish();
	}

	private async void button1_Click(object sender, EventArgs e)
	{
		while (base.Opacity > 0.0)
		{
			await Task.Delay(10);
			base.Opacity -= 0.1;
		}
		try
		{
			string[] files = Directory.GetFiles("bin/tabs");
			for (int i = 0; i < ((files.Length != 0) ? (files.Length / 2) : 0); i++)
			{
				if (customTabControl1.TabPages.Count <= i + 1)
				{
					File.Delete($"bin/tabs/{i}_name.txt");
					File.Delete($"bin/tabs/{i}_source.lua");
				}
			}
			for (int j = 0; j < customTabControl1.TabPages.Count; j++)
			{
				TabPage tabPage = customTabControl1.TabPages[j];
				if (tabPage.Controls.Count > 0)
				{
					Scintilla scintilla = tabPage.Controls[0] as Scintilla;
					File.WriteAllText($"bin/tabs/{j}_name.txt", tabPage.Text.ToString());
					File.WriteAllText($"bin/tabs/{j}_source.lua", scintilla.Text.ToString());
				}
			}
		}
		catch
		{
		}
		Environment.Exit(Environment.ExitCode);
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void panel1_MouseMove(object sender, MouseEventArgs e)
	{
		if ((dynamic)e.Button == MouseButtons.Left)
		{
			ReleaseCapture();
			SendMessage(base.Handle, 161, 2, 0);
		}
	}

	private void tabPage1_Click(object sender, EventArgs e)
	{
	}

	private void closeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TabPage contextTab = customTabControl1.contextTab;
		customTabControl1.CloseTab(contextTab);
	}

	private void clearToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Scintilla scintilla = customTabControl1.contextTab.Controls[0] as Scintilla;
		scintilla.Text = "";
		scintilla.Refresh();
	}

	private void openIntoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TabPage contextTab = customTabControl1.contextTab;
		if ((dynamic)contextTab == null)
		{
			throw new Exception("SELECTED TAB NOT FOUND");
		}
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.CheckFileExists = true;
		openFileDialog.Filter = "Script Files (*.txt, *.lua)|*.txt;*.lua|All Files (*.*)|*.*";
		openFileDialog.RestoreDirectory = true;
		if ((dynamic)openFileDialog.ShowDialog() == DialogResult.OK)
		{
			contextTab.Text = Path.GetFileNameWithoutExtension(openFileDialog.SafeFileName);
			dynamic val = File.ReadAllText(openFileDialog.FileName);
			Scintilla scintilla = contextTab.Controls[0] as Scintilla;
			scintilla.Text = val;
			scintilla.Refresh();
		}
	}

	private void saveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		TabPage contextTab = customTabControl1.contextTab;
		if ((dynamic)contextTab == null)
		{
			throw new Exception("TAB NOT FOUND");
		}
		contextTab.Text = customTabControl1.OpenSaveDialog(contextTab, contextTab.Controls[0].Text);
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool WaitNamedPipe(string name, int timeout);

	private static bool findpipe(string pipeName)
	{
		if ((dynamic)(!WaitNamedPipe(Path.GetFullPath("\\\\.\\pipe\\" + pipeName), 0)) && (Marshal.GetLastWin32Error() == 0 || Marshal.GetLastWin32Error() == 2))
		{
			return false;
		}
		return true;
	}

	public static void pipeshit(string script)
	{
		try
		{
			if ((dynamic)findpipe("krnlpipe"))
			{
				using NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", "krnlpipe", PipeDirection.Out);
				namedPipeClientStream.Connect();
				if ((dynamic)(!namedPipeClientStream.IsConnected))
				{
					throw new IOException("Failed To Connect To Pipe....");
				}
				StreamWriter streamWriter = new StreamWriter(namedPipeClientStream, Encoding.Default, 999999);
				streamWriter.Write(script);
				streamWriter.Dispose();
			}
			else
			{
				MessageBox.Show("Please Inject To Execute Scripts", "krnl");
			}
		}
		catch (Exception)
		{
		}
	}

	public static void Pipe(string script)
	{
		if ((dynamic)findpipe("krnlpipe"))
		{
			pipeshit(script);
		}
		else
		{
			MessageBox.Show("Please Inject To Execute Scripts", "krnl");
		}
	}

	private void bunifuFlatButton1_Click(object sender, EventArgs e)
	{
		try
		{
			Pipe(customTabControl1.GetWorkingTextEditor().Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void bunifuFlatButton2_Click(object sender, EventArgs e)
	{
		customTabControl1.GetWorkingTextEditor().Text = "";
	}

	private void bunifuFlatButton3_Click(object sender, EventArgs e)
	{
		customTabControl1.OpenFileDialog(customTabControl1.SelectedTab);
	}

	private void bunifuFlatButton4_Click(object sender, EventArgs e)
	{
		ScriptView.Nodes.Clear();
		ScriptLoading();
		customTabControl1.OpenSaveDialog(customTabControl1.SelectedTab, customTabControl1.GetWorkingTextEditor().Text);
	}

	private void injectToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void bunifuFlatButton5_Click(object sender, EventArgs e)
	{
		new Thread((ParameterizedThreadStart)delegate
		{
			try
			{
				Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
				IntPtr intPtr = FindWindowA("WINDOWSCLIENT", "Roblox");
				int lpdwProcessId = 0;
				GetWindowThreadProcessId(intPtr, out lpdwProcessId);
				if ((dynamic)(nint)intPtr != IntPtr.Zero)
				{
					List<string> list = processesByName[0].MainModule.FileName.Split('\\').ToList();
					list.Remove("RobloxPlayerBeta.exe");
					string directory = string.Join("\\", list.ToArray());
					Program.writeToDir(directory);
					try
					{
						bool flag = injectdll("krnl.dll", lpdwProcessId);
						anim_CompletedTask();
					}
					catch
					{
					}
				}
				else
				{
					MessageBox.Show("Roblox Process Not Found", "Krnl");
				}
			}
			catch
			{
				MessageBox.Show("Caught an unknown error while injecting!");
			}
		}).Start();
	}

	private void executeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			dynamic fullPath = ScriptView.SelectedNode.FullPath;
			dynamic val = File.ReadAllText(Settings.Default.ScriptPath + "//" + fullPath);
			Pipe(val);
		}
		catch
		{
		}
	}

	private void loadIntoEditorToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			dynamic fullPath = ScriptView.SelectedNode.FullPath;
			dynamic val = File.ReadAllText(Settings.Default.ScriptPath + "//" + fullPath);
			customTabControl1.GetWorkingTextEditor().Text = val;
		}
		catch (Exception)
		{
		}
	}

	private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			dynamic fullPath = ScriptView.SelectedNode.FullPath;
			File.Delete(Settings.Default.ScriptPath + "//" + fullPath);
		}
		catch (Exception)
		{
		}
	}

	private void changePathToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			dynamic val = new FolderBrowserDialog();
			using ((IDisposable)val)
			{
				dynamic val2 = val.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(val.SelectedPath);
				if (val2)
				{
					ScriptPath = val.SelectedPath;
					Settings.Default.ScriptPath = val.SelectedPath;
					Settings.Default.Save();
				}
			}
			ScriptView.Nodes.Clear();
			ScriptLoading();
		}
		catch
		{
		}
	}

	private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ScriptView.Nodes.Clear();
		ScriptLoading();
	}

	private void ScriptView_AfterSelect(object sender, TreeViewEventArgs e)
	{
	}

	private void customTabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void renameToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void openGuiToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://pastebin.com/raw/UXmbai5q', true))()");
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/CriShoux/OwlHub/master/OwlHub.txt'))();");
	}

	private void toolStripMenuItem2_Click(object sender, EventArgs e)
	{
		Pipe("if game:GetService'CoreGui':FindFirstChild'Dex'then game:GetService'CoreGui'.Dex:Destroy()end;math.randomseed(tick())local a={}for b=48,57 do table.insert(a,string.char(b))end;for b=65,90 do table.insert(a,string.char(b))end;for b=97,122 do table.insert(a,string.char(b))end;function RandomCharacters(c)if c>0 then return RandomCharacters(c-1)..a[math.random(1,#a)]else return''end end;local d=game:GetObjects('rbxassetid://3567096419')[1]d.Name=RandomCharacters(math.random(5,20))d.Parent=game:GetService('CoreGui')local function e(f,g)local function h(i,j)local k={}local l={script=j}local m={}m.__index=function(n,o)if l[o]==nil then return getfenv()[o]else return l[o]end end;m.__newindex=function(n,o,p)if l[o]==nil then getfenv()[o]=p else l[o]=p end end;setmetatable(k,m)setfenv(i,k)return i end;local function q(j)if j.ClassName=='Script'or j.ClassName=='LocalScript'then spawn(function()h(loadstring(j.Source,'='..j:GetFullName()),j)()end)end;for b,r in pairs(j:GetChildren())do q(r)end end;q(f)end;e(d)");
	}

	private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void bunifuFlatButton6_Click(object sender, EventArgs e)
	{
		if ((dynamic)Application.OpenForms.OfType<settings>().Count() != 1)
		{
			new settings(this).Show();
			Application.OpenForms.OfType<settings>().First().SetDesktopLocation(base.Location.X + base.Size.Width + 5, base.Location.Y);
		}
	}

	private void gamesToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		MessageBox.Show("Disabled as most scripts are patched.");
	}

	private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		if ((dynamic)Application.OpenForms.OfType<About>().Count() != 1)
		{
			new About().Show();
			Application.OpenForms.OfType<About>().First().SetDesktopLocation(base.Location.X + base.Size.Width + 5, base.Location.Y);
		}
	}

	private void injectToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		IntPtr intPtr = FindWindowA("WINDOWSCLIENT", "Roblox");
		Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
		int lpdwProcessId = 0;
		GetWindowThreadProcessId(intPtr, out lpdwProcessId);
		for (int i = 0; i < processesByName.Length; i++)
		{
			if ((dynamic)processesByName[i].Id != lpdwProcessId)
			{
				processesByName[i].Kill();
			}
		}
		if ((dynamic)(nint)intPtr != IntPtr.Zero)
		{
			List<string> list = processesByName[0].MainModule.FileName.Split('\\').ToList();
			list.Remove("RobloxPlayerBeta.exe");
			string directory = string.Join("\\", list.ToArray());
			Program.writeToDir(directory);
			string[] array = new string[1] { "krnl" };
			for (int j = 0; j < array.Length && (!(((dynamic)(!injectdll(array[j] + ".dll", lpdwProcessId))) ? true : false) || 1 == 0); j++)
			{
				Task.Delay(10).GetAwaiter().GetResult();
				anim_CompletedTask();
			}
		}
		else
		{
			MessageBox.Show("Roblox Process Not Found", "Krnl");
		}
	}

	private void openGuiToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://pastebin.com/raw/UXmbai5q', true))()");
	}

	private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/CriShoux/OwlHub/master/OwlHub.txt'))();");
	}

	private void killRobloxToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
		int num = 0;
		for (int i = 0; i < processesByName.Length; i++)
		{
			processesByName[i].Kill();
			num++;
		}
		MessageBox.Show($"Terminated {num} Process", "krnl");
	}

	private void krnl_FormClosing(object sender, FormClosingEventArgs e)
	{
		TabPage tabPage = null;
		Scintilla scintilla = null;
		try
		{
			string[] files = Directory.GetFiles("bin/tabs");
			for (int i = 0; i < ((files.Length != 0) ? (files.Length / 2) : 0); i++)
			{
				if (customTabControl1.TabPages.Count <= i + 1)
				{
					File.Delete($"bin/tabs/{i}_name.txt");
					File.Delete($"bin/tabs/{i}_source.lua");
				}
			}
			for (int j = 0; j < customTabControl1.TabPages.Count; j++)
			{
				tabPage = customTabControl1.TabPages[j];
				if (tabPage.Controls.Count > 0)
				{
					scintilla = tabPage.Controls[0] as Scintilla;
					File.WriteAllText($"bin/tabs/{j}_name.txt", tabPage.Text.ToString());
					File.WriteAllText($"bin/tabs/{j}_source.lua", scintilla.Text.ToString());
				}
			}
		}
		catch
		{
		}
		Environment.Exit(Environment.ExitCode);
	}

	private async void krnl_Deactivate(object sender, EventArgs e)
	{
		if ((dynamic)Settings.Default.fadein_out_opacity)
		{
			while (base.Opacity > 0.5)
			{
				await Task.Delay(10);
				base.Opacity -= 0.05;
			}
		}
		else
		{
			base.Opacity = 1.0;
		}
	}

	private async void krnl_Activated(object sender, EventArgs e)
	{
		if ((dynamic)Settings.Default.fadein_out_opacity)
		{
			while (base.Opacity < 1.0)
			{
				await Task.Delay(10);
				base.Opacity += 0.05;
			}
		}
		else
		{
			base.Opacity = 1.0;
		}
	}

	private void gameSenseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://pastebin.com/raw/rPnPiYZV'))();");
	}

	private void remoteSpyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://pastebin.com/raw/JZaJe9Sg'))();");
	}

	protected override void Dispose(bool disposing)
	{
		if ((dynamic)disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(krnlss.krnl));
		panel1 = new System.Windows.Forms.Panel();
		panel3 = new System.Windows.Forms.Panel();
		button2 = new System.Windows.Forms.Button();
		button1 = new System.Windows.Forms.Button();
		pictureBox1 = new System.Windows.Forms.PictureBox();
		label1 = new System.Windows.Forms.Label();
		TabContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
		clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		openIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		ScriptView = new System.Windows.Forms.TreeView();
		contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
		executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		loadIntoEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		changePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		bunifuFlatButton1 = new Bunifu.Framework.UI.BunifuFlatButton();
		bunifuFlatButton2 = new Bunifu.Framework.UI.BunifuFlatButton();
		bunifuFlatButton3 = new Bunifu.Framework.UI.BunifuFlatButton();
		bunifuFlatButton4 = new Bunifu.Framework.UI.BunifuFlatButton();
		bunifuFlatButton5 = new Bunifu.Framework.UI.BunifuFlatButton();
		bunifuFlatButton6 = new Bunifu.Framework.UI.BunifuFlatButton();
		menuStrip1 = new System.Windows.Forms.MenuStrip();
		fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		injectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		killRobloxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		hotScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		openGuiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		remoteSpyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		unnamedESPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
		cMDXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
		toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
		timer1 = new System.Windows.Forms.Timer(components);
		toolTip1 = new System.Windows.Forms.ToolTip(components);
		panel2 = new System.Windows.Forms.Panel();
		errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
		customTabControl1 = new Controls.CustomTabControl();
		tabPage1 = new System.Windows.Forms.TabPage();
		panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		TabContextMenu.SuspendLayout();
		contextMenuStrip1.SuspendLayout();
		menuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
		customTabControl1.SuspendLayout();
		SuspendLayout();
		panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		panel1.BackColor = System.Drawing.Color.FromArgb(29, 29, 29);
		panel1.Controls.Add(panel3);
		panel1.Controls.Add(button2);
		panel1.Controls.Add(button1);
		panel1.Controls.Add(pictureBox1);
		panel1.Controls.Add(label1);
		panel1.Location = new System.Drawing.Point(0, 0);
		panel1.Name = "panel1";
		panel1.Size = new System.Drawing.Size(690, 33);
		panel1.TabIndex = 0;
		panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(panel1_MouseMove);
		panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		panel3.BackColor = System.Drawing.Color.DodgerBlue;
		panel3.Location = new System.Drawing.Point(-1, -1);
		panel3.Name = "panel3";
		panel3.Size = new System.Drawing.Size(693, 3);
		panel3.TabIndex = 5;
		panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(krnl_MouseMove);
		button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
		button2.BackColor = System.Drawing.Color.FromArgb(29, 29, 29);
		button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		button2.FlatAppearance.BorderSize = 0;
		button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button2.Font = new System.Drawing.Font("Corbel", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		button2.ForeColor = System.Drawing.Color.White;
		button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		button2.Location = new System.Drawing.Point(620, 0);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(35, 33);
		button2.TabIndex = 4;
		button2.UseVisualStyleBackColor = false;
		button2.Click += new System.EventHandler(button2_Click);
		button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
		button1.BackColor = System.Drawing.Color.FromArgb(29, 29, 29);
		button1.FlatAppearance.BorderSize = 0;
		button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		button1.Font = new System.Drawing.Font("Corbel", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		button1.ForeColor = System.Drawing.Color.White;
		button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		button1.Location = new System.Drawing.Point(655, 0);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(35, 33);
		button1.TabIndex = 3;
		button1.UseVisualStyleBackColor = false;
		button1.Click += new System.EventHandler(button1_Click);
		button1.MouseMove += new System.Windows.Forms.MouseEventHandler(krnl_MouseMove);
		pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new System.Drawing.Point(4, 4);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new System.Drawing.Size(25, 25);
		pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		pictureBox1.TabIndex = 1;
		pictureBox1.TabStop = false;
		label1.Anchor = System.Windows.Forms.AnchorStyles.None;
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		label1.ForeColor = System.Drawing.Color.White;
		label1.Location = new System.Drawing.Point(328, 7);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(45, 20);
		label1.TabIndex = 0;
		label1.Text = "KRNL";
		label1.MouseDown += new System.Windows.Forms.MouseEventHandler(panel1_MouseMove);
		TabContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
		TabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { clearToolStripMenuItem, openIntoToolStripMenuItem, saveToolStripMenuItem, renameToolStripMenuItem });
		TabContextMenu.Name = "TabContextMenu";
		TabContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		TabContextMenu.Size = new System.Drawing.Size(128, 92);
		clearToolStripMenuItem.Name = "clearToolStripMenuItem";
		clearToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
		clearToolStripMenuItem.Text = "Clear";
		clearToolStripMenuItem.Click += new System.EventHandler(clearToolStripMenuItem_Click);
		openIntoToolStripMenuItem.Name = "openIntoToolStripMenuItem";
		openIntoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
		openIntoToolStripMenuItem.Text = "Open Into";
		openIntoToolStripMenuItem.Click += new System.EventHandler(openIntoToolStripMenuItem_Click);
		saveToolStripMenuItem.Name = "saveToolStripMenuItem";
		saveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
		saveToolStripMenuItem.Text = "Save";
		saveToolStripMenuItem.Click += new System.EventHandler(saveToolStripMenuItem_Click);
		renameToolStripMenuItem.Name = "renameToolStripMenuItem";
		renameToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
		renameToolStripMenuItem.Text = "Rename";
		renameToolStripMenuItem.Click += new System.EventHandler(renameToolStripMenuItem_Click_1);
		ScriptView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		ScriptView.BackColor = System.Drawing.Color.FromArgb(29, 29, 29);
		ScriptView.BorderStyle = System.Windows.Forms.BorderStyle.None;
		ScriptView.ContextMenuStrip = contextMenuStrip1;
		ScriptView.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		ScriptView.ForeColor = System.Drawing.Color.White;
		ScriptView.HideSelection = false;
		ScriptView.LineColor = System.Drawing.Color.White;
		ScriptView.Location = new System.Drawing.Point(565, 59);
		ScriptView.Name = "ScriptView";
		ScriptView.Size = new System.Drawing.Size(121, 259);
		ScriptView.TabIndex = 4;
		ScriptView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(ScriptView_AfterSelect);
		ScriptView.MouseMove += new System.Windows.Forms.MouseEventHandler(krnl_MouseMove);
		contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { executeToolStripMenuItem, loadIntoEditorToolStripMenuItem, deleteFileToolStripMenuItem, changePathToolStripMenuItem, reloadToolStripMenuItem });
		contextMenuStrip1.Name = "contextMenuStrip1";
		contextMenuStrip1.Size = new System.Drawing.Size(159, 114);
		executeToolStripMenuItem.Name = "executeToolStripMenuItem";
		executeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		executeToolStripMenuItem.Text = "Execute";
		executeToolStripMenuItem.Click += new System.EventHandler(executeToolStripMenuItem_Click);
		loadIntoEditorToolStripMenuItem.Name = "loadIntoEditorToolStripMenuItem";
		loadIntoEditorToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		loadIntoEditorToolStripMenuItem.Text = "Load Into Editor";
		loadIntoEditorToolStripMenuItem.Click += new System.EventHandler(loadIntoEditorToolStripMenuItem_Click);
		deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
		deleteFileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		deleteFileToolStripMenuItem.Text = "Delete File";
		deleteFileToolStripMenuItem.Click += new System.EventHandler(deleteFileToolStripMenuItem_Click);
		changePathToolStripMenuItem.Name = "changePathToolStripMenuItem";
		changePathToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		changePathToolStripMenuItem.Text = "Change Path";
		changePathToolStripMenuItem.Click += new System.EventHandler(changePathToolStripMenuItem_Click);
		reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
		reloadToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
		reloadToolStripMenuItem.Text = "Reload";
		reloadToolStripMenuItem.Click += new System.EventHandler(reloadToolStripMenuItem_Click);
		bunifuFlatButton1.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		bunifuFlatButton1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton1.BorderRadius = 0;
		bunifuFlatButton1.ButtonText = "EXECUTE";
		bunifuFlatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton1.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton1.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton1.Iconimage = null;
		bunifuFlatButton1.Iconimage_right = null;
		bunifuFlatButton1.Iconimage_right_Selected = null;
		bunifuFlatButton1.Iconimage_Selected = null;
		bunifuFlatButton1.IconMarginLeft = 0;
		bunifuFlatButton1.IconMarginRight = 0;
		bunifuFlatButton1.IconRightVisible = true;
		bunifuFlatButton1.IconRightZoom = 0.0;
		bunifuFlatButton1.IconVisible = true;
		bunifuFlatButton1.IconZoom = 20.0;
		bunifuFlatButton1.IsTab = false;
		bunifuFlatButton1.Location = new System.Drawing.Point(4, 321);
		bunifuFlatButton1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton1.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton1.Name = "bunifuFlatButton1";
		bunifuFlatButton1.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton1.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton1.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton1.selected = false;
		bunifuFlatButton1.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton1.TabIndex = 7;
		bunifuFlatButton1.Text = "EXECUTE";
		bunifuFlatButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton1.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton1.Click += new System.EventHandler(bunifuFlatButton1_Click);
		bunifuFlatButton2.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		bunifuFlatButton2.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton2.BorderRadius = 0;
		bunifuFlatButton2.ButtonText = "CLEAR";
		bunifuFlatButton2.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton2.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton2.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton2.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton2.Iconimage = null;
		bunifuFlatButton2.Iconimage_right = null;
		bunifuFlatButton2.Iconimage_right_Selected = null;
		bunifuFlatButton2.Iconimage_Selected = null;
		bunifuFlatButton2.IconMarginLeft = 0;
		bunifuFlatButton2.IconMarginRight = 0;
		bunifuFlatButton2.IconRightVisible = true;
		bunifuFlatButton2.IconRightZoom = 0.0;
		bunifuFlatButton2.IconVisible = true;
		bunifuFlatButton2.IconZoom = 20.0;
		bunifuFlatButton2.IsTab = false;
		bunifuFlatButton2.Location = new System.Drawing.Point(107, 321);
		bunifuFlatButton2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton2.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton2.Name = "bunifuFlatButton2";
		bunifuFlatButton2.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton2.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton2.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton2.selected = false;
		bunifuFlatButton2.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton2.TabIndex = 8;
		bunifuFlatButton2.Text = "CLEAR";
		bunifuFlatButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton2.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton2.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton2.Click += new System.EventHandler(bunifuFlatButton2_Click);
		bunifuFlatButton3.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		bunifuFlatButton3.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton3.BorderRadius = 0;
		bunifuFlatButton3.ButtonText = "OPEN FILE";
		bunifuFlatButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton3.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton3.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton3.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton3.Iconimage = null;
		bunifuFlatButton3.Iconimage_right = null;
		bunifuFlatButton3.Iconimage_right_Selected = null;
		bunifuFlatButton3.Iconimage_Selected = null;
		bunifuFlatButton3.IconMarginLeft = 0;
		bunifuFlatButton3.IconMarginRight = 0;
		bunifuFlatButton3.IconRightVisible = true;
		bunifuFlatButton3.IconRightZoom = 0.0;
		bunifuFlatButton3.IconVisible = true;
		bunifuFlatButton3.IconZoom = 20.0;
		bunifuFlatButton3.IsTab = false;
		bunifuFlatButton3.Location = new System.Drawing.Point(210, 321);
		bunifuFlatButton3.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton3.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton3.Name = "bunifuFlatButton3";
		bunifuFlatButton3.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton3.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton3.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton3.selected = false;
		bunifuFlatButton3.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton3.TabIndex = 9;
		bunifuFlatButton3.Text = "OPEN FILE";
		bunifuFlatButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton3.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton3.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton3.Click += new System.EventHandler(bunifuFlatButton3_Click);
		bunifuFlatButton4.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		bunifuFlatButton4.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton4.BorderRadius = 0;
		bunifuFlatButton4.ButtonText = "SAVE FILE";
		bunifuFlatButton4.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton4.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton4.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton4.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton4.Iconimage = null;
		bunifuFlatButton4.Iconimage_right = null;
		bunifuFlatButton4.Iconimage_right_Selected = null;
		bunifuFlatButton4.Iconimage_Selected = null;
		bunifuFlatButton4.IconMarginLeft = 0;
		bunifuFlatButton4.IconMarginRight = 0;
		bunifuFlatButton4.IconRightVisible = true;
		bunifuFlatButton4.IconRightZoom = 0.0;
		bunifuFlatButton4.IconVisible = true;
		bunifuFlatButton4.IconZoom = 20.0;
		bunifuFlatButton4.IsTab = false;
		bunifuFlatButton4.Location = new System.Drawing.Point(313, 321);
		bunifuFlatButton4.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton4.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton4.Name = "bunifuFlatButton4";
		bunifuFlatButton4.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton4.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton4.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton4.selected = false;
		bunifuFlatButton4.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton4.TabIndex = 10;
		bunifuFlatButton4.Text = "SAVE FILE";
		bunifuFlatButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton4.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton4.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton4.Click += new System.EventHandler(bunifuFlatButton4_Click);
		bunifuFlatButton5.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		bunifuFlatButton5.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton5.BorderRadius = 0;
		bunifuFlatButton5.ButtonText = "INJECT";
		bunifuFlatButton5.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton5.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton5.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton5.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton5.Iconimage = null;
		bunifuFlatButton5.Iconimage_right = null;
		bunifuFlatButton5.Iconimage_right_Selected = null;
		bunifuFlatButton5.Iconimage_Selected = null;
		bunifuFlatButton5.IconMarginLeft = 0;
		bunifuFlatButton5.IconMarginRight = 0;
		bunifuFlatButton5.IconRightVisible = true;
		bunifuFlatButton5.IconRightZoom = 0.0;
		bunifuFlatButton5.IconVisible = true;
		bunifuFlatButton5.IconZoom = 20.0;
		bunifuFlatButton5.IsTab = false;
		bunifuFlatButton5.Location = new System.Drawing.Point(416, 321);
		bunifuFlatButton5.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton5.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton5.Name = "bunifuFlatButton5";
		bunifuFlatButton5.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton5.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton5.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton5.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton5.selected = false;
		bunifuFlatButton5.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton5.TabIndex = 11;
		bunifuFlatButton5.Text = "INJECT";
		bunifuFlatButton5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton5.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton5.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton5.Click += new System.EventHandler(bunifuFlatButton5_Click);
		bunifuFlatButton6.Activecolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		bunifuFlatButton6.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		bunifuFlatButton6.BorderRadius = 0;
		bunifuFlatButton6.ButtonText = "OPTIONS";
		bunifuFlatButton6.Cursor = System.Windows.Forms.Cursors.Hand;
		bunifuFlatButton6.DisabledColor = System.Drawing.Color.Gray;
		bunifuFlatButton6.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton6.Iconcolor = System.Drawing.Color.Transparent;
		bunifuFlatButton6.Iconimage = null;
		bunifuFlatButton6.Iconimage_right = null;
		bunifuFlatButton6.Iconimage_right_Selected = null;
		bunifuFlatButton6.Iconimage_Selected = null;
		bunifuFlatButton6.IconMarginLeft = 0;
		bunifuFlatButton6.IconMarginRight = 0;
		bunifuFlatButton6.IconRightVisible = true;
		bunifuFlatButton6.IconRightZoom = 0.0;
		bunifuFlatButton6.IconVisible = true;
		bunifuFlatButton6.IconZoom = 20.0;
		bunifuFlatButton6.IsTab = false;
		bunifuFlatButton6.Location = new System.Drawing.Point(586, 321);
		bunifuFlatButton6.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
		bunifuFlatButton6.MinimumSize = new System.Drawing.Size(84, 25);
		bunifuFlatButton6.Name = "bunifuFlatButton6";
		bunifuFlatButton6.Normalcolor = System.Drawing.Color.FromArgb(36, 36, 36);
		bunifuFlatButton6.OnHovercolor = System.Drawing.Color.FromArgb(39, 39, 39);
		bunifuFlatButton6.OnHoverTextColor = System.Drawing.Color.White;
		bunifuFlatButton6.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
		bunifuFlatButton6.selected = false;
		bunifuFlatButton6.Size = new System.Drawing.Size(100, 25);
		bunifuFlatButton6.TabIndex = 12;
		bunifuFlatButton6.Text = "OPTIONS";
		bunifuFlatButton6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		bunifuFlatButton6.Textcolor = System.Drawing.Color.White;
		bunifuFlatButton6.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		bunifuFlatButton6.Click += new System.EventHandler(bunifuFlatButton6_Click);
		menuStrip1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		menuStrip1.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
		menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { fileToolStripMenuItem, aboutToolStripMenuItem, gamesToolStripMenuItem, hotScriptsToolStripMenuItem, toolStripMenuItem5 });
		menuStrip1.Location = new System.Drawing.Point(0, 33);
		menuStrip1.Name = "menuStrip1";
		menuStrip1.Size = new System.Drawing.Size(289, 24);
		menuStrip1.TabIndex = 13;
		menuStrip1.Text = "menuStrip1";
		fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { injectToolStripMenuItem, killRobloxToolStripMenuItem });
		fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		fileToolStripMenuItem.Text = "File";
		injectToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		injectToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		injectToolStripMenuItem.Name = "injectToolStripMenuItem";
		injectToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		injectToolStripMenuItem.Text = "Inject";
		injectToolStripMenuItem.Click += new System.EventHandler(injectToolStripMenuItem_Click_1);
		killRobloxToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		killRobloxToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		killRobloxToolStripMenuItem.Name = "killRobloxToolStripMenuItem";
		killRobloxToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		killRobloxToolStripMenuItem.Text = "Kill Roblox";
		killRobloxToolStripMenuItem.Click += new System.EventHandler(killRobloxToolStripMenuItem_Click);
		aboutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
		aboutToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
		aboutToolStripMenuItem.Text = "Credits";
		aboutToolStripMenuItem.Click += new System.EventHandler(aboutToolStripMenuItem_Click_1);
		gamesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		gamesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
		gamesToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
		gamesToolStripMenuItem.Text = "Games";
		gamesToolStripMenuItem.Click += new System.EventHandler(gamesToolStripMenuItem_Click_1);
		hotScriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { toolStripMenuItem2, openGuiToolStripMenuItem, toolStripMenuItem4, toolStripMenuItem1, remoteSpyToolStripMenuItem, toolStripMenuItem3, unnamedESPToolStripMenuItem, toolStripMenuItem8, cMDXToolStripMenuItem });
		hotScriptsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		hotScriptsToolStripMenuItem.Name = "hotScriptsToolStripMenuItem";
		hotScriptsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
		hotScriptsToolStripMenuItem.Text = "Hot-Scripts";
		toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem2.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem2.Name = "toolStripMenuItem2";
		toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
		toolStripMenuItem2.Text = "DarkDex";
		toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click);
		openGuiToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		openGuiToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		openGuiToolStripMenuItem.Name = "openGuiToolStripMenuItem";
		openGuiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		openGuiToolStripMenuItem.Text = "OpenGui";
		openGuiToolStripMenuItem.Click += new System.EventHandler(openGuiToolStripMenuItem_Click_1);
		toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem4.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem4.Name = "toolStripMenuItem4";
		toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
		toolStripMenuItem4.Text = "Owl Hub";
		toolStripMenuItem4.Click += new System.EventHandler(toolStripMenuItem4_Click);
		toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem1.Name = "toolStripMenuItem1";
		toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		toolStripMenuItem1.Text = "Galaxy Hub";
		toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click_2);
		remoteSpyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		remoteSpyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		remoteSpyToolStripMenuItem.Name = "remoteSpyToolStripMenuItem";
		remoteSpyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		remoteSpyToolStripMenuItem.Text = "Remote Spy";
		remoteSpyToolStripMenuItem.Click += new System.EventHandler(remoteSpyToolStripMenuItem_Click);
		toolStripMenuItem3.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem3.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem3.Name = "toolStripMenuItem3";
		toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
		toolStripMenuItem3.Text = "Game Sense";
		toolStripMenuItem3.Click += new System.EventHandler(gameSenseToolStripMenuItem_Click);
		unnamedESPToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		unnamedESPToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		unnamedESPToolStripMenuItem.Name = "unnamedESPToolStripMenuItem";
		unnamedESPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		unnamedESPToolStripMenuItem.Text = "Unnamed ESP";
		unnamedESPToolStripMenuItem.Click += new System.EventHandler(unnamedESPToolStripMenuItem_Click);
		toolStripMenuItem8.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem8.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem8.Name = "toolStripMenuItem8";
		toolStripMenuItem8.Size = new System.Drawing.Size(180, 22);
		toolStripMenuItem8.Text = "Infinite Yield";
		toolStripMenuItem8.Click += new System.EventHandler(toolStripMenuItem8_Click);
		cMDXToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		cMDXToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		cMDXToolStripMenuItem.Name = "cMDXToolStripMenuItem";
		cMDXToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		cMDXToolStripMenuItem.Text = "CMD-X";
		cMDXToolStripMenuItem.Click += new System.EventHandler(cMDXToolStripMenuItem_Click);
		toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { toolStripMenuItem6, toolStripMenuItem7 });
		toolStripMenuItem5.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem5.Name = "toolStripMenuItem5";
		toolStripMenuItem5.Size = new System.Drawing.Size(54, 20);
		toolStripMenuItem5.Text = "Others";
		toolStripMenuItem6.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem6.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem6.Name = "toolStripMenuItem6";
		toolStripMenuItem6.Size = new System.Drawing.Size(173, 22);
		toolStripMenuItem6.Text = "Get Key";
		toolStripMenuItem6.Click += new System.EventHandler(bunifuFlatButton8_Click);
		toolStripMenuItem7.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		toolStripMenuItem7.ForeColor = System.Drawing.Color.White;
		toolStripMenuItem7.Name = "toolStripMenuItem7";
		toolStripMenuItem7.Size = new System.Drawing.Size(173, 22);
		toolStripMenuItem7.Text = "Join Discord Server";
		toolStripMenuItem7.Click += new System.EventHandler(toolStripMenuItem7_Click);
		timer1.Enabled = true;
		timer1.Interval = 1000;
		timer1.Tick += new System.EventHandler(timer1_Tick);
		panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		panel2.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
		panel2.Location = new System.Drawing.Point(288, 32);
		panel2.Name = "panel2";
		panel2.Size = new System.Drawing.Size(402, 25);
		panel2.TabIndex = 0;
		panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(krnl_MouseMove);
		errorProvider1.ContainerControl = this;
		customTabControl1.ActiveColor = System.Drawing.Color.FromArgb(30, 30, 30);
		customTabControl1.AllowDrop = true;
		customTabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		customTabControl1.BackTabColor = System.Drawing.Color.FromArgb(36, 36, 36);
		customTabControl1.BorderColor = System.Drawing.Color.FromArgb(30, 30, 30);
		customTabControl1.ClosingButtonColor = System.Drawing.Color.WhiteSmoke;
		customTabControl1.ClosingMessage = null;
		customTabControl1.Controls.Add(tabPage1);
		customTabControl1.HeaderColor = System.Drawing.Color.FromArgb(45, 45, 48);
		customTabControl1.HorizontalLineColor = System.Drawing.Color.FromArgb(30, 30, 30);
		customTabControl1.ItemSize = new System.Drawing.Size(240, 16);
		customTabControl1.Location = new System.Drawing.Point(4, 59);
		customTabControl1.Name = "customTabControl1";
		customTabControl1.SelectedIndex = 0;
		customTabControl1.SelectedTextColor = System.Drawing.Color.FromArgb(255, 255, 255);
		customTabControl1.ShowClosingButton = false;
		customTabControl1.ShowClosingMessage = false;
		customTabControl1.Size = new System.Drawing.Size(556, 259);
		customTabControl1.TabIndex = 3;
		customTabControl1.TextColor = System.Drawing.Color.FromArgb(255, 255, 255);
		customTabControl1.SelectedIndexChanged += new System.EventHandler(customTabControl1_SelectedIndexChanged);
		tabPage1.BackColor = System.Drawing.Color.FromArgb(36, 36, 36);
		tabPage1.Location = new System.Drawing.Point(4, 20);
		tabPage1.Name = "tabPage1";
		tabPage1.Size = new System.Drawing.Size(548, 235);
		tabPage1.TabIndex = 0;
		tabPage1.Click += new System.EventHandler(tabPage1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
		base.ClientSize = new System.Drawing.Size(690, 350);
		base.Controls.Add(menuStrip1);
		base.Controls.Add(ScriptView);
		base.Controls.Add(customTabControl1);
		base.Controls.Add(panel1);
		base.Controls.Add(panel2);
		base.Controls.Add(bunifuFlatButton5);
		base.Controls.Add(bunifuFlatButton4);
		base.Controls.Add(bunifuFlatButton3);
		base.Controls.Add(bunifuFlatButton2);
		base.Controls.Add(bunifuFlatButton1);
		base.Controls.Add(bunifuFlatButton6);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "krnl";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "krnl";
		base.TopMost = true;
		base.Activated += new System.EventHandler(krnl_Activated);
		base.Deactivate += new System.EventHandler(krnl_Deactivate);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(krnl_FormClosing);
		base.Load += new System.EventHandler(Form1_Load);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(krnl_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(krnl_MouseMove);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(krnl_MouseUp);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		TabContextMenu.ResumeLayout(false);
		contextMenuStrip1.ResumeLayout(false);
		menuStrip1.ResumeLayout(false);
		menuStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
		customTabControl1.ResumeLayout(false);
		ResumeLayout(false);
		PerformLayout();
	}

	private void unnamedESPToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://ic3w0lf.xyz/rblx/protoesp.lua', true))()");
	}

	private bool injectdll(dynamic filename, int PID)
	{
		Program.injecting = true;
		Program.form.Invoke((MethodInvoker)delegate
		{
			(Program.form.Controls["bunifuFlatButton5"] as BunifuFlatButton).Text = "INJECTING";
		});
		krnlgay.krnlgayResult krnlgayResult = krnlgay.DllInjector.GetInstance.Inject(Application.StartupPath + $"\\\\{(object)filename}", PID);
		string value = "";
		string text = "";
		if ((dynamic)krnlgayResult == krnlgay.krnlgayResult.DllNotFound)
		{
			value = $"{(object)filename} is missing!";
		}
		if ((dynamic)krnlgayResult == krnlgay.krnlgayResult.Failed)
		{
			value = "Failed to inject for unknown reason.";
		}
		if ((dynamic)krnlgayResult == krnlgay.krnlgayResult.Success)
		{
			injectedPID = PID;
		}
		if ((dynamic)krnlgayResult == krnlgay.krnlgayResult.threaderr)
		{
			value = "Caught Thread Error";
			text = "Unknown Error";
		}
		if ((dynamic)(!string.IsNullOrEmpty(value)))
		{
			Program.injecting = false;
			Program.failed_inject = true;
			MessageBox.Show(value, (text != "") ? text : "Krnl Error");
			return false;
		}
		return true;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if ((dynamic)Settings.Default.remove_crash_logs)
		{
			try
			{
				if ((dynamic)Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\logs\\archive"))
				{
					Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\logs\\archive", recursive: true);
				}
				else if ((dynamic)Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Roblox\\logs\\archive"))
				{
					Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Roblox\\logs\\archive", recursive: true);
				}
			}
			catch
			{
			}
		}
		if (!(((dynamic)Settings.Default.autoinject) ? true : false) || 1 == 0)
		{
			return;
		}
		if ((dynamic)Process.GetProcessesByName("RobloxPlayerLauncher").Length > 0)
		{
			launcherDetected = true;
		}
		if ((dynamic)launcherDetected)
		{
			try
			{
				try
				{
					injectedPID = 0;
				}
				catch (ArgumentException)
				{
				}
			}
			catch (Win32Exception)
			{
			}
			if ((dynamic)Process.GetProcessesByName("RobloxPlayerLauncher").Length == 0 && Process.GetProcessesByName("RobloxPlayerBeta").Length != 0)
			{
				injectedPID = 0;
				launcherDetected = false;
				timeout = 6.0;
				new Thread((ThreadStart)delegate
				{
					Invoke((MethodInvoker)delegate
					{
						bunifuFlatButton5.Text = "Injecting...";
					});
					Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
					int num = 0;
					while (processesByName.Length == 0 && num != 10)
					{
						processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
						num++;
					}
					if ((dynamic)num != 10)
					{
						IntPtr intPtr = FindWindowA("WINDOWSCLIENT", "Roblox");
						int lpdwProcessId = 0;
						GetWindowThreadProcessId(intPtr, out lpdwProcessId);
						if ((dynamic)(nint)intPtr != IntPtr.Zero)
						{
							List<string> list = processesByName[0].MainModule.FileName.Split('\\').ToList();
							list.Remove("RobloxPlayerBeta.exe");
							string directory = string.Join("\\", list.ToArray());
							Program.writeToDir(directory);
							try
							{
								bool flag = injectdll("krnl.dll", lpdwProcessId);
								anim_CompletedTask();
							}
							catch
							{
							}
						}
						Invoke((MethodInvoker)delegate
						{
							bunifuFlatButton5.Text = "INJECT";
						});
					}
				}).Start();
				return;
			}
			timeout -= 1.0;
		}
		if ((dynamic)timeout == 0)
		{
			launcherDetected = false;
			timeout = 6.0;
		}
	}

	private void toolStripMenuItem1_Click_2(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/LaziestBoy/Krnl-Hub/master/Krnl-Hub.lua', true))()");
	}

	private void toolStripMenuItem4_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/CriShoux/OwlHub/master/OwlHub.txt'))();");
	}

	private void pictureBox2_MouseEnter(object sender, EventArgs e)
	{
		toolTip1.SetToolTip((PictureBox)sender, "Click to join the server");
	}

	private void pictureBox2_MouseLeave(object sender, EventArgs e)
	{
		toolTip1.Hide((PictureBox)sender);
	}

	private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
	{
		Process.Start("https://krnl.ca/invite.php");
	}

	private void bunifuFlatButton8_Click(object sender, EventArgs e)
	{
		Process.Start("https://cdn.krnl.ca/getkey.php");
	}

	private void toolStripMenuItem7_Click(object sender, EventArgs e)
	{
		Process.Start("https://krnl.ca/invite.php");
	}

	private void krnl_MouseMove(object sender, MouseEventArgs e)
	{
		if ((dynamic)mMouseDown)
		{
			Refresh();
			SuspendLayout();
			if ((dynamic)isonEdge)
			{
				if (Cursor == Cursors.PanSouth)
				{
					if ((dynamic)e.Y > 350)
					{
						heightUnchanged = false;
						SetBounds(base.Left, base.Top, base.Width, base.Height - (base.Height - e.Y));
					}
					else
					{
						heightUnchanged = true;
						SetBounds(base.Left, base.Top, base.Width, 350);
					}
				}
				if (Cursor == Cursors.PanEast)
				{
					if ((dynamic)e.X > 690)
					{
						widthUnchanged = false;
						SetBounds(base.Left, base.Top, base.Width - (base.Width - e.X), base.Height);
					}
					else
					{
						widthUnchanged = true;
						SetBounds(base.Left, base.Top, 690, base.Height);
					}
				}
				else if (Cursor == Cursors.PanSE)
				{
					SetBounds(base.Left, base.Top, (base.Width - (base.Width - e.X) < 690) ? 690 : (base.Width - (base.Width - e.X)), (base.Height - (base.Height - e.Y) < 350) ? 350 : (base.Height - (base.Height - e.Y)));
				}
				panel3.Width = base.Width;
			}
			ResumeLayout();
		}
		else if ((dynamic)e.Y > base.Height - 10 && e.X < base.Width - 5)
		{
			Cursor = Cursors.PanSouth;
			isonEdge = true;
		}
		else if ((dynamic)e.X > base.Width - (mWidth + 2) && e.Y > base.Height - (mWidth + 2))
		{
			Cursor = Cursors.PanSE;
			isonEdge = true;
		}
		else if ((dynamic)e.X > base.Width - 5 && e.Y > button1.Size.Height)
		{
			Cursor = Cursors.PanEast;
			isonEdge = true;
		}
		else
		{
			Cursor = Cursors.Default;
			isonEdge = false;
		}
	}

	private void krnl_MouseDown(object sender, MouseEventArgs e)
	{
		if ((dynamic)e.Button == MouseButtons.Left)
		{
			mMouseDown = true;
		}
	}

	private void krnl_MouseUp(object sender, MouseEventArgs e)
	{
		mMouseDown = false;
	}

	private void bunifuFlatButton7_Click(object sender, EventArgs e)
	{
	}

	private async void anim_CompletedTask()
	{
		for (int j = 0; j < 70; j += 5)
		{
			await Task.Delay(1);
			panel3.BackColor = Color.FromArgb(30, 144 - j, 255 - j);
		}
		for (int j = 0; j < 69; j += 5)
		{
			await Task.Delay(1);
			panel3.BackColor = Color.FromArgb(30, 74 + j, 185 + j);
		}
	}

	private async void anim_AwaitingTaskFinish()
	{
		while (Anim_ATF_break)
		{
			for (int j = 0; j < 70; j++)
			{
				if ((dynamic)(!Anim_ATF_break))
				{
					panel3.BackColor = Color.FromArgb(30, 144, 255);
					break;
				}
				await Task.Delay(3);
				panel3.BackColor = Color.FromArgb(30, 144 - j, 255 - j);
			}
			for (int j = 0; j < 69; j++)
			{
				if ((dynamic)(!Anim_ATF_break))
				{
					panel3.BackColor = Color.FromArgb(30, 144, 255);
					break;
				}
				await Task.Delay(3);
				panel3.BackColor = Color.FromArgb(30, 74 + j, 185 + j);
			}
		}
	}

	private void bunifuFlatButton8_Click_1(object sender, EventArgs e)
	{
		Anim_ATF_break = true;
		anim_AwaitingTaskFinish();
		Anim_ATF_break = false;
	}

	private void bunifuFlatButton10_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem8_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source'))()");
	}

	private void renameToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		TabPage contextTab = customTabControl1.contextTab;
		if (contextTab != null)
		{
			Form prompt = new Form
			{
				Width = 200,
				Height = 50,
				MinimumSize = new Size(200, 50),
				MaximumSize = new Size(200, 50),
				FormBorderStyle = FormBorderStyle.None,
				Text = "What do you want to rename this tab to?",
				StartPosition = FormStartPosition.CenterParent
			};
			Label value = new Label
			{
				Width = 200,
				Height = 50,
				Text = "What do you want to rename this tab to?",
				Top = 0,
				Left = 0
			};
			TextBox textBox = new TextBox
			{
				Left = 0,
				Top = 30,
				Width = 150,
				Text = contextTab.Text
			};
			Button button = new Button
			{
				Text = "Ok",
				Left = 150,
				Width = 50,
				Top = 30,
				DialogResult = DialogResult.OK
			};
			prompt.TopMost = true;
			button.Click += delegate
			{
				prompt.Close();
			};
			prompt.Controls.Add(textBox);
			prompt.Controls.Add(button);
			prompt.Controls.Add(value);
			prompt.AcceptButton = button;
			string text = ((prompt.ShowDialog() == DialogResult.OK) ? textBox.Text : "");
			if (text.Length > 0)
			{
				contextTab.Text = text;
			}
		}
	}

	private void cMDXToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Pipe("loadstring(game:HttpGet('https://raw.githubusercontent.com/CMD-X/CMD-X/master/Source', true))()");
	}
}
