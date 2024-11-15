/* ****************************************************************************
 * File name: App.cs
 *
 * Author: Tamás Kiss
 * Created: Nov/7/2024
 *
 * Last Editor: Tamás Kiss
 * Last Modified: Nov/7/2024
 *
 * Copyright (C) Tamás Kiss, 2024.
 * ************************************************************************* */

namespace WebApp
{
    public class App
    {
        private App()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static App Instance()
        {
            if (_app == null)
            {
                _app = new App();
            }

            return _app;
        }

        public IConfigurationRoot Config
        {
            get
            {
                return _config;
            }
        }

        private static App? _app;
        private IConfigurationRoot _config;
    }
}
