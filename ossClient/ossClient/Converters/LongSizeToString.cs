﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OssClientMetro.Converters
{

    class LongSizeToString : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                long temp = long.Parse(value.ToString());
                if (temp == 0)
                    temp = 0;
                else
                {
                    temp = temp / 1024 + ((temp % 1024) > 0 ? 1 : 0);
                }

                return string.Format("{0:#,###0.#}", temp) + " KB";
            }
            else
                return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
