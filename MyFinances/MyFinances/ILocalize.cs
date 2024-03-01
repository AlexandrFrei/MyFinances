using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace MyFinances
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
