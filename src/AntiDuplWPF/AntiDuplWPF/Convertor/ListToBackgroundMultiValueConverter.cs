﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using AntiDuplWPF.ObjectModel;

namespace AntiDuplWPF.Convertor
{
    public class ListToBackgroundMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is IEnumerable
                //&& values[1] is uint
                && values[2] != DependencyProperty.UnsetValue
                && values[3] != DependencyProperty.UnsetValue
                && parameter != null)
            {
                System.Windows.Media.Color goodColor = (System.Windows.Media.Color)values[2];
                System.Windows.Media.Color badColor = (System.Windows.Media.Color)values[3];
                MaxProperty property = (MaxProperty)parameter;

                uint maxUint = 0;
                double maxDouble = 0.0;
                decimal maxDecimal = 0;
                ulong maxUlong = 0UL;

                uint minUint = 0;
                double minDouble = 0.0;
                decimal minDecimal = 0;
                ulong minUlong = 0UL;


                foreach (ImageInfoClass info in (IEnumerable)values[0])
                {
                    switch (property)
                    {
                        case MaxProperty.FileSize:
                            if (info.FileSize > maxUlong)
                                maxUlong = info.FileSize;
                            if (minUlong == 0UL)
                                minUlong = info.FileSize;
                            else if (info.FileSize < minUlong)
                                minUlong = info.FileSize;
                            break;
                        case MaxProperty.Blockiness:
                            if (info.Blockiness > maxDouble)
                                maxDouble = info.Blockiness;
                            if (minDouble == 0.0)
                                minDouble = info.Blockiness;
                            else if (info.Blockiness < minDouble)
                                minDouble = info.Blockiness;
                            break;
                        case MaxProperty.Bluring:
                            if (info.Bluring > maxDouble)
                                maxDouble = info.Bluring;
                            if (minDouble == 0.0)
                                minDouble = info.Bluring;
                            else if (info.Bluring < minDouble)
                                minDouble = info.Bluring;
                            break;
                        case MaxProperty.JpegPeaks:
                            if (info.JpegPeaks > maxUint)
                                maxUint = info.JpegPeaks;
                            if (minUint == 0.0)
                                minUint = info.JpegPeaks;
                            else if (info.JpegPeaks < minUint)
                                minUint = info.JpegPeaks;
                            break;
                        case MaxProperty.Resolution:
                            if (info.Width * info.Height > maxUint)
                                maxUint = info.Width * info.Height;
                            if (minUint == 0.0)
                                minUint = info.Width * info.Height;
                            else if (info.Width * info.Height < minUint)
                                minUint = info.Width * info.Height;
                            break;
                        case MaxProperty.UtilityIndex:
                            if (info.UtilityIndex > maxDecimal)
                                maxDecimal = info.UtilityIndex;
                            if (minDecimal == 0)
                                minDecimal = info.UtilityIndex;
                            else if (info.UtilityIndex < minDecimal)
                                minDecimal = info.UtilityIndex;
                            break;
                    }
                }

                switch (property)
                {
                    case MaxProperty.FileSize:
                        if (minUlong == maxUlong)
                            return new SolidColorBrush();
                        if ((ulong)values[1] == maxUlong
                            && maxUlong != 0)
                            return new SolidColorBrush(goodColor);
                        break;
                    case MaxProperty.JpegPeaks:
                        if (maxUint == minUint)
                            return new SolidColorBrush();
                        if ((uint)values[1] == minUint && minUint != 0)
                            return new SolidColorBrush(goodColor);
                        break;
                    case MaxProperty.Resolution:
                        if (maxUint == minUint)
                            return new SolidColorBrush();
                        if ((uint)values[1] * (uint)values[4] == maxUint
                            && maxUint != 0)
                            return new SolidColorBrush(goodColor);
                        break;
                    case MaxProperty.Blockiness:
                    case MaxProperty.Bluring:
                        if (maxDouble == minDouble)
                            return new SolidColorBrush();
                        if ((double)values[1] == minDouble
                            && minDouble != 0.0)
                            return new SolidColorBrush(goodColor);
                        break;
                    case MaxProperty.UtilityIndex:
                        if (maxDecimal == minDecimal)
                            return new SolidColorBrush();
                        if ((decimal)values[1] == maxDecimal
                            && maxDecimal != 0)
                            return new SolidColorBrush(goodColor);
                        break;
                }

                return new SolidColorBrush(badColor);
            }
            return new SolidColorBrush();
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
