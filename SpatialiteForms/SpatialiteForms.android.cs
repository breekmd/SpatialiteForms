/* 
 * This file is part of the SpatialiteForms distribution (https://github.com/breekmd/SpatialiteForms).
 * Copyright (c) 2018 breekmd.
 * 
 * This program is free software: you can redistribute it and/or modify  
 * it under the terms of the GNU General Public License as published by  
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License 
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */


using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Linq;
using System.IO;
using Android.Content;

namespace Plugin.SpatialiteForms
{
    public class SpatialiteFormsImplementation : ISpatialiteForms
    {
        public SpatialiteConnection GetSpatialiteConnection(string dbPath, string prepackagedDatabaseName = null, bool overrideIfPrepackagedExists = false)
        {
            //first move the pre-packaged database if it doesnt exist already or if overrideRequired
            if (!string.IsNullOrEmpty(prepackagedDatabaseName) && (!File.Exists(dbPath) || overrideIfPrepackagedExists))
            {
                if (File.Exists(dbPath))
                {
                    File.Delete(dbPath);
                }

                Context context = Android.App.Application.Context;

                if (context != null)
                {
                    using (var br = new BinaryReader(context.Assets.Open(prepackagedDatabaseName)))
                    {
                        using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }

            SQLiteConnection sqliteConnection = new SQLiteConnection(dbPath);

            sqliteConnection.EnableLoadExtension(true);

            //enable extension via loading module through sqlite select and specifying entry point
            //as android rule makes library name to start with lib
            //and if not specified entry point, sqlite will "guess" the entry point using name
            try
            {
                sqliteConnection.Query<object>("select load_extension('libspatialite.so','sqlite3_modspatialite_init');");

                SpatialiteConnection spatialiteConnection = new SpatialiteConnection { SQLiteConnection = sqliteConnection };

                return spatialiteConnection;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
