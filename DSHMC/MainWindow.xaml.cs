﻿using System.Threading.Tasks;
using AdonisUI.Controls;
using Nefarius.Devcon;

namespace DSHMC
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(Action);
        }

        private void Action()
        {
            var instance = 0;

            while (Devcon.Find(
                DsHidMiniDriver.DeviceInterfaceGuid,
                out var path,
                out var instanceId,
                instance++))
            {
                var device = Device.GetDeviceByInstanceId(instanceId);

                var battery =
                    (DsHidMiniDriver.DsBatteryStatus) device.GetProperty<byte>(DsHidMiniDriver.BatteryStatusProperty);

                var mode =
                    (DsHidMiniDriver.DsHidDeviceMode) device.GetProperty<byte>(DsHidMiniDriver.HidDeviceModeProperty);
            }
        }
    }
}