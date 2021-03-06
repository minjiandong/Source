//------------------------------------------------------------------------------
//  Microsoft Avalon
//  Copyright (c) Microsoft Corporation, 2001
//
//  File: PixelFormatConverter.cs
//
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using MS.Internal;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;

using SR=MS.Internal.PresentationCore.SR;
using SRID=MS.Internal.PresentationCore.SRID;

namespace System.Windows.Media 
{
    #region PixelFormatConverter
    //------------------------------------------------------------------------------
    // PixelFormatConverter
    //------------------------------------------------------------------------------
    /// <summary>
    /// PixelFormatConverter
    /// </summary>
    public sealed class PixelFormatConverter : TypeConverter
    {
        /// <summary>
        /// CanConvertFrom - Returns whether or not this class can convert from a given type
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
        {
            if (t == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// TypeConverter method override.
        /// </summary>
        /// <param name="context">ITypeDescriptorContext</param>
        /// <param name="destinationType">Type to convert to</param>
        /// <returns>true if conversion is possible</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor) ||
                destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// test
        /// </summary>
        public new object ConvertFromString(String value)
        {
            if ( null == value)
            {
                return null;
            }

            return new PixelFormat (value);
        }

        /// <summary>
        /// test
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext td, System.Globalization.CultureInfo ci, object o)
        {
            if ( null == o)
            {
                return null;
            }

            return new PixelFormat(o as string);
        }

        /// <summary>
        /// TypeConverter method implementation.
        /// </summary>
        /// <param name="context">ITypeDescriptorContext</param>
        /// <param name="culture">current culture (see CLR specs)</param>
        /// <param name="value">value to convert from</param>
        /// <param name="destinationType">Type to convert to</param>
        /// <returns>converted value</returns>
        ///<SecurityNote>
        ///     Critical: calls InstanceDescriptor ctor which LinkDemands
        ///     PublicOK: can only make an InstanceDescriptor for PixelFormat, not an arbitrary class
        ///</SecurityNote> 
        [SecurityCritical]
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (null == destinationType)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (!(value is PixelFormat))
            {
                throw new ArgumentException(SR.Get(SRID.General_Expected_Type,"PixelFormat"));
            }

            if (destinationType == typeof(InstanceDescriptor))
            {
                ConstructorInfo ci = typeof(PixelFormat).GetConstructor(new Type[]{typeof(string)});
                PixelFormat p = (PixelFormat)value;
                return new InstanceDescriptor(ci, new object[]{p.ToString()});
            }
            else if (destinationType == typeof(string))
            {
                PixelFormat p = (PixelFormat)value;
                return p.ToString ();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
#endregion // PixelFormatConverter
}
