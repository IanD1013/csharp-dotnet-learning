using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dometrain.EFCore.Api.Data.ValueConverters;

public class DateTimeToChar8Convertor : ValueConverter<DateTime, string>
{
    public DateTimeToChar8Convertor() : base(
        dateTime => dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
        stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture)
    )
    {
    }
}