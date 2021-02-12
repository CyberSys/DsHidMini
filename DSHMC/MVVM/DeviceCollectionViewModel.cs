﻿using Microsoft.Win32;
using Nefarius.DsHidMini.Util;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Security.Principal;

namespace Nefarius.DsHidMini.MVVM
{
    public class DeviceCollectionViewModel : INotifyPropertyChanged
    {
        public DeviceCollectionViewModel()
        {
            Devices = new ObservableCollection<DeviceViewModel>();

            Devices.CollectionChanged += (sender, args) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasNoDevices"));
            };
        }

        /// <summary>
        ///     List of detected devices.
        /// </summary>
        public ObservableCollection<DeviceViewModel> Devices { get; set; }

        /// <summary>
        ///     Currently selected device, if any.
        /// </summary>
        public DeviceViewModel SelectedDevice { get; set; }

        /// <summary>
        ///     Is a device currently selected.
        /// </summary>
        public bool HasDeviceSelected => SelectedDevice != null;

        /// <summary>
        ///     Are there devices connected.
        /// </summary>
        public bool HasNoDevices => Devices.Count == 0;

        /// <summary>
        ///     Helper to check if run with elevated privileges.
        /// </summary>
        public bool IsElevated
        {
            get
            {
                var securityIdentifier = WindowsIdentity.GetCurrent().Owner;
                return !(securityIdentifier is null) && securityIdentifier
                    .IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
            }
        }

        /// <summary>
        ///     Is it possible to edit the selected device.
        /// </summary>
        public bool IsEditable => IsElevated && HasDeviceSelected;

        /// <summary>
        ///     Version to display in window title.
        /// </summary>
        public string Version => $"Version: {Assembly.GetEntryAssembly().GetName().Version}";

        private static string ParametersKey => "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\WUDF\\Services\\dshidmini\\Parameters";

        /// <summary>
        ///     Indicates if verbose logging is on or off.
        /// </summary>
        public bool VerboseOn
        {
            get
            {
                using (RegistryKey key = RegistryHelpers.GetRegistryKey(ParametersKey))
                {
                    if (key != null)
                    {
                        var value = key.GetValue("VerboseOn");
                        return (value == null) ? false : ((int)value) > 0;
                    }

                    return false;
                }
            }
            set
            {
                using (RegistryKey key = RegistryHelpers.GetRegistryKey(ParametersKey, true))
                {
                    if (key != null)
                    {
                        key.SetValue("VerboseOn", value ? 1 : 0, RegistryValueKind.DWord);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}