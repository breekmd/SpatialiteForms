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
using System.Text;

namespace Plugin.SpatialiteForms
{
    public interface ISpatialiteForms
    {
        SpatialiteConnection GetSpatialiteConnection(string dbPath,
                              string prepackagedDatabaseName = null, bool overrideIfPrepackagedExists = false);
    }
}
