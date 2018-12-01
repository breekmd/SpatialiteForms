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


using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ObjCRuntime;
using SQLite;
using System.Linq;
using Foundation;

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

                var existingDb = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(prepackagedDatabaseName),
                                                                     Path.GetExtension(prepackagedDatabaseName));
                File.Copy(existingDb, dbPath);
            }

            //different library names based on arch
            string x86_64 = "mod_spatialite_x86_64.7.dylib";
            string arm64 = "mod_spatialite_arm64.7.dylib";

            bool isSimulator = IsSimulator();

            string libPath = new FileInfo($"./Frameworks/iOSSpatialite.framework/Frameworks/{(isSimulator ? x86_64 : arm64)}")
                    .FullName;

            SQLiteConnection sqliteConnection = new SQLiteConnection(dbPath);

            //getting reference to raw db to enable loading extensions
            sqliteConnection.EnableLoadExtension(true);

            string query = $"select load_extension('{libPath}','sqlite3_modspatialite_init');";

            try
            {
                sqliteConnection.Query<object>(query);

                SpatialiteConnection spatialiteConnection = new SpatialiteConnection { SQLiteConnection = sqliteConnection };

                return spatialiteConnection;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool IsSimulator()
        {
            if (Runtime.Arch == Arch.SIMULATOR)
            {
                return true;
            }

            return false;
        }
    }
}
