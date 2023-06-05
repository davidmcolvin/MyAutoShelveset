using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace MyAutoShelveset
{
  public partial class Service1 : ServiceBase
  {
    log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //LogHelper.GetLogger()
    private Timer timer;

    public Service1()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      log.Debug("OnStart");
      timer = new Timer();
      timer.Interval = 60000; // 1 minute
      timer.Elapsed += TimerElapsed;
      timer.Start();
    }

    protected override void OnStop()
    {
      log.Debug("OnStop");
      timer.Stop();
      timer.Dispose();
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
      log.Debug("TimerElapsed");
      // Check if the current time is within 8 am to 5 pm
      if (IsWithinWorkingHours())
      {
        log.Debug("IsWithinWorkingHours Is True");
        // Check if the time is exactly on the hour
        if (IsOnTheHour())
        {
          log.Debug("IsOnTheHour Is True");
          // Create shelveset
          CreateShelveset();
        }
      }
    }

    private bool IsWithinWorkingHours()
    {
      log.Debug("IsWithinWorkingHours");
      DateTime now = DateTime.Now;
      return now.Hour >= 8 && now.Hour < 17;
    }

    private bool IsOnTheHour()
    {
      log.Debug("IsOnTheHour");
      DateTime now = DateTime.Now;
      return now.Minute % 1 == 0;
    }

    private void CreateShelveset()
    {
      log.Debug("CreateShelveset");
      try
      {
        using (var process = new Process())
        {
          process.StartInfo.FileName = "powershell.exe";
          process.StartInfo.Arguments = "-ExecutionPolicy Bypass -File \"C:\\Users\\dc90888\\OneDrive - harriscomputer\\Documents\\CreateShelveset.ps1\"";
          process.StartInfo.UseShellExecute = false;
          process.StartInfo.RedirectStandardOutput = true;

          process.Start();

          // To log output to console
          log.Debug(process.StandardOutput.ReadToEnd());

          process.WaitForExit();
          log.Debug("WaitForExit");
        }
      }
      catch (Exception ex)
      {
        log.Error(ex.ToString());
      }
    }
  }
}
