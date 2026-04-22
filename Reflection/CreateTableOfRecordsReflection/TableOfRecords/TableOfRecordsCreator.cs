using System.Globalization;
using System.Reflection;
using System.Text;

namespace TableOfRecords;

/// <summary>
/// Presents method that write in table form to the text stream a set of elements of type T.
/// </summary>
public static class TableOfRecordsCreator
{
    /// <summary>
    /// Write in table form to the text stream a set of elements of type T (<see cref="ICollection{T}"/>),
    /// where the state of each object of type T is described by public properties that have only build-in
    /// type (int, char, string etc.)
    /// </summary>
    /// <typeparam name="T">Type selector.</typeparam>
    /// <param name="collection">Collection of elements of type T.</param>
    /// <param name="writer">Text stream.</param>
    /// <exception cref="ArgumentNullException">Throw if <paramref name="collection"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Throw if <paramref name="writer"/> is null.</exception>
    /// <exception cref="ArgumentException">Throw if <paramref name="collection"/> is empty.</exception>
    public static void WriteTable<T>(ICollection<T>? collection, TextWriter? writer)
    {
        ArgumentNullException.ThrowIfNull(collection);
        ArgumentNullException.ThrowIfNull(writer);

        if (collection.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty");
        }

        PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        string[] propertyNames = propertyInfos.Select(f => f.Name).ToArray();

        var columnLengths = CalculateColumnLengths(propertyInfos);

        StringBuilder builder = new StringBuilder();

        AppendBorderLine(columnLengths, builder);

        AppendTableHeader(propertyNames, columnLengths, builder);

        AppendBorderLine(columnLengths, builder);

        AppendTableContent(collection, propertyInfos, columnLengths, builder);

        writer.Write(builder.ToString());
        writer.Flush();

        int[] CalculateColumnLengths(PropertyInfo[] propertyInfos)
        {
            int[] columnLengths = new int[propertyInfos.Length];

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                columnLengths[i] = propertyInfos[i].Name.Length;
            }

            foreach (var item in collection)
            {
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    var propertyValue = propertyInfos[i].GetValue(item);
                    var value = propertyValue != null
                        ? string.Format(CultureInfo.InvariantCulture, "{0}", propertyValue)
                        : string.Empty;

                    if (value.Length > columnLengths[i])
                    {
                        columnLengths[i] = value.Length;
                    }
                }
            }

            return columnLengths;
        }

        static void AppendTableHeader(string[] propertyNames, int[] columnLengths, StringBuilder builder)
        {
            for (int i = 0; i < propertyNames.Length; i++)
            {
                builder.Append("| ");
                builder.Append(propertyNames[i].PadRight(columnLengths[i]) + ' ');
            }

            builder.Append('|');
            builder.Append(Environment.NewLine);
        }

        static void AppendTableContent(ICollection<T> collection, PropertyInfo[] propertyInfos, int[] columnLengths, StringBuilder builder)
        {
            foreach (var item in collection)
            {
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    builder.Append("| ");

                    bool isNumericType = propertyInfos[i].PropertyType == typeof(int) ||
                     propertyInfos[i].PropertyType == typeof(long) ||
                     propertyInfos[i].PropertyType == typeof(decimal) ||
                     propertyInfos[i].PropertyType == typeof(double) ||
                     propertyInfos[i].PropertyType == typeof(float) ||
                     propertyInfos[i].PropertyType == typeof(DateTime);

                    var propertyValue = propertyInfos[i].GetValue(item);
                    var value = propertyValue != null
                    ? string.Format(CultureInfo.InvariantCulture, "{0}", propertyValue)
                    : string.Empty;

                    if (isNumericType)
                    {
                        builder.Append(value.PadLeft(columnLengths[i]));
                    }
                    else
                    {
                        builder.Append(value.PadRight(columnLengths[i]));
                    }

                    builder.Append(' ');
                }

                builder.Append('|');
                builder.Append(Environment.NewLine);

                AppendBorderLine(columnLengths, builder);
            }
        }

        static void AppendBorderLine(int[] columnLengths, StringBuilder builder)
        {
            foreach (var column in columnLengths)
            {
                builder.Append("+-");
                for (int i = 0; i < column; i++)
                {
                    builder.Append('-');
                }

                builder.Append('-');
            }

            builder.Append('+');
            builder.Append(Environment.NewLine);
        }
    }
}
