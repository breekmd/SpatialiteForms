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

namespace Plugin.SpatialiteForms
{
    /// <summary>
    /// Cross SpatialiteForms
    /// </summary>
    public static class CrossSpatialiteForms
    {
        static Lazy<ISpatialiteForms> implementation = new Lazy<ISpatialiteForms>(() => CreateSpatialiteForms(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static ISpatialiteForms Current
        {
            get
            {
                ISpatialiteForms ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static ISpatialiteForms CreateSpatialiteForms()
        {
#if NETSTANDARD1_1 || NETSTANDARD2_0
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new SpatialiteFormsImplementation();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}
