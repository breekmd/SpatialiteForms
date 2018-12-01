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
    public class SpatialiteInfo
    {
        internal static readonly string InfoQuery = @"select spatialite_version() as spatialite, proj4_version() as proj4, " +
                                        "geos_version() as geos, libxml2_version() as libxml2," +
                                        "hasiconv() as hasiconv, hasmathsql() as hasmathsql, hasgeocallbacks() as hasgeocallbacks," +
                                        "hasproj() as hasproj, hasgeos() as hasgeos, hasgeosadvanced() as hasgeosadvanced," +
                                        "hasgeostrunk() as hasgeostrunk, haslibxml2() as haslibxml2," +
                                        "hasepsg() as hasepsg, hasfreexl() as hasfreexl ;";

        public string Spatialite { get; set; }
        public string Proj4 { get; set; }
        public string Geos { get; set; }
        public string Libxml2 { get; set; }
        public bool? HasIconv { get; set; }
        public bool? HasGeoCallbacks { get; set; }
        public bool? HasMathSQL { get; set; }
        public bool? HasProj { get; set; }
        public bool? HasGeos { get; set; }
        public bool? HasGeosAdvanced { get; set; }
        public bool? HasGeosTrunk { get; set; }
        public bool? HasLibxml2 { get; set; }
        public bool? HasEpsg { get; set; }
        public bool? HasFreeXL { get; set; }
    }
}
