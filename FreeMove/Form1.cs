using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace FreeMove {
  public partial class Form1 : Form {
    bool loaded = false;

    public Form1() {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e) {
      SetToolTips();

      string[] args = Environment.GetCommandLineArgs();
      string path = "";
      for (int i = 1; i < args.Length; i++)
        path += args[i] + " ";
      path = path.Trim();
      if (!String.IsNullOrEmpty(path)) {
        if (Directory.Exists(path)) {
          opFolder.Checked = true;
          textBox_From.Text = path;
        } else {
          if (File.Exists(path)) {
            opFile.Checked = true;
            textBox_From.Text = path;
          }
        }
      }

      // Shell integration
      RegistrySecurity rs = new RegistrySecurity();

      string user = Environment.UserDomainName + "\\" + Environment.UserName;
      rs.AddAccessRule(new RegistryAccessRule(user,
                  RegistryRights.WriteKey | RegistryRights.ReadKey | RegistryRights.Delete,
                  InheritanceFlags.None,
                  PropagationFlags.None,
                  AccessControlType.Allow));
      RegistryKey key1 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("Folder").OpenSubKey("shell", true);
      if (key1.OpenSubKey("FreeMove") != null)
        chShellInt.Checked = true;
      loaded = true;
    }

    private async void Button_Move_Click(object sender, EventArgs e) {
      string source, destination;
      source = textBox_From.Text;
      destination = textBox_To.Text + "\\" + Path.GetFileName(source);

      if (opFolder.Checked) {
        if (CheckFolders(source, destination)) {
          bool success;

          // MOVING
          if (Directory.GetDirectoryRoot(source) == Directory.GetDirectoryRoot(destination)) {
            try {
              Directory.Move(source, destination);
              success = true;
            } catch (IOException ex) {
              Unauthorized(ex);
              success = false;
            }
          } else {
            success = await StartMoving(source, destination, false);
          }

          // LINKING
          if (success) {
            if (MakeLink(destination, source, SymbolicLink.Directory)) {
              if (checkBox1.Checked) {
                DirectoryInfo olddir = new DirectoryInfo(source);
                var attrib = File.GetAttributes(source);
                olddir.Attributes = attrib | FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly;
              }
              MessageBox.Show("Done.");
              Reset();
            } else {
              var result = MessageBox.Show("ERROR creating symbolic link.\nThe folder is in the new position but the link could not be created.\nTry running as administrator\n\nDo you want to move the files back?", "ERROR, could not create a directory junction", MessageBoxButtons.YesNo);
              if (result == DialogResult.Yes) {
                await StartMoving(destination, source, true, "Wait, moving files back");
              }
            }
          }
        }
      } else {
        if (CheckFiles(source, destination)) {
          bool success;

          // MOVING
          if (Directory.GetDirectoryRoot(source) == Directory.GetDirectoryRoot(destination)) {
            try {
              File.Move(source, destination);
              success = true;
            } catch (IOException ex) {
              Unauthorized(ex);
              success = false;
            }
          } else {
            success = await StartMoving(source, destination, false);
          }

          // LINKING
          if (success) {
            if (MakeLink(destination, source, SymbolicLink.File)) {
              if (checkBox1.Checked) {
                FileInfo oldfile = new FileInfo(source);
                var attrib = File.GetAttributes(source);
                oldfile.Attributes = attrib | FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly;
              }
              MessageBox.Show("Done.");
              Reset();
            } else {
              var result = MessageBox.Show("ERROR creating symbolic link.\nThe file is in the new position but the link could not be created.\nTry running as administrator\n\nDo you want to move the file back?", "ERROR, could not create a file junction", MessageBoxButtons.YesNo);
              if (result == DialogResult.Yes) {
                await StartMoving(Path.Combine(destination, Path.GetFileName(source)), Path.GetFullPath(source), true, "Wait, moving file back");
              }
            }
          }
        }
      }
    }

    private Task<bool> StartMoving(string source, string destination, bool DontReplace) {
      return StartMoving(source, destination, DontReplace, "Moving Files...");
    }

    private async Task<bool> StartMoving(string source, string destination, bool DontReplace, string ProgressMessage) {
      ProgressDialog pdiag = new ProgressDialog(this);
      pdiag.Show();
      this.Enabled = false;

      bool res;
      if (opFile.Checked)
        res = await Task.Run(() => MoveFile(source, destination, DontReplace));
      else
        res = await Task.Run(() => MoveFolder(source, destination, DontReplace));

      this.Enabled = true;
      pdiag.Close();
      pdiag.Dispose();
      return res;
    }

    #region SymLink
    [DllImport("kernel32.dll")]
    static extern bool CreateSymbolicLink(
      string lpSymlinkFileName,
      string lpTargetFileName,
      SymbolicLink dwFlags);

    enum SymbolicLink {
      File = 0,
      Directory = 1
    }

    private bool MakeLink(string target, string source, SymbolicLink type) {
      return CreateSymbolicLink(source, target, type);
    }
    #endregion

    #region PrivateMethods
    private bool MoveFolder(string source, string destination, bool DontReplace) {
      CopyFolder(source, destination, DontReplace);
      try {
        Directory.Delete(source, true);
        return true;
      } catch (UnauthorizedAccessException ex) {
        MoveFolder(destination, source, true);
        Unauthorized(ex);
        return false;
      }
    }

    private bool MoveFile(string source, string destination, bool DontReplace) {
      CopyFile(source, Path.GetDirectoryName(destination), DontReplace);
      try {
        File.Delete(source);
        return true;
      } catch (UnauthorizedAccessException ex) {
        MoveFile(destination, source, true);
        Unauthorized(ex);
        return false;
      }
    }

    private void Unauthorized(Exception ex) {
      MessageBox.Show("ERROR: a file could not be moved, it may be in use or you may not have the required permissions.\n\nTry running this program as administrator and/or close any program that is using the file specified in the details\n\nDETAILS: " + ex.Message, "Unauthorized Access");
    }

    private bool CheckFolders(string frompath, string topath) {
      bool passing = true;
      string errors = "";
      try {
        Path.GetFullPath(frompath);
        Path.GetFullPath(topath);
      } catch (Exception) {
        errors += "ERROR, invalid path name\n\n";
        passing = false;
      }
      string pattern = "^[A-Z]:\\\\";
      if (!Regex.IsMatch(frompath, pattern) || !Regex.IsMatch(topath, pattern)) {
        errors += "ERROR, invalid path format\n\n";
        passing = false;
      }

      if (!Directory.Exists(frompath)) {
        errors += "ERROR, source folder doesn't exist\n\n";
        passing = false;
      }
      if (Directory.Exists(topath)) {
        errors += "ERROR, destination folder already contains a folder with the same name\n\n";
        passing = false;
      }
      if (!Directory.Exists(Directory.GetParent(topath).FullName)) {
        errors += "destination folder doesn't exist\n\n";
        passing = false;
      }
      string TestFile = Path.Combine(frompath, "~deleteme");
      while (File.Exists(TestFile)) TestFile += new Random().Next(0, 10).ToString();
      try {
        System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(frompath);
        File.Create(TestFile).Close();
      } catch (UnauthorizedAccessException) {
        errors += "You do not have the required privileges to move the directory.\nTry running as administrator\n\n";
        passing = false;
      } finally {
        if (File.Exists(TestFile))
          File.Delete(TestFile);
      }


      if (!passing)
        MessageBox.Show(errors);

      return passing;
    }

    private bool CheckFiles(string fromfile, string topath) {
      bool passing = true;
      string errors = "";
      try {
        Path.GetFullPath(fromfile);
        Path.GetFullPath(topath);
      } catch (Exception) {
        errors += "ERROR, invalid path name\n\n";
        passing = false;
      }
      string pattern = "^[A-Z]:\\\\";
      if (!Regex.IsMatch(fromfile, pattern) || !Regex.IsMatch(topath, pattern)) {
        errors += "ERROR, invalid path format\n\n";
        passing = false;
      }

      if (!File.Exists(fromfile)) {
        errors += "ERROR, source file doesn't exist\n\n";
        passing = false;
      }
      if (!Directory.Exists(Directory.GetParent(topath).FullName)) {
        errors += "destination folder doesn't exist\n\n";
        passing = false;
      }
      if (File.Exists(Path.Combine(topath, Path.GetFileName(fromfile)))) {
        errors += "ERROR, destination file already contains a folder with the same name\n\n";
        passing = false;
      }
      string TestFile = Path.Combine(Path.GetDirectoryName(fromfile), "~deleteme");
      while (File.Exists(TestFile)) TestFile += new Random().Next(0, 10).ToString();
      try {
        System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(Path.GetFullPath(fromfile));
        File.Create(TestFile).Close();
      } catch (UnauthorizedAccessException) {
        errors += "You do not have the required privileges to move the directory.\nTry running as administrator\n\n";
        passing = false;
      } finally {
        if (File.Exists(TestFile))
          File.Delete(TestFile);
      }

      if (!passing)
        MessageBox.Show(errors);

      return passing;
    }

    private void CopyFolder(string sourceFolder, string destFolder, bool DontReplace) {
      if (!Directory.Exists(destFolder))
        Directory.CreateDirectory(destFolder);
      string[] files = Directory.GetFiles(sourceFolder);
      foreach (string file in files) {
        string name = Path.GetFileName(file);
        string dest = Path.Combine(destFolder, name);
        if (!(DontReplace && File.Exists(dest)))
          File.Copy(file, dest);
      }
      string[] folders = Directory.GetDirectories(sourceFolder);
      foreach (string folder in folders) {
        string name = Path.GetFileName(folder);
        string dest = Path.Combine(destFolder, name);
        CopyFolder(folder, dest, DontReplace);
      }
    }

    private void CopyFile(string sourceFile, string destFolder, bool DontReplace) {
      if (!Directory.Exists(destFolder))
        Directory.CreateDirectory(destFolder);
      string name = Path.GetFileName(sourceFile);
      string dest = Path.Combine(destFolder, name);
      if (!(DontReplace && File.Exists(dest)))
        File.Copy(sourceFile, dest);
    }

    private void Reset() {
      textBox_From.Text = "";
      textBox_To.Text = "";
      textBox_From.Focus();
    }

    private void WriteLog(string log) {
      File.AppendAllText(GetTempFolder() + @"\log.log", log);
    }

    private string ReadLog() {
      return File.ReadAllText(GetTempFolder() + @"\log.log");
    }

    private string GetTempFolder() {
      string dir = Environment.GetEnvironmentVariable("TEMP") + @"\FreeMove";
      Directory.CreateDirectory(dir);
      return dir;
    }

    private void SetToolTips() {
      ToolTip Tip = new ToolTip();
      Tip.ShowAlways = true;
      Tip.AutoPopDelay = 5000;
      Tip.InitialDelay = 600;
      Tip.ReshowDelay = 500;
      Tip.SetToolTip(this.textBox_From, "Select the folder you want to move");
      Tip.SetToolTip(this.textBox_To, "Select where you want to move the folder");
      Tip.SetToolTip(this.checkBox1, "Select whether you want to hide the shortcut which is created in the old location or not");
    }

    #endregion

    #region EventHandlers
    private void Button_BrowseFrom_Click(object sender, EventArgs e) {
      DialogResult result;
      if (opFile.Checked) {
        result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK) {
          textBox_From.Text = openFileDialog1.FileName;
        }
      } else {
        result = folderBrowserDialog1.ShowDialog();
        if (result == DialogResult.OK) {
          textBox_From.Text = folderBrowserDialog1.SelectedPath;
        }
      }
    }

    private void Button_BrowseTo_Click(object sender, EventArgs e) {
      DialogResult result = folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK) {
        textBox_To.Text = folderBrowserDialog1.SelectedPath;
      }
    }

    private void Button_Close_Click(object sender, EventArgs e) {
      Close();
    }
    #endregion

    private void GitHubToolStripMenuItem_Click(object sender, EventArgs e) {
      Process.Start("https://github.com/imDema/FreeMove");
    }

    private void chShellInt_CheckedChanged(object sender, EventArgs e) {
      RegistryKey key1, key2, key3;

      if (!loaded)
        return;

      RegistrySecurity rs = new RegistrySecurity();
      string user = Environment.UserDomainName + "\\" + Environment.UserName;
      rs.AddAccessRule(new RegistryAccessRule(user,
                  RegistryRights.WriteKey | RegistryRights.ReadKey | RegistryRights.Delete,
                  InheritanceFlags.None,
                  PropagationFlags.None,
                  AccessControlType.Allow));

      if (chShellInt.Checked) {
        if (MessageBox.Show("Confirm context menu integration?", "Shell Integration", MessageBoxButtons.YesNo) != DialogResult.Yes) {
          chShellInt.Checked = false;
          return;
        }

        try {
          key1 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("Folder").OpenSubKey("shell", true);
          key2 = key1.CreateSubKey("FreeMove", RegistryKeyPermissionCheck.Default, rs);
          key3 = key2.CreateSubKey("command", RegistryKeyPermissionCheck.Default, rs);
          key3.SetValue("", Application.ExecutablePath + " \"%1\"");
          key3.Close();
          key2.Close();
          key1.Close();
          key1 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("*").OpenSubKey("shell", true);
          key2 = key1.CreateSubKey("FreeMove", RegistryKeyPermissionCheck.Default, rs);
          key3 = key2.CreateSubKey("command", RegistryKeyPermissionCheck.Default, rs);
          key3.SetValue("", Application.ExecutablePath + " \"%1\"");
          key3.Close();
          key2.Close();
          key1.Close();
        } catch (Exception ee) {
          MessageBox.Show("Error writing to Registry (" + ee.Message + ").");
        }
      } else {
        if (MessageBox.Show("Confirm remove context menu integration?", "Shell Integration", MessageBoxButtons.YesNo) != DialogResult.Yes) {
          chShellInt.Checked = true;
          return;
        }

        try {
          key1 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("Folder").OpenSubKey("shell", true);
          key2 = key1.OpenSubKey("FreeMove", true);
          key2.DeleteSubKey("command");
          key2.Close();
          key1.DeleteSubKey("FreeMove");
          key1.Close();
          key1 = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32).OpenSubKey("*").OpenSubKey("shell", true);
          key2 = key1.OpenSubKey("FreeMove", true);
          key2.DeleteSubKey("command");
          key2.Close();
          key1.DeleteSubKey("FreeMove");
          key1.Close();
        } catch (Exception ee) {
          MessageBox.Show("Error writing to Registry (" + ee.Message + ").");
        }
      }
    }
  }
}
