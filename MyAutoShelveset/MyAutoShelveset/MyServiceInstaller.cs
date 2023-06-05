using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

[RunInstaller(true)]
  public class MyServiceInstaller : Installer
  {
    public MyServiceInstaller()
    {
      var serviceProcessInstaller = new ServiceProcessInstaller();
      var serviceInstaller = new ServiceInstaller();

      // Configure the service process installer
      serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

      // Configure the service installer
      serviceInstaller.ServiceName = "MyAutoShelveset";
      serviceInstaller.DisplayName = "MyAutoShelveset";
      serviceInstaller.Description = "MyAutoShelveset";
      serviceInstaller.StartType = ServiceStartMode.Automatic;

      // Add the installers to the installer collection
      Installers.Add(serviceProcessInstaller);
      Installers.Add(serviceInstaller);
    }
  }

