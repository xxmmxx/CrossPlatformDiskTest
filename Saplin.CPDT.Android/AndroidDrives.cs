﻿using System.Collections.Generic;
using Android.App;
using Android.OS;
using Saplin.CPDT.UICore;
using Xamarin.Forms;

[assembly: Dependency(typeof(Saplin.CPDT.Droid.AndroidDrives))]
namespace Saplin.CPDT.Droid
{
    public class AndroidDrives  : IAndroidDrives
    {
        public IEnumerable<AndroidDrive> GetDrives()
        {
            var drives = new List<AndroidDrive>();

            var drive = new AndroidDrive();

            drive.AppFolderPath = MainActivity.Instance.FilesDir.AbsolutePath;

            InitDrive(drive, 3);

            drives.Add(drive);

            var ext = MainActivity.Instance.GetExternalFilesDirs(null);

            foreach (var e in ext)
            {
                if (e == null) continue;

                drive = new AndroidDrive();

                drive.AppFolderPath = e.AbsolutePath;

                InitDrive(drive, 4);

                drives.Add(drive);
            }

            return drives;
        }

        private static void InitDrive(AndroidDrive drive, int dashesIncludeInName)
        {
            drive.Name = drive.AppFolderPath;
            var c = 0;
            var i = 0;
            for (i = 0; i < drive.Name.Length; i++) // extract '/data/user'
            {
                if (drive.Name[i] == '/') c++;
                if (c == dashesIncludeInName) break;
            }

            if (i < drive.Name.Length)
                drive.Name = drive.Name.Substring(0, i);

            var stats = new StatFs(drive.AppFolderPath);
            drive.BytesFree = stats.AvailableBlocksLong * stats.BlockSizeLong;
        }
    }
}
