﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ReadingAppConfigFile
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadAllAppSettings();
            ReadAllConnectionStrings();

            /*
            ReadSetting("Setting1");
            ReadSetting("NotValid");
            AddUpdateAppSettings("NewSetting", "May 7, 2014");
            AddUpdateAppSettings("Setting1", "May 8, 2020");
            ReadAllSettings();*/

            System.Console.WriteLine("Addition Branch 1");
            System.Console.WriteLine("Addition Branch 2");
            System.Console.WriteLine("Another Addition for Branch 2");
            System.Console.WriteLine("Addition Branch 3");
            System.Console.ReadKey();
        }

        static void ReadAllConnectionStrings()
        {
            try
            {
                string connectionStrings = ConfigurationManager.ConnectionStrings["Database1"].ProviderName;
                Console.WriteLine(connectionStrings);
                string valueconnectionStrings = ConfigurationManager.ConnectionStrings["Database1"].ConnectionString;
                Console.WriteLine(valueconnectionStrings);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void ReadAllAppSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

    }
}
